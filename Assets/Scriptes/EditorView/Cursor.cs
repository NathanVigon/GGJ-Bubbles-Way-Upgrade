using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Cursor : MonoBehaviour{
    public static Cursor Instance;

    public Camera mainCamera;
    public GameObject previewPrefab;
    public SpriteRenderer spriteRenderer;
    public bool canPlaceHere;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update(){
        // Vérifier si le cursor se trouve au dessus d'un UI
        if (IsPointerOverUIObject()){
            spriteRenderer.color = new Color(0, 0, 0, 0);
            if(previewPrefab){
                previewPrefab.SetActive(false);
            }
            return;
        }
        if(previewPrefab){
            previewPrefab.SetActive(true);
        }

        // Obtenir la position de la souris dans l'espace du monde
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane; // Pour obtenir la position correcte dans l'espace du monde
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Convertir la position du monde en coordonnées de la grille
        Vector3 gridPosition = new Vector3(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y), 0);

        // Déplacer l'objet vers la position de la grille
        transform.position = gridPosition;

        CheckIfCanPlaceObject(gridPosition, previewPrefab != null);
        
        // Vérifier si le bouton gauche de la souris est enfoncé
        if(Input.GetMouseButtonDown(0)){
            if(canPlaceHere){
                PlacePrefab(gridPosition);
            }
        }
    }

    public void SetPrefab(GameObject prefab){
        if(previewPrefab != null){
            Destroy(previewPrefab);
        }

        if(prefab){
            previewPrefab = Instantiate(prefab, transform, this);
            previewPrefab.transform.position = transform.position;
        }
    }

    private void PlacePrefab(Vector3 position){
        if(previewPrefab != null){
            // Instancier le prefab à la position de la grille
            GameObject placedPrefab = Instantiate(previewPrefab, position, Quaternion.identity);
            placedPrefab.transform.parent = GridManager.Instance.transform.Find("Bubbles");
            GameTestManager.Instance.money -= placedPrefab.GetComponent<Bubble>().prix;
        } else {
            // Vérifier s'il y a un objet sous le curseur
            Collider[] colliders = Physics.OverlapSphere(position, 0.1f);

            foreach (Collider collider in colliders){
                // Vérifier si l'objet possède le script Bubble
                Bubble bubble = collider.gameObject.GetComponent<Bubble>();
                if (bubble != null){
                    // Supprimer l'objet
                    GameTestManager.Instance.money += bubble.prix;
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    private void CheckIfCanPlaceObject(Vector3 position, bool havePrefab){
        canPlaceHere = true;
        if(havePrefab){
            if(GameTestManager.Instance.money - previewPrefab.GetComponent<Bubble>().prix < 0){
                canPlaceHere = false;
            }

            Collider[] colliders = Physics.OverlapSphere(position, 0.1f);

            foreach (Collider collider in colliders){
                if (collider.gameObject != previewPrefab){
                    canPlaceHere = false;
                    break;
                }
            }

            if (canPlaceHere){
                spriteRenderer.color = new Color(0, 0, 0, 0.1f);
            } else {
                spriteRenderer.color = new Color(1, 0, 0, 0.2f);
            }
        } else {
            spriteRenderer.color = new Color(0, 0, 0, 0.1f);
        }
    }

    private bool IsPointerOverUIObject(){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Cursor : MonoBehaviour {
    public static Cursor Instance { get; private set; }

    public Camera mainCamera;
    public GameObject previewPrefab;
    public SpriteRenderer spriteRenderer;
    public bool canPlaceHere;
    [SerializeField] private Transform ParentBubble;

    public Sprite pin;
    public Sprite square;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Menu.Instance.isPaused || LevelManager.Instance.isPlaying) {
            spriteRenderer.color = new Color(0, 0, 0, 0);
            if (previewPrefab) {
                previewPrefab.SetActive(false);
            }

            return;
        }

        // Vérifier si le cursor se trouve au dessus d'un UI
        if (IsPointerOverUIObject()) {
            spriteRenderer.color = new Color(0, 0, 0, 0);
            if (previewPrefab) {
                previewPrefab.SetActive(false);
            }

            return;
        }

        if (previewPrefab) {
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

        CheckIfCanPlaceObject(gridPosition, previewPrefab);

        // Vérifier si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0)) {
            if (canPlaceHere) {
                PlacePrefab(gridPosition);
            }
        }
    }

    public void SetPrefab(GameObject prefab) {
        if (previewPrefab != null) {
            Destroy(previewPrefab);
        }

        if (prefab) {
            previewPrefab = Instantiate(prefab, transform, this);
            previewPrefab.transform.position = transform.position;
            GetComponent<SpriteRenderer>().sprite = square;
        } else {
            GetComponent<SpriteRenderer>().sprite = pin;
        }
    }

    private void PlacePrefab(Vector3 position) {
        if (previewPrefab) {
            // Instancier le prefab à la position de la grille
            GameObject placedPrefab = Instantiate(previewPrefab, position, Quaternion.identity);
            placedPrefab.transform.parent = ParentBubble;
            LevelManager.Instance.ChangeMoney(-placedPrefab.GetComponent<Bubble>().prix);
        } else {
            // Vérifier s'il y a un objet sous le curseur
            Collider[] colliders = Physics.OverlapSphere(position, 0.1f);

            foreach (Collider col in colliders) {
                // Vérifier si l'objet possède le script Bubble
                Bubble bubble = col.transform.GetComponent<Bubble>();
                if (bubble) {
                    // Supprimer l'objet
                    LevelManager.Instance.ChangeMoney(bubble.prix);
                    Destroy(bubble.gameObject);
                }
            }
        }
    }

    private void CheckIfCanPlaceObject(Vector3 position, bool havePrefab) {
        canPlaceHere = true;
        if (havePrefab) {
            if (LevelManager.Instance.money - previewPrefab.GetComponent<Bubble>().prix < 0) {
                canPlaceHere = false;
            }

            Collider[] colliders = Physics.OverlapSphere(position, 0.1f);

            foreach (Collider collider in colliders) {
                if (!collider.transform.IsChildOf(previewPrefab.transform)) {
                    canPlaceHere = false;
                    break;
                }
            }

            spriteRenderer.color = canPlaceHere ? new Color(0, 0, 0, 0.1f) : new Color(1, 0, 0, 0.5f);
        } else {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current) {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
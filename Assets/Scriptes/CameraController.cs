using UnityEngine;

public class CameraController : MonoBehaviour {
    public float zoomSpeed = 10f;

    private Camera cameraComponent;
    private bool isDragging;
    private Vector3 lastMousePosition;

    void Start() {
        cameraComponent = GetComponent<Camera>();
    }

    void Update() {
        HandleZoom();
        HandleMove();
    }

    void HandleZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) {
            cameraComponent.orthographicSize -= scroll * zoomSpeed;
            cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, 5f, 25f);
        }
    }

    void HandleMove() {
        if (Input.GetMouseButtonDown(1)) {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // Vérifie si le bouton droit de la souris est relâché
        if (Input.GetMouseButtonUp(1)) {
            isDragging = false;
        }

        // Si le bouton droit de la souris est enfoncé, déplace la caméra
        if (isDragging) {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) / (Screen.height / 2) *
                           cameraComponent.orthographicSize;
            transform.Translate(move, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }
}
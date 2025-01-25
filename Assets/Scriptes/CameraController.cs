using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance { get; private set; }

    private Camera cameraComponent;
    private float cameraHeight;
    private float cameraWidth;

    private bool isDragging;
    private Vector3 lastMousePosition;
    public int width;
    public int height;
    public Vector2 minCameraPosition;
    public Vector2 maxCameraPosition;

    public float zoomSpeed = 10f;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        cameraComponent = GetComponent<Camera>();
    }

    void Update() {
        cameraHeight = cameraComponent.orthographicSize;
        cameraWidth = cameraHeight * cameraComponent.aspect;
        
        HandleMove();
        HandleZoom();
    }

    public void SetMinAndMax(int gridWidth, int gridHeight) {
        width = gridWidth;
        height = gridHeight;
        minCameraPosition = new Vector2(-(gridWidth - 1) / 2f, -(gridHeight - 1) / 2f);
        maxCameraPosition = new Vector2((gridWidth - 1) / 2f, (gridHeight - 1) / 2f);
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

        // Constrain the camera position to the grid limits
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minCameraPosition.x + cameraWidth,
            maxCameraPosition.x - cameraWidth + 1);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minCameraPosition.y + cameraHeight,
            maxCameraPosition.y - cameraHeight + 1);
        transform.position = clampedPosition;
    }

    void HandleZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) {
            cameraComponent.orthographicSize -= scroll * zoomSpeed;
            cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, 5f, Mathf.Min(height/2f, width*cameraHeight/cameraWidth/2f, 25f));
        }
    }
}
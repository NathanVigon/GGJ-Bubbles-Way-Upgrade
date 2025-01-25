using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    public static GridManager Instance { get; private set; }

    public int gridWidthT = 100;
    public int gridHeightT = 50;
    public Color lineColor = Color.black;
    public List<LineRenderer> lines;
    public Visibility visibility = Visibility.Visible;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        lines = new List<LineRenderer>();
        GenerateGrid(gridWidthT, gridHeightT);
    }

    void GenerateGrid(int gridWidth, int gridHeight) {
        transform.position = new Vector3(-(gridWidth - 1) / 2f, -(gridHeight - 1) / 2f, 0);
        for (int x = 0; x <= gridWidth; x++) {
            DrawLine(new Vector3(x, 0, 0) + transform.position, new Vector3(x, gridHeight, 0) + transform.position);
        }

        for (int y = 0; y <= gridHeight; y++) {
            DrawLine(new Vector3(0, y, 0) + transform.position, new Vector3(gridWidth, y, 0) + transform.position);
        }

        CameraController.Instance.SetMinAndMax(gridWidth, gridHeight);
    }

    private void DrawLine(Vector3 start, Vector3 end) {
        GameObject lineObj = new GameObject("Line") {
            transform = {
                parent = transform.Find("Lines"),
            }
        };
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lines.Add(lr);
    }
    
    public void ChangeVisibility(Visibility visible) {
        visibility = visible;
        switch (visibility) {
            case Visibility.Visible:
                foreach (LineRenderer line in lines) {
                    line.enabled = true;
                }
                break;
            case Visibility.EditMode:
                if (GameTestManager.Instance.isPlaying) {
                    foreach (LineRenderer line in lines) {
                        line.enabled = false;
                    }
                } else {
                    foreach (LineRenderer line in lines) {
                        line.enabled = true;
                    }
                }
                break;
            case Visibility.Hidden:
                foreach (LineRenderer line in lines) {
                    line.enabled = false;
                }
                break;
        }
    }
    
    public void ChangeAlpha(AlphaGrid alpha) {
        switch (alpha) {
            case AlphaGrid.Full:
                foreach (LineRenderer line in lines) {
                    line.startColor = new Color(lineColor.r, lineColor.g, lineColor.b, 1);
                    line.endColor = new Color(lineColor.r, lineColor.g, lineColor.b, 1);
                }
                break;
            case AlphaGrid.Middle:
                foreach (LineRenderer line in lines) {
                    line.startColor = new Color(lineColor.r, lineColor.g, lineColor.b, 0.5f);
                    line.endColor = new Color(lineColor.r, lineColor.g, lineColor.b, 0.5f);
                }
                break;
            case AlphaGrid.Little:
                foreach (LineRenderer line in lines) {
                    line.startColor = new Color(lineColor.r, lineColor.g, lineColor.b, 0.1f);
                    line.endColor = new Color(lineColor.r, lineColor.g, lineColor.b, 0.1f);
                }
                break;
        }
    }
}
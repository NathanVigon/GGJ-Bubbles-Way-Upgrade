using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour{
    public static GridManager Instance;

    public int gridWidth = 50;
    public int gridHeight = 50;
    public Color lineColor = Color.black;
    public List<LineRenderer> lines;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start(){
        lines = new List<LineRenderer>();
        GenerateGrid();
    }

    void GenerateGrid(){
        transform.position = new Vector3(-(gridWidth-1)/2f, -(gridHeight-1)/2f, 0);
        for (int x = 0; x <= gridWidth; x++){
            DrawLine(new Vector3(x, 0, 0) + transform.position, new Vector3(x, gridHeight, 0) + transform.position);
        }

        for (int y = 0; y <= gridHeight; y++){
            DrawLine(new Vector3(0, y, 0) + transform.position, new Vector3(gridWidth, y, 0) + transform.position);
        }

    }

    void DrawLine(Vector3 start, Vector3 end){
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.parent = transform.Find("Lines");
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
}

using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private GameObject lineGO;
    private BoxCollider2D bc;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 pos;

    public float rangeOfY = 0.55f;
    private float firstY = 0;
    private float firstX = -15;
    private float secondX = 5;
    private float thirdX = 10;
    private float lineWidth = 0.1f;
    private float colliderWidth = 0.1f;
    private float nextX;
    private float nextY;
    private float z = 0;
    private float rangeOfStartX = 1;
    private float rangeOfEndX = 2;
    private int count = 0;
    private int sortingOrder = 1;
    private string materialPath = "Sprites/Default";
    private string defaultLayerName = "Default";
    private string lineName = "line";
    private string levelPref = "Level";
    private int level;
    private int initialLevel;
    private float multiple = 1/100;
    void Start()
    {
        createLineGameObject();
        addComponentsTolineGameObject();
        createLineRenderer();
        orderLine();
        addFirstLines();
        level = PlayerPrefs.GetInt(levelPref);
        initialLevel = level;
        rangeOfY += rangeOfY * multiple * (level-1);
    }

    void Update()
    {
        level = PlayerPrefs.GetInt(levelPref);
        if (initialLevel != level) {
            rangeOfY += rangeOfY * multiple;
            initialLevel = level;
        }
        drawLineWithRange();
    }

    private void addFirstLines()
    {
        addLine(firstX, firstY);
        addLine(secondX, firstY);
        addLine(thirdX, -rangeOfY);
    }

    private void createLineGameObject()
    {
        lineGO = new GameObject("DrawLines");
        if (lineGO == null)
            Debug.Log("Can not find 'lineGO' script");
    }

    private void addComponentsTolineGameObject()
    {
        lineGO.AddComponent<LineRenderer>();
        lineGO.AddComponent<SpriteRenderer>();
        lineGO.AddComponent<IsTouchingLine>();
    }

    private void createLineRenderer()
    {
        line = lineGO.GetComponent<LineRenderer>();
        lineGO.name = lineName;
        line.SetWidth(lineWidth, lineWidth);
        line.useWorldSpace = true;
        line.SetColors(Color.black, Color.white);
        line.material = new Material(Shader.Find(materialPath));
    }

    private void orderLine()
    {
        SpriteRenderer spr = lineGO.GetComponent<SpriteRenderer>();
        spr.sortingLayerName = defaultLayerName;
        spr.sortingOrder = sortingOrder;
        line.sortingLayerID = spr.sortingLayerID;
        line.sortingOrder = spr.sortingOrder;
    }

    void addLine(float x, float y)
    {
        startPos = endPos;
        nextX = x;
        nextY = y;
        pos = new Vector3(nextX, nextY, z);
        line.SetVertexCount(count + 1);
        line.SetPosition(count, pos);
        endPos = new Vector3(nextX, nextY, z);
        addColliderToLine(startPos.x, startPos.y, endPos.x, endPos.y);
        count++;
    }

    void drawLineWithRange()
    {
        nextX = Random.Range(nextX + rangeOfStartX, nextX + rangeOfEndX);
        nextY = Random.Range(-rangeOfY, rangeOfY);
        addLine(nextX, nextY);
    }

    private void addColliderToLine(float startX, float startY, float endX, float endY)
    {
        EdgeCollider2D col = lineGO.AddComponent<EdgeCollider2D>();
        col.offset = new Vector2(colliderWidth, 0);
        col.transform.parent = line.transform;
        col.isTrigger = true;
        Vector2[] pointsArray = new Vector2[]{
            new Vector2(startX, startY), new Vector2(endX, endY)
        };
        col.points = pointsArray;
    }
}

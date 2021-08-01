using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MapEditor : MonoBehaviour
{
    public Grid grdMap;

    public Color sideColor = Color.white;
    public Color insideColor = Color.black;

    public int screenWidth = 720;
    public int screenHeight = 1280;
    public float mapHeight = 10f;
    public int baseLine = -1;

    private readonly float staticZ = -1f;
    private float mapWidth = 100f;
    private float cellWidth = 10.0f;
    private float cellHeight = 1.0f;
    private int startX = 0;
    private int mw = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        cellWidth = grdMap.cellSize.x;
        cellHeight = grdMap.cellSize.y;

        var ratio = (float)Screen.width / Screen.height;
        if (screenWidth > 0 && screenHeight > 0)
        {
            ratio = (float)screenWidth / screenHeight;
        }
        var mh = Camera.main.orthographicSize * 2;
        mapWidth = mh * ratio;

        startX = Mathf.FloorToInt(mapWidth / 2);
        mw = startX;
    }

    void OnDrawGizmos()
    {
        DrawBGLine();
    }

    public void DrawBGLine()
    {
#if UNITY_EDITOR
        // InsideLine
        Gizmos.color = insideColor;
        var xPoint = -startX - cellWidth;
        var mWidth = mw + cellWidth;

        var setY = 0f;
        var rows = (mapHeight - baseLine) * (1 / cellHeight) + baseLine + 1;
        var countY = 1;
        for (float y = baseLine; y < rows; y++)
        {
            setY = (y + baseLine) * cellHeight;

            Gizmos.DrawLine(new Vector3(xPoint, setY, staticZ), new Vector3(mWidth, setY, staticZ));
            if (y != rows - 1)
            {
                Handles.Label(new Vector3(-(mapWidth / 2) - 0.5f, setY + cellHeight / 2f, 0f), "" + countY);
            }

            countY++;
        }


        var setX = 0f;
        var cells = (mw - xPoint) * (1 / cellWidth) - startX + 1;
        var countX = 1;
        for (float x = xPoint; x < cells; x++)
        {
            setX = (x + xPoint) * cellWidth;

            Gizmos.DrawLine(new Vector3(setX, baseLine, staticZ), new Vector3(setX, mapHeight, staticZ));
            if (x < cells - 1)
            {
                Handles.Label(new Vector3(setX + cellWidth / 2f, baseLine - 0.2f, 0f), "" + countX);
            }
            countX++;
        }

        // OutsideLine
        Gizmos.color = sideColor;

        Gizmos.DrawLine(new Vector3(xPoint, baseLine, staticZ), new Vector3(mWidth, baseLine, staticZ));
        Gizmos.DrawLine(new Vector3(mWidth, baseLine, staticZ), new Vector3(mWidth, mapHeight, staticZ));
        Gizmos.DrawLine(new Vector3(mWidth, mapHeight, staticZ), new Vector3(xPoint, mapHeight, staticZ));
        Gizmos.DrawLine(new Vector3(xPoint, mapHeight, staticZ), new Vector3(xPoint, baseLine, staticZ));
#endif
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 6;
    public int rows = 10;
    public float columnWidth = 0.86f;
    public float rowHeight = 0.86f;
    public Count wallCount = new Count(5, 9);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] items;

    private Transform boardHolder;
    private Transform boardItemsHolder;
    private List<Vector3> gridPositions;
    private Vector3 fixedPosition;

    private void Awake()
    {
        gridPositions = new List<Vector3>();
    }

    private void Start()
    { }

    public void SetRowsCount(int rows)
    {
        if (rows == this.rows)
        {
            return;
        }

        this.rows = rows;
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x * columnWidth, y * rowHeight, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        boardItemsHolder = new GameObject("BoardItem").transform;
        float spriteWidth = 0f;
        float spriteHeight = 0f;

        int outStartX = -1;
        int outStartY = -1;
        for (int x = outStartX; x < columns + 1; x++)
        {
            for (int y = outStartY; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate
                        , new Vector3(x * columnWidth, y * rowHeight, 0f)
                        , Quaternion.identity) as GameObject;
                instance.name = instance.name + x + y;
                instance.SetLayerOrder<SpriteRenderer>(Mathf.Abs(y - rows));
                instance.transform.SetParent(boardHolder);

                if (spriteWidth <= 0f && spriteHeight <= 0f)
                {
                    SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteWidth = spriteRenderer.size.x;
                        spriteHeight = spriteRenderer.size.y;
                    }
                }
            }
        }

        BoardHolderCenterOffset(spriteWidth, spriteHeight, outStartX, outStartY);
    }

    void BoardHolderCenterOffset(float spriteWidth, float spriteHeight, int outStartX, int outStartY)
    {
        // width
        if (columnWidth < spriteWidth)
        {
            spriteWidth = columnWidth;
        }
        spriteWidth = spriteWidth * GameManager.Instance.Scale;
        int totalColumns = columns + Mathf.Abs(outStartX) + 1;
        float mapLeftPosX = (Mathf.Abs(outStartX) * spriteWidth + spriteWidth / 2) - (spriteWidth * totalColumns / 2);

        //height
        float height = spriteHeight;
        if (rowHeight < spriteHeight)
        {
            height = rowHeight;
        }
        height = height * GameManager.Instance.Scale;
        spriteHeight = spriteHeight * GameManager.Instance.Scale;
        int totalRows = rows + Mathf.Abs(outStartY) + 1;
        float mapLeftPosY = (Mathf.Abs(outStartY) * height + spriteHeight / 2) - ((height * totalRows + spriteHeight - height) / 2);

        // fixed
        float fixVerticalStart = ((height * totalRows + spriteHeight - height) - Camera.main.orthographicSize * 2) / 2;
        fixedPosition = new Vector3(mapLeftPosX, mapLeftPosY + fixVerticalStart, 0f);
        // center reposition
        boardHolder.localPosition = fixedPosition;
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition + fixedPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity);
            instance.transform.SetParent(boardHolder);

            var itemChoice = items[Random.Range(0, items.Length)];
            var instanceItem = Instantiate(itemChoice, randomPosition, Quaternion.identity);
            instanceItem.transform.SetParent(boardItemsHolder);

            Transform target = null;
            foreach (Transform item in boardHolder.transform.GetComponentsInChildren<Transform>())
            {
                if (item.localPosition.x == instance.transform.localPosition.x &&
                    item.localPosition.y == instance.transform.localPosition.y)
                {
                    target = item;
                    break;
                }
            }

            if (target != null)
            {
                instance.SetLayerOrder<SpriteRenderer>(target.gameObject.GetLayerOrder<SpriteRenderer>());
                target.SetVisibility(false);
            }
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

        boardHolder.localScale = new Vector3(GameManager.Instance.Scale, GameManager.Instance.Scale, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
    // The board manager generates random stage

    [Serializable]
    public class Count
    {
        public int min;
        public int max;

        public Count(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public int column = 8;
    public int row = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floortiles, wallTiles, foodTiles, enemyTiles, outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Generate Position in the board
    void GeneratePosition()
    {
        gridPositions.Clear();

        for (int x = 1; x < column - 1; x++)    // x, y start with 1 so that monster wont be too close to player's starting point
        {
            for (int y = 1; y < row - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    
    // Build the Board - Generate Floor Tiles
    void SetupBoard()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < column + 1; x++) // x = -1 to include the outer walls
        {
            for (int y = -1; y < row + 1; y++)
            {
                GameObject toInstantiate = floortiles[Random.Range(0, floortiles.Length)];

                if (x == -1 || x == column || y == -1 || y == row)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void SpawnObjectAtRandom(GameObject[] tiles, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject chosenTile = tiles[Random.Range(0, tiles.Length)];
            Instantiate(chosenTile, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        SetupBoard();
        GeneratePosition();
        SpawnObjectAtRandom(wallTiles, wallCount.min, wallCount.max);
        SpawnObjectAtRandom(foodTiles, foodCount.min, foodCount.max);

        int enemyCount = (int)Mathf.Log(level, 2f);
        SpawnObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(column - 1, row - 1, 0f), Quaternion.identity);
    } 
}

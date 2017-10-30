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

    void InitialList()
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
    
    void BoardSetup()
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

                instance.transform.SetParent(boardHolder); // Minute 9:19
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

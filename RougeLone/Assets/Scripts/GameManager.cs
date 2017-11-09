using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManagerInstance = null;
    public BoardManager boardManager;

    private int level = 3; // monster spawns at lvl 3 as default
	// Use this for initialization
	void Awake () {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else if (gameManagerInstance != this)
        {
            Destroy(this); 
        }

        DontDestroyOnLoad(this); // keep game manager alive when loading new scene
        boardManager = GetComponent<BoardManager>();
        InitGame();
	}

    private void InitGame()
    {
        boardManager.SetupScene(level);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

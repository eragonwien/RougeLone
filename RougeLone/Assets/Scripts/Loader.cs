using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

	// Use this for initialization
	void Awake () {
        if (gameManager.gameObject.scene.name == null) // check if the game manager is a prefabs 
        {
            Instantiate(gameManager); 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

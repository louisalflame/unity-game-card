﻿using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        Application.Quit();
        Debug.Log("Quit?");

    }
}
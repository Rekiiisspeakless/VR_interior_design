﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour {

    private Scene targetSceneReference;
    
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string path)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(path, LoadSceneMode.Additive);
    }

    public void CloseScene()
    {
        
    }

    
}

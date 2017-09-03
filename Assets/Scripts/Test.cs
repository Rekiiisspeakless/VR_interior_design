using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Test : MonoBehaviour {
    GameObject controller;
    GameObject target;
    VRTK_Pointer pointer;
	// Use this for initialization
	void Start () {
        pointer = GetComponent<VRTK_Pointer>();
        pointer.DestinationMarkerEnter += OnEnter;
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    void OnEnter(object sender, DestinationMarkerEventArgs e)
    {
        Debug.Log(e.target.name + " " + "touch!");
    }
}

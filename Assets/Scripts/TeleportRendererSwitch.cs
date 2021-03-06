using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportRendererSwitch : MonoBehaviour {
	public bool onButtonPressed;
	public VRTK_ControllerEvents vrtkControllerEvents; 
	public VRTK_Pointer vrtk_Pointer;
	public VRTK_StraightPointerRenderer vrtk_StraightPointerRenderer;
	public VRTK_BezierPointerRenderer vrtk_BezierPointerRenderer;
	private GameObject[] straightPointerObjects;
	private GameObject[] bezierPointerObjects;

	// Use this for initialization
	void Start () {
		vrtkControllerEvents = GetComponent<VRTK_ControllerEvents> ();
		vrtk_Pointer = GetComponent<VRTK_Pointer> ();
		vrtk_StraightPointerRenderer = GetComponent<VRTK_StraightPointerRenderer> ();
		vrtk_BezierPointerRenderer = GetComponent<VRTK_BezierPointerRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		onButtonPressed = vrtkControllerEvents.buttonTwoPressed;
		if (onButtonPressed) {
			straightPointerObjects = vrtk_StraightPointerRenderer.GetPointerObjects ();
			bezierPointerObjects = vrtk_BezierPointerRenderer.GetPointerObjects ();
			for (int i = 0; i < straightPointerObjects.Length; i++) {
				straightPointerObjects [i].SetActive(false);
			}
			for (int i = 0; i < bezierPointerObjects.Length; i++) {
				bezierPointerObjects [i].SetActive(true);
			}
			vrtk_Pointer.pointerRenderer = vrtk_BezierPointerRenderer;
		} else {
			straightPointerObjects = vrtk_StraightPointerRenderer.GetPointerObjects ();
			bezierPointerObjects = vrtk_BezierPointerRenderer.GetPointerObjects ();
			for (int i = 0; i < straightPointerObjects.Length; i++) {
				straightPointerObjects [i].SetActive(true);
			}
			for (int i = 0; i < bezierPointerObjects.Length; i++) {
				bezierPointerObjects [i].SetActive(false);
			}
			vrtk_Pointer.pointerRenderer = vrtk_StraightPointerRenderer;
		}
	}
}

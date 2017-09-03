using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerGrabManager : MonoBehaviour {

	VRTK_InteractGrab interactGrab;
	GameObject target;
	Rigidbody targetRigidbody;
	VRTK_ObjectAutoGrab autoGrab;
	bool isAutoGrab = false;
	// Use this for initialization
	void Start () {
		interactGrab = GetComponent<VRTK_InteractGrab> ();
		interactGrab.ControllerGrabInteractableObject += Grab;
		interactGrab.ControllerUngrabInteractableObject += Ungrab; 
		autoGrab = GetComponent<VRTK_ObjectAutoGrab> ();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: sometimes it will grab wrong object 

		if (interactGrab.IsGrabButtonPressed () && target == null && !isAutoGrab) {
			target = interactGrab.GetGrabbedObject ();
			if (target != null) {
				Debug.Log ("Grab target = " + target.name);
				targetRigidbody = target.GetComponent<Rigidbody> ();
				targetRigidbody.isKinematic = false;
			}
		} else if (!interactGrab.IsGrabButtonPressed () && target != null) {
			targetRigidbody.isKinematic = true;
			target = null;
		} else if (isAutoGrab && interactGrab.IsGrabButtonPressed ()) {
			target = interactGrab.GetGrabbedObject ();
			if (target != null) {
				targetRigidbody = target.GetComponent<Rigidbody> ();
				targetRigidbody.isKinematic = true;
			}
			autoGrab.objectToGrab = null;
			autoGrab.enabled = false;
			isAutoGrab = false;
		}
	}

	public void SetAutoGrab(bool isAutoGrab){
		this.isAutoGrab = isAutoGrab;
	}

	public void AttempGrab(){
		// interactGrab.AttemptGrab ();
	}

	void Ungrab(object o, ObjectInteractEventArgs  e){
		// e.target.GetComponent<Rigidbody> ().isKinematic = false;
	}

	void Grab(object o, ObjectInteractEventArgs  e){
		// e.target.GetComponent<Rigidbody> ().isKinematic = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class InputFieldManager : MonoBehaviour, IPointerClickHandler
{
    InputField inputField;
    NumPadManager numPadManager;

    // Use this for initialization
    void Start () {
        inputField = GetComponent<InputField>();
        numPadManager = GameObject.FindGameObjectWithTag("NumPadManager")
            .GetComponent<NumPadManager>();
		
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked!");
		numPadManager.SetTarget(inputField);
    }

    public void SetText(string s)
    {
        inputField.text = s;
		Debug.Log ("Text change to " + inputField.text);
    }
}

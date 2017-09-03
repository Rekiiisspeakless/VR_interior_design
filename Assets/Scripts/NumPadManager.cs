using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class NumPadManager : MonoBehaviour {

    private string number;
    public InputField target;
	public GameObject targetObject;
    public GameObject previewObject;
	public GameObject canvas;
	public UIController uiController;

    Text previewText;
	[SerializeField]
	GameObject controller;
	[SerializeField]
	bool isShow;
	VRTK_UIPointer pointer;

	// Use this for initialization
	void Awake(){
		previewText = previewObject.GetComponent<Text>();
		pointer = controller.GetComponent<VRTK_UIPointer> ();
		uiController = canvas.GetComponent<UIController> ();
		pointer.UIPointerElementClick += OnUIElementClicked;
		pointer.UIPointerElementEnter += OnUIElementEnter;
		canvas.SetActive (false);
	}
	void Start () {
        
    }

	public void OnUIElementClicked(object o, UIPointerEventArgs e){
		Debug.Log (e.currentTarget.name + " clicked");
		if (e.currentTarget.name == "InputField") {
			SetTarget (e.currentTarget.GetComponent<InputField>());
			targetObject = e.currentTarget;
			uiController.Show ();
		}
	}


	public void OnUIElementEnter(object o, UIPointerEventArgs e){
		//Debug.Log (e.currentTarget.name + " entered");
	}
	
	// Update is called once per frame
	void Update () {
		isShow = uiController.isShow;
	}
	public void SetTarget(InputField target)
    {
        this.target = target;
        number = target.text;
        previewText.text = number;
    }

    public void InputNumber(string s)
    {
        number += s;
        Debug.Log("Number = " + number);
        previewText.text = number;
    }

    public void BackSpace()
    {
        Debug.Log("Delete");
        
        if (number != null && number.Length > 0)
        {
            number = number.Substring(0, number.Length - 1);
            previewText.text = number;
            Debug.Log("Number = " + number);
        }else
        {
            Debug.Log("Number length = 0");
        }
    }
    public void Enter()
    {
		Debug.Log ("Enter");
		if (target.GetComponent<InputFieldManager> () == null) 
		{
			Debug.LogWarning("InputFieldManager is null!");	
		}
        target.GetComponent<InputFieldManager>().SetText(number);
		uiController.Hide ();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;
using VRTK;

public class Inspector : MonoBehaviour {

    private GameObject target;
	private Transform targetTransform;
	private GameObject tempTarget;
	private Transform tempTargetTransform;
    private InputField[] inputFields;
    private TransformGizmos transformGizmos;
    private PanelManager panelManager;
    private VRTK_Pointer pointer;
	private bool uiTouched = false;
    private VRTK_ControllerEvents vrtk_ControllerEvents;
	private Text headerText;
	private VRTK_InteractGrab interactGrab;

	public string targetName;
    
    [SerializeField]
    GameObject inspectorPanel;
    [SerializeField]
    GameObject myCamera;
    [SerializeField]
    GameObject panelManagerObject;
	[SerializeField]
	GameObject header;
	public GameObject controller;
    void Awake()
    {
        transformGizmos = myCamera.GetComponent<TransformGizmos>();
        inputFields = inspectorPanel.GetComponentsInChildren<InputField>();
        panelManager = panelManagerObject.GetComponent<PanelManager>();
        pointer = controller.GetComponent<VRTK_Pointer>();
        vrtk_ControllerEvents = controller.GetComponent<VRTK_ControllerEvents>();
        pointer.DestinationMarkerEnter += OnPointerEnter;
        pointer.DestinationMarkerExit += OnPointerExit;
		headerText = header.GetComponentsInChildren<Text>()[0];
		interactGrab = controller.GetComponent<VRTK_InteractGrab> ();
        // pointer.DestinationMarkerHover += OnPointerEnter;
    }
    // Use this for initialization
    void Start () {
		
	}

    void OnPointerEnter(object sender, DestinationMarkerEventArgs e)
    {
		if (e.target.gameObject.layer != 5) {
			uiTouched = false;
			tempTarget = e.target.gameObject;
			tempTargetTransform = e.target;
			Debug.Log (vrtk_ControllerEvents.IsButtonPressed (VRTK_ControllerEvents.ButtonAlias.TriggerPress));
		} else {
			uiTouched = true;
		}
    }

    void OnPointerExit(object sender, DestinationMarkerEventArgs e)
    {
        tempTarget = null;
        tempTargetTransform = null;
    }
        
	
    public void SetFileTransform(GameObject target)
    {
        this.target = target;
        this.targetTransform = target.transform;
		targetName = target.name;
        UpdateInputField();
    }
    public void UpdateInputField()
    {
        // Debug.Log("Number of input field = " + inputFields.Length);
        inputFields[0].text = targetTransform.position.x.ToString();
        inputFields[1].text = targetTransform.position.y.ToString();
        inputFields[2].text = targetTransform.position.z.ToString();

        inputFields[3].text = targetTransform.eulerAngles.x.ToString();
        inputFields[4].text = targetTransform.eulerAngles.y.ToString();
        inputFields[5].text = targetTransform.eulerAngles.z.ToString();

        inputFields[6].text = targetTransform.localScale.x.ToString();
        inputFields[7].text = targetTransform.localScale.y.ToString();
        inputFields[8].text = targetTransform.localScale.z.ToString();
    }
	// Update is called once per frame
	void Update () {
		if (vrtk_ControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress) 
			&& !uiTouched && tempTarget !=null && tempTargetTransform != null)
        {
            target = tempTarget;
            targetTransform = tempTargetTransform;
			targetName = target.name;
			SetHeaderText ();
            UpdateInputField();
			if (panelManager.currentPanelName != "inspector") {
				panelManager.ChangePanelTo("inspector");
				Debug.Log("Change Panel to Inspector");
			}
        }
        if (target != null)
        {
            
            if (transformGizmos.isTransforming)
            {
                UpdateInputField();
            }
			if (!interactGrab.IsGrabButtonPressed ()) {
				UpdateTargetTransform ();
			} else {
				UpdateInputField();
			}
            
        }

    }
	public void SetHeaderText(){
		headerText.text = targetName;
	}

    void UpdateTargetTransform()
    {
        float positionX = float.Parse(inputFields[0].text);
        float positionY = float.Parse(inputFields[1].text);
        float positionZ = float.Parse(inputFields[2].text);

        target.transform.position  = new Vector3(positionX, positionY, positionZ);

        float rotationX = float.Parse(inputFields[3].text);
        float rotationY = float.Parse(inputFields[4].text);
        float rotationZ = float.Parse(inputFields[5].text);

        target.transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
         
       

        float scaleX = float.Parse(inputFields[6].text);
        float scaleY = float.Parse(inputFields[7].text);
        float scaleZ = float.Parse(inputFields[8].text);

        target.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

        //Debug.Log(inputFields[0].text + " " + inputFields[1].text + " " + inputFields[2].text);
       /* Debug.Log(target.transform.position.ToString() + "\n" +
            target.transform.rotation.ToString() + "\n" + 
            target.transform.localScale.ToString());*/
    }
}

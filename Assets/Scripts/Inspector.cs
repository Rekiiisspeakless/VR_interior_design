using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;

public class Inspector : MonoBehaviour {

    private GameObject target;
    private Transform targetTransform;
    private InputField[] inputFields;
    private TransformGizmos transformGizmos;
    
    [SerializeField]
    GameObject inspectorPanel;
    [SerializeField]
    GameObject myCamera;
    void Awake()
    {
        transformGizmos = myCamera.GetComponent<TransformGizmos>();
        inputFields = inspectorPanel.GetComponentsInChildren<InputField>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
    public void SetFileTransform(GameObject target)
    {
        this.target = target;
        this.targetTransform = target.transform;
        
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
        if(target != null)
        {
            if (transformGizmos.isTransforming)
            {
                UpdateInputField();
            }
            
            UpdateTargetTransform();
        }
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
        Debug.Log(target.transform.position.ToString() + "\n" +
            target.transform.rotation.ToString() + "\n" + 
            target.transform.localScale.ToString());
    }
}

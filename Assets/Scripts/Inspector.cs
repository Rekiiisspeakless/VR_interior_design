using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour {

    private GameObject target;
    private Transform targetTransform;
    private InputField[] inputFields;

    [SerializeField]
    GameObject inspectorPanel;
    
    // Use this for initialization
    void Start () {
		
	}
	
    public void SetFileTransform(GameObject target)
    {
        this.target = target;
        this.targetTransform = target.transform;
        inputFields = inspectorPanel.GetComponentsInChildren<InputField>();
        Debug.Log("Number of input field = " + inputFields.Length);
        inputFields[0].text = transform.position.x.ToString();
        inputFields[1].text = transform.position.y.ToString();
        inputFields[2].text = transform.position.z.ToString();

        inputFields[3].text = transform.rotation.x.ToString();
        inputFields[4].text = transform.rotation.y.ToString();
        inputFields[5].text = transform.rotation.z.ToString();

        inputFields[6].text = transform.localScale.x.ToString();
        inputFields[7].text = transform.localScale.y.ToString();
        inputFields[8].text = transform.localScale.z.ToString();
    }
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
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

        target.transform.rotation =  new Quaternion(rotationX, rotationY, rotationZ, target.transform.rotation.w);
       

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using VRTK;
using VRTK.SecondaryControllerGrabActions;

public class FileItemButton : MonoBehaviour {

    string fileAbsoluteDirectory;
    string fileRelativeDirectory;
    bool isDirectory;
    Button button;
    FileSystem fs;
    GameObject panelManagerObject;
    GameObject inspectorObject;
    GameObject sceneManagerObject;
    private PanelManager panelManager;
    private Inspector inspector;
    private SceneManager sceneManager;
	private GameObject controller;
	private ControllerGrabManager grabManager;
    
    // Use this for initialization

    void Start () {
        button = GetComponent<Button>();
        fs = GameObject.FindGameObjectWithTag("FileSystem").GetComponent<FileSystem>();
        panelManagerObject = GameObject.FindGameObjectWithTag("PanelManager");
        inspectorObject = GameObject.FindGameObjectWithTag("Inspector");
        sceneManagerObject = GameObject.FindGameObjectWithTag("SceneManager");
        button.onClick.AddListener(HandleClick);
        panelManager = panelManagerObject.GetComponent<PanelManager>();
        inspector = inspectorObject.GetComponent<Inspector>();
		controller = inspector.controller;
		grabManager = controller.GetComponent<ControllerGrabManager> ();
        sceneManager = sceneManagerObject.GetComponent<SceneManager>();
    }
	public void FileSetup(string fileAbsoluteDirectory, string fileRelativeDirectory, bool isDirectory)
    {
        this.isDirectory = isDirectory;
        this.fileAbsoluteDirectory = fileAbsoluteDirectory;
        this.fileRelativeDirectory = fileRelativeDirectory;
    }

    public void TextureSetup()
    {

    }
    void HandleClick()
    {
        
        if (fs == null)
        {
            Debug.LogError("File system not found!");
        }
        else if (!isDirectory)
        {
            if (fileAbsoluteDirectory.Contains(".prefab"))
            {

                Debug.Log("Prefab directory: " + fileRelativeDirectory);
                Object targetObject = (GameObject)AssetDatabase.LoadAssetAtPath<Object>(fileRelativeDirectory);
                GameObject target = Instantiate(targetObject) as GameObject;
                target.AddComponent<VRTK_InteractableObject>();
                target.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
                target.AddComponent<VRTK_AxisScaleGrabAction>();
                inspector.SetTarget(target);
                VRTK_ObjectAutoGrab objectAutoGrab = inspector.controller.GetComponent<VRTK_ObjectAutoGrab>();
                grabManager.SetAutoGrab(true);
                objectAutoGrab.objectToGrab = target.GetComponent<VRTK_InteractableObject>();
                objectAutoGrab.enabled = true;
                panelManager.ChangePanelTo("inspector");
            }
            else if (fileAbsoluteDirectory.Contains(".unity"))
            {
                Debug.Log("Path: " + fileRelativeDirectory);
                sceneManager.LoadScene(fileRelativeDirectory);
                //sceneManager.LoadScene("VR_Interior_Design");
            }
            else
            {
                Debug.LogError("Can't open file or directory!");
            }

        }
        else
        {
            fs.ChangeDirectory(fileAbsoluteDirectory);
        }
        
        
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}

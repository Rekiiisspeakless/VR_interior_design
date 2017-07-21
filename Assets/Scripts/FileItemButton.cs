using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
        sceneManager = sceneManagerObject.GetComponent<SceneManager>();
    }
	public void Setup(string fileAbsoluteDirectory, string fileRelativeDirectory, bool isDirectory)
    {
        this.isDirectory = isDirectory;
        this.fileAbsoluteDirectory = fileAbsoluteDirectory;
        this.fileRelativeDirectory = fileRelativeDirectory;
    }
    void HandleClick()
    {
        if(fs == null)
        {
            Debug.LogError("File system not found!");
        }
        else if(!isDirectory)
        {
            if (fileAbsoluteDirectory.Contains(".prefab"))
            {
                panelManager.ChangePanelTo("inspector");
                Debug.Log("Prefab directory: " + fileRelativeDirectory);
                Object targetObject = (GameObject)AssetDatabase.LoadAssetAtPath<Object>(fileRelativeDirectory);
                GameObject target = Instantiate(targetObject) as GameObject;
                inspector.SetFileTransform(target);
            } else if (fileAbsoluteDirectory.Contains(".unity")) {
                Debug.Log("Path: " + fileRelativeDirectory);
                sceneManager.LoadScene(fileRelativeDirectory);
                //sceneManager.LoadScene("VR_Interior_Design");
            } else
            {
                Debug.LogError("Can't open file or directory!");
            }
            
        }else
        {
            fs.ChangeDirectory(fileAbsoluteDirectory);
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}

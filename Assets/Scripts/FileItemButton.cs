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
    private PanelManager panelManager;
    private Inspector inspector;
    // Use this for initialization
    
    void Start () {
        button = GetComponent<Button>();
        fs = GameObject.FindGameObjectWithTag("FileSystem").GetComponent<FileSystem>();
        panelManagerObject = GameObject.FindGameObjectWithTag("PanelManager");
        inspectorObject = GameObject.FindGameObjectWithTag("Inspector");
        button.onClick.AddListener(HandleClick);
        panelManager = panelManagerObject.GetComponent<PanelManager>();
        inspector = inspectorObject.GetComponent<Inspector>();
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
            }else
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

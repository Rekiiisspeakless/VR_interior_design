using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProjectPanelBackButton : MonoBehaviour {

    public UIController uiController;
    public PanelManager panelManager;
    private Button button;
    private FileSystem fs;
    private string panelMode;
    
	// Use this for initialization
	void Start () {
        fs = GameObject.FindGameObjectWithTag("FileSystem").GetComponent<FileSystem>();
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
        panelMode = "project";

	}
	// Update is called once per frame
	void Update () {
        panelMode = panelManager.GetPanelName();
	}


    public void HandleClick()
    {
        if(panelMode == "project")
        {
            fs.ChangeDirectory("./");
        }else if(panelMode == "inspector")
        {
            panelManager.ChangePanelTo("project");
        }
        
    }
}

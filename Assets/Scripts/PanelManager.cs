using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    [SerializeField]
    GameObject inspectorPanel;
    [SerializeField]
    GameObject projectPanel;
    [SerializeField]
    GameObject texturePanel;
	[SerializeField]
	GameObject fileSystemObject;
	[SerializeField]
	GameObject inspectorObject;
    [SerializeField]
    GameObject textureManagerObject;
    

    private UIController inspectorUIController;
    private UIController projectUIController;
    private UIController textureUIController;
    public string currentPanelName;
    public GameObject header;
    private Text headerPanelNameText;
	private Text headerPanelContentText;
	private FileSystem filesystem;
	private Inspector inspector;
    private TextureManager textureManager;


    // Use this for initialization
    void Start () {
        inspectorUIController = inspectorPanel.GetComponent<UIController>();
        projectUIController = projectPanel.GetComponent<UIController>();
        textureUIController = texturePanel.GetComponent<UIController>();
        projectUIController.Show();
        inspectorUIController.Hide();
        currentPanelName = "project";
        // headerPanelNameText = header.GetComponentsInChildren<Text>()[0];
		headerPanelContentText = header.GetComponentsInChildren<Text>()[0];
        //headerPanelNameText.text = "Project";
		filesystem = fileSystemObject.GetComponent<FileSystem> ();
		inspector = inspectorObject.GetComponent<Inspector> ();
        textureManager = textureManagerObject.GetComponent<TextureManager>();
    }

	public void ShowCurrentPanel(){
		if (currentPanelName == "project") {
			projectUIController.Show ();
			inspectorUIController.Hide ();
            textureUIController.Hide();
		} else if(currentPanelName == "inspector"){
			projectUIController.Hide();
			inspectorUIController.Show();
            textureUIController.Hide();
        }
        else
        {
            projectUIController.Hide();
            inspectorUIController.Hide();
            textureUIController.Show();
        }
	}

	public void HideCurrentPanel(){
		projectUIController.Hide ();
		inspectorUIController.Hide ();
        textureUIController.Hide();
	}

    public void TabValueChanged(string tabName)
    {
        if (tabName == "project") {
            if(projectUIController.isShow == true)
            {
                projectUIController.Hide();
            }else
            {
                projectUIController.Show();
                currentPanelName = "project";
                // headerPanelNameText.text = "Project";
                headerPanelContentText.text = filesystem.currentRelativeDirectory;
            }
        }else if (tabName == "inspector")
        {
            if (inspectorUIController.isShow == true)
            {
                inspectorUIController.Hide();
            }
            else
            {
                inspectorUIController.Show();
                currentPanelName = "inspector";
                // headerPanelNameText.text = "Inspector";
                headerPanelContentText.text = inspector.targetName;
            }
        }else if(tabName == "texture")
        {
            if(textureUIController.isShow == true)
            {
                textureUIController.Hide();
            }else
            {
                textureUIController.Show();
                currentPanelName = "texture";
                headerPanelContentText.text = textureManager.textureName;
            }
        }
    }

	public void ChangePanelTo(string panelName)
    {
        if(currentPanelName == panelName)
        {
            return;
        }else if(panelName == "project")
        {
            projectUIController.Show();
            inspectorUIController.Hide();
            textureUIController.Hide();
            currentPanelName = "project";
            // headerPanelNameText.text = "Project";
			headerPanelContentText.text = filesystem.currentRelativeDirectory;
        }
        else if(panelName == "inspector")
        {
            projectUIController.Hide();
            inspectorUIController.Show();
            textureUIController.Hide();
            currentPanelName = "inspector";
            // headerPanelNameText.text = "Inspector";
			headerPanelContentText.text = inspector.targetName;
        }else if (panelName == "texture")
        {
            projectUIController.Hide();
            inspectorUIController.Hide();
            textureUIController.Show();
            currentPanelName = "texture";

            headerPanelNameText.text = textureManager.textureName;
        }
    }
    
    public string GetPanelName()
    {
        return currentPanelName;
    }
	// Update is called once per frame
	void Update () {
		
	}
}

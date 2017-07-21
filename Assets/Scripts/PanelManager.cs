using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    [SerializeField]
    GameObject inspectorPanel;
    [SerializeField]
    GameObject projectPanel;

    private UIController inspectorUIController;
    private UIController projectUIController;
    private string currentPanelName;
    public GameObject header;
    private Text headerPanelNameText;


    // Use this for initialization
    void Start () {
        inspectorUIController = inspectorPanel.GetComponent<UIController>();
        projectUIController = projectPanel.GetComponent<UIController>();
        projectUIController.Show();
        inspectorUIController.Hide();
        currentPanelName = "project";
        headerPanelNameText = header.GetComponentsInChildren<Text>()[0];
        headerPanelNameText.text = "Project";
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
            currentPanelName = "project";
            headerPanelNameText.text = "Project";
        }
        else if(panelName == "inspector")
        {
            projectUIController.Hide();
            inspectorUIController.Show();
            currentPanelName = "inspector";
            headerPanelNameText.text = "Inspector";
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

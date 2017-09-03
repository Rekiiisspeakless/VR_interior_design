using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AssetsMenuController : MonoBehaviour {
	public GameObject assetMenu;
	public GameObject Controller;
    [SerializeField]
    public GameObject assetsMenuPanel;
	public UIController assetsMenuPanelController;
    public bool isShow = true;
	private Quaternion initialRotation;
	private GameObject panelManagerObject;
	private PanelManager panelManager;

	// Use this for initialization
	void Start () {
		assetMenu = GameObject.FindGameObjectWithTag ("AssetsMenu");
		// Controller = GameObject.FindGameObjectWithTag ("LeftController");
		assetsMenuPanelController = assetsMenuPanel.GetComponent<UIController>();
		initialRotation = this.transform.rotation;
		panelManagerObject = GameObject.FindGameObjectWithTag ("PanelManager");
		panelManager = panelManagerObject.GetComponent<PanelManager> ();
    }

	// Update is called once per frame
	void Update () {
		assetMenu.transform.localScale = new Vector3 (0.001f, 0.001f, 0.001f);
		assetMenu.transform.position = Controller.transform.position + new Vector3(0, 0.2f, 0);
        assetMenu.transform.rotation = Controller.transform.rotation;
	}	
    public void ShowPanel()
    {
        isShow = true;
		assetsMenuPanelController.Show();
		panelManager.ShowCurrentPanel ();
    }
    public void ClosePanel()
    {
        isShow = false;
		assetsMenuPanelController.Hide();
		panelManager.HideCurrentPanel ();
    }

	public Quaternion getInitialRotation(){
		return this.initialRotation;
	}
}
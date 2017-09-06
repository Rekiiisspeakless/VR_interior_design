using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RuntimeGizmos;
using VRTK;

public class Inspector : MonoBehaviour {

    private GameObject target;
	private Transform targetTransform;
    private Material targetMaterial;
    private Texture targetBumpMap;
    private Texture targetMainTexture;
    private Vector2 targetBumpMapOffset;
    private Vector2 targetBumpMapTiling;
    private Vector2 targetMainTextureOffset;
    private Vector2 targetMainTextureTiling;

    private GameObject tempTarget;
	private Transform tempTargetTransform;
    private InputField[] inputFields;
    private TransformGizmos transformGizmos;
    private PanelManager panelManager;
    private TextureManager textureManager;
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
    GameObject textureManagerObject;
	[SerializeField]
	GameObject header;

    [SerializeField]
    Image mainTexture;
    [SerializeField]
    Image bumpMap;

    [SerializeField]
    Button mainTextureButton;
    [SerializeField]
    Button bumpMapButton;

	public GameObject controller;
    void Awake()
    {
        transformGizmos = myCamera.GetComponent<TransformGizmos>();
        inputFields = inspectorPanel.GetComponentsInChildren<InputField>();
        panelManager = panelManagerObject.GetComponent<PanelManager>();
        textureManager = textureManagerObject.GetComponent<TextureManager>();
        pointer = controller.GetComponent<VRTK_Pointer>();
        vrtk_ControllerEvents = controller.GetComponent<VRTK_ControllerEvents>();
        pointer.DestinationMarkerEnter += OnPointerEnter;
        pointer.DestinationMarkerExit += OnPointerExit;
		headerText = header.GetComponentsInChildren<Text>()[0];
		interactGrab = controller.GetComponent<VRTK_InteractGrab> ();

        mainTextureButton.onClick.AddListener(OnMainTextureButtonClick);
        bumpMapButton.onClick.AddListener(OnBumpMapButtonClick);

        // pointer.DestinationMarkerHover += OnPointerEnter;
    }
    // Use this for initialization
    void Start () {
		
	}

    void OnMainTextureButtonClick()
    {
        textureManager.SetTarget(target, "_MainTexture", targetMainTexture);
    }

    void OnBumpMapButtonClick()
    {
        textureManager.SetTarget(target, "_BumpMap", targetBumpMap);
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
        
	
    public void SetTarget(GameObject target)
    {
        this.target = target;
        targetTransform = target.transform;
        targetMaterial = target.GetComponent<Renderer>().GetComponent<Material>();
        targetBumpMap = targetMaterial.GetTexture("_BumpMap");
        targetMainTexture = targetMaterial.GetTexture("_MainTexture");
        targetBumpMapOffset = targetMaterial.GetTextureOffset("_BumpMap");
        targetMainTextureOffset = targetMaterial.GetTextureOffset("_MainTexture");
        targetBumpMapTiling = targetMaterial.GetTextureScale("_BumpMap");
        targetMainTextureTiling = targetMaterial.GetTextureScale("_MainTexture");

        targetName = target.name;
        UpdateInputField();
    }
    public void UpdateInputField()
    {
        // transform
        inputFields[0].text = targetTransform.position.x.ToString();
        inputFields[1].text = targetTransform.position.y.ToString();
        inputFields[2].text = targetTransform.position.z.ToString();

        inputFields[3].text = targetTransform.eulerAngles.x.ToString();
        inputFields[4].text = targetTransform.eulerAngles.y.ToString();
        inputFields[5].text = targetTransform.eulerAngles.z.ToString();

        inputFields[6].text = targetTransform.localScale.x.ToString();
        inputFields[7].text = targetTransform.localScale.y.ToString();
        inputFields[8].text = targetTransform.localScale.z.ToString();

        // material
        inputFields[9].text = targetMainTextureTiling.x.ToString();
        inputFields[10].text = targetMainTextureTiling.y.ToString();

        inputFields[11].text = targetMainTextureOffset.x.ToString();
        inputFields[12].text = targetMainTextureOffset.y.ToString();

        inputFields[13].text = targetBumpMapTiling.x.ToString();
        inputFields[14].text = targetBumpMapTiling.y.ToString();

        inputFields[15].text = targetBumpMapOffset.x.ToString();
        inputFields[16].text = targetBumpMapOffset.y.ToString();
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
				UpdateTarget();
			} else {
				UpdateInputField();
			}
            
        }

    }
	public void SetHeaderText(){
		headerText.text = targetName;
	}

    void UpdateTarget()
    {
        // transform
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

        // material

        ////main texture
        float mainTextureTilingX = float.Parse(inputFields[9].text);
        float mainTextureTilingY = float.Parse(inputFields[10].text);

        targetMaterial.SetTextureScale("_MainTexture", 
            new Vector2(mainTextureTilingX, mainTextureTilingY));

        float mainTextureOffsetX = float.Parse(inputFields[11].text);
        float mainTextureOffsetY = float.Parse(inputFields[12].text);

        targetMaterial.SetTextureOffset("_MainTexture",
            new Vector2(mainTextureOffsetX, mainTextureOffsetY));

        /////bumpmap

        float bumpMapTilingX = float.Parse(inputFields[13].text);
        float bumpMapTilingY = float.Parse(inputFields[14].text);

        targetMaterial.SetTextureScale("_BumpMap",
            new Vector2(bumpMapTilingX, bumpMapTilingY));

        float bumpMapOffsetX = float.Parse(inputFields[15].text);
        float bumpMapOffsetY = float.Parse(inputFields[16].text);

        targetMaterial.SetTextureOffset("_BumpMap",
            new Vector2(bumpMapOffsetX, bumpMapOffsetY));


        targetBumpMap = targetMaterial.GetTexture("_BumpMap");
        Sprite bumpMapSprite = Sprite.Create((Texture2D)targetBumpMap, 
            new Rect(0.0f, 0.0f, targetBumpMap.width, targetBumpMap.height), 
            Vector2.zero);
        bumpMap.sprite = bumpMapSprite;

        targetMainTexture = targetMaterial.GetTexture("_MainTexture");
        Sprite mainTextureSprite = Sprite.Create((Texture2D)targetMainTexture,
            new Rect(0.0f, 0.0f, targetMainTexture.width, targetMainTexture.height),
            Vector2.zero);
        mainTexture.sprite = mainTextureSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TextureManager : MonoBehaviour {

    [SerializeField]
    GameObject fileItemInstance;
    [SerializeField]
    Transform contentTransfrom;
    [SerializeField]
    GameObject sphereInstance;
    [SerializeField]
    Shader shader;
    [SerializeField]
    GameObject header;

    private GameObject[] fileItemList;
    private Object[] assetList;
    private string[] guids;
    private GameObject target;
    private Texture texture;
    private Text headerText;

    public string textureName;

    // Use this for initialization
    void Start () {
        headerText = header.GetComponentsInChildren<Text>()[0];
        LoadTexture();   
    }

    void LoadTexture() { 
        guids = AssetDatabase.FindAssets("t:texture");
        assetList = new Object[guids.Length];
        fileItemList = new GameObject[guids.Length];
        for (int i = 0; i < guids.Length; ++i)
        {
            string fileRelativeDir = AssetDatabase.GUIDToAssetPath(guids[i]);
            Debug.Log(fileRelativeDir);
            assetList[i] = AssetDatabase.LoadMainAssetAtPath(fileRelativeDir);
            fileItemList[i] = Instantiate<GameObject>(fileItemInstance);

            //TODO: change fileItemButton to fileItemToggle
            Text name = fileItemList[i].GetComponentInChildren<Text>();
            RawImage thumbNail = fileItemList[i].GetComponentInChildren<RawImage>();
            Button button = fileItemList[i].GetComponentInChildren<Button>();

            Texture texture = (Texture)assetList[i];
            // sphere.AddComponent(typeof(Material));

            if (assetList[i].name.Length > 10)
            {
                name.text = assetList[i].name.Substring(0, 10) + "...";
            }
            else
            {
                name.text = assetList[i].name;
            }

            Texture2D thumbNailTexture = AssetPreview.GetMiniThumbnail(assetList[i]);
            // Texture2D thumbNailTexture = AssetPreview.GetAssetPreview(sphereInstance);
            
            thumbNail.texture = thumbNailTexture;
            fileItemList[i].transform.SetParent(contentTransfrom);
            RectTransform fileItemRectTransform = fileItemList[i].GetComponent<RectTransform>();
            fileItemRectTransform.localPosition = new Vector3(fileItemRectTransform.localPosition.x, fileItemRectTransform.localPosition.y, 0.0f);
            fileItemRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    public void SetTarget(GameObject target) {
        this.target = target;
    }

    public void SetTexture(Texture texture, string textureName) {
        this.texture = texture;
        this.textureName = textureName;
        headerText.text = textureName;
    }

    public void ApplyTexture()
    {
        Material targetMaterial;
        targetMaterial = target.GetComponent<Renderer>().material;
        targetMaterial = new Material(shader);
        targetMaterial.mainTexture = texture;
    }

	// Update is called once per frame
	void Update () {
		
	}
}

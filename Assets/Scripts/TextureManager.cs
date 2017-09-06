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
    GameObject header;

    private GameObject[] fileItemList;
    private Object[] assetList;
    private string[] guids;
    private GameObject target;
    private Texture texture;
    private Text headerText;
    private string targetTextureType;
    private Texture targetTexture;

    public string textureName;
    public Image[] images;

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
            // Debug.Log(fileRelativeDir);
            assetList[i] = AssetDatabase.LoadMainAssetAtPath(fileRelativeDir);
            fileItemList[i] = Instantiate<GameObject>(fileItemInstance);

            //TODO: change fileItemButton to fileItemToggle
            FileItemToggle fileItemToggle = fileItemList[i].GetComponentInChildren<FileItemToggle>();
            Text name = fileItemList[i].GetComponentInChildren<Text>();
            Toggle toggle = fileItemList[i].GetComponentInChildren<Toggle>();
            images = fileItemList[i].GetComponentsInChildren<Image>();
            Debug.Log("images length = " + images.Length);
            Image thumbNail = images[1];
            images[2] = null;

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

            if (texture == targetTexture)
            {
                toggle.isOn = true;
            }else
            {
                toggle.isOn = false;
            }
            Texture2D thumbNailTexture = AssetPreview.GetMiniThumbnail(assetList[i]);
            // Texture2D thumbNailTexture = AssetPreview.GetAssetPreview(sphereInstance);

            Sprite s = Sprite.Create(thumbNailTexture, 
                new Rect(0, 0, thumbNailTexture.width, thumbNailTexture.height), 
                Vector2.zero);
            thumbNail.sprite = s;
            fileItemList[i].transform.SetParent(contentTransfrom);
            
            RectTransform fileItemRectTransform = fileItemList[i].GetComponent<RectTransform>();
            fileItemRectTransform.localPosition = new Vector3(fileItemRectTransform.localPosition.x, fileItemRectTransform.localPosition.y, 0.0f);
            fileItemRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    public void SetTarget(GameObject target, string targetTextureType, Texture targetTexture) {
        this.target = target;
        this.targetTextureType = targetTextureType;
        this.targetTexture = targetTexture;
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
        targetMaterial.SetTexture(targetTextureType, texture);   
    }

	// Update is called once per frame
	void Update () {
		
	}
}

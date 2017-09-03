using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FileItemToggle : MonoBehaviour {

    GameObject textureManager;
    string fileRelativeDirectory = null;
    Text headerText;
    Texture texture = null;
    Toggle toggle;
    // Use this for initialization
    void Start () {
        textureManager = GameObject.FindGameObjectWithTag("TextureManager");
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ToggleClick);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDirectory(string fileRelativeDirectory)
    {
        this.fileRelativeDirectory = fileRelativeDirectory;
        texture = (Texture)AssetDatabase.LoadMainAssetAtPath(fileRelativeDirectory);
    }

    void ToggleClick(bool isChecked)
    {
        if (isChecked)
        {
            if(texture == null)
            {
                textureManager.GetComponent<TextureManager>().SetTexture(texture, "None");
            }
            else
            {
                textureManager.GetComponent<TextureManager>().SetTexture(texture, texture.name);
            }
            
        }
    }
}

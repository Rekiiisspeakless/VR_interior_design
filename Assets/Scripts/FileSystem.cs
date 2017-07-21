using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class FileSystem : MonoBehaviour {
    [SerializeField]
    Transform contentTransfrom;
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject fileItemInstance;
    [SerializeField]
    RawImage image;
    [SerializeField]
    Object o;
    [SerializeField]
    Text fileName;
    [SerializeField]
    Material materialInstance;
    [SerializeField]
    GameObject header;

    private  DirectoryInfo currentDirectoryInfo;
    private string currentDirectory;
    private DirectoryInfo dir;
    private GameObject[] fileItemList;
    private Object[] assetList;
    private string parentDirectoryName = "";

    // Use this for initialization
    void Start () {
        currentDirectory = Application.dataPath;
        
        LoadDirectory();

        /*fileItemList = new GameObject[1];
        fileItemList[0] = Instantiate<GameObject>(fileItemInstance);
        Text name = fileItemList[0].GetComponentInChildren<Text>();
        RawImage thumbNail = fileItemList[0].GetComponentInChildren<RawImage>();
        Debug.Log(name.text + " loaded...");
        string[] guid = AssetDatabase.FindAssets("Prefabs");
        Debug.Log(guid.Length + " results found...");
        string path = AssetDatabase.GUIDToAssetPath(guid[0]);

        assetList = new Object[1];
        assetList[0] = AssetDatabase.LoadMainAssetAtPath(path);
        Debug.Log("Path: " + path);
        name.text = assetList[0].name;
        Texture2D thumbNailTexture = AssetPreview.GetMiniThumbnail(assetList[0]);
        thumbNail.texture = thumbNailTexture;
        fileItemList[0].transform.SetParent(contentTransfrom);*/
    }
	
	// Update is called once per frame
	void Update () {
        /*text.text = dir.ToString();
        Material m = Instantiate(materialInstance);
        string[] results = AssetDatabase.FindAssets("Prefabs");
        Texture2D texture2d = AssetPreview.GetMiniThumbnail(o);
        m.SetTexture("_MainTex", texture2d);
        image.texture = texture2d;
        //fileName.text = o.name;
        if(results.Length == 0)
        {
            Debug.LogError("No matching result!");
        }else
        {
            fileName.text = results[0];
        }*/
        
        
    }
    public void LoadDirectory()
    {
        currentDirectoryInfo = new DirectoryInfo(currentDirectory);
        string currentRelativeDirectory = currentDirectory.Substring(Application.dataPath.Length - 6, currentDirectory.Length - Application.dataPath.Length + 6);
        Debug.Log("Current directory: " + currentRelativeDirectory);
        Text headerText = header.GetComponentsInChildren<Text>()[1];
        headerText.text = currentRelativeDirectory;
        FileInfo[] info = currentDirectoryInfo.GetFiles("*.meta");
        DirectoryInfo[] directory = currentDirectoryInfo.GetDirectories(Application.dataPath);
        assetList = new Object[info.Length];
        fileItemList = new GameObject[info.Length];
        for (int i = 0; i < info.Length; ++i)
        {
            string fileName = info[i].Name.Substring(0, info[i].Name.Length - 5);
            string fileRelativeDir = "Assets" + info[i].FullName.Substring(Application.dataPath.Length,
                info[i].FullName.Length - Application.dataPath.Length - 5).Replace('\\', '/');
            string fileAbsoluteDir = info[i].FullName.Substring(0,
                info[i].FullName.Length - 5).Replace('\\', '/');
            Debug.Log("File: " + fileName +
                "\nRelative Path: "  + fileRelativeDir + 
                "\nAbsolute Path: " + fileAbsoluteDir);
            assetList[i] = AssetDatabase.LoadMainAssetAtPath(fileRelativeDir);
            fileItemList[i] = Instantiate<GameObject>(fileItemInstance);
            Text name = fileItemList[i].GetComponentInChildren<Text>();
            RawImage thumbNail = fileItemList[i].GetComponentInChildren<RawImage>();
            Button button = fileItemList[i].GetComponentInChildren<Button>();
            if (fileName.Contains("."))
            {
                // Debug.Log("File " + fileName + " is not a directory.");
                button.GetComponent<FileItemButton>().Setup(fileAbsoluteDir, fileRelativeDir, false);
            }else
            {
                // Debug.Log("File " + fileName + " is a directory.");
                button.GetComponent<FileItemButton>().Setup(fileAbsoluteDir, fileRelativeDir, true);
            }
            
            if(assetList[i].name.Length > 10)
            {
                name.text = assetList[i].name.Substring(0, 10) + "...";
            }
            else
            {
                name.text = assetList[i].name;
            }
            
            Debug.Log(name.text + " loaded...");
            Texture2D thumbNailTexture = AssetPreview.GetMiniThumbnail(assetList[i]);
            thumbNail.texture = thumbNailTexture;
            fileItemList[i].transform.SetParent(contentTransfrom);
        }
    }
    
    public void ChangeDirectory(string targetDir)
    {
        
        if(targetDir == "./")
        {
            if(parentDirectoryName != "")
            {
                RemoveDirectory();
                currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - parentDirectoryName.Length);
                string[] directorySplit = currentDirectory.Split('/');
                if (directorySplit[directorySplit.Length - 1] == "Assets")
                {
                    parentDirectoryName = "";
                }else
                {
                    parentDirectoryName = "/" + directorySplit[directorySplit.Length - 1];
                }
                LoadDirectory();
            }
        }else
        {
            RemoveDirectory();
            parentDirectoryName = targetDir.Substring(currentDirectory.Length, targetDir.Length - currentDirectory.Length);
            Debug.Log("Parent Directory change to " + parentDirectoryName + "...");
            currentDirectory = targetDir;
            LoadDirectory();
        }
        
        
        // TODO: not deleting object from asset list may or maynot cause problem
        
    }

    public void RemoveDirectory()
    {
        for(int i = 0; i < fileItemList.Length; ++i)
        {
            GameObject.Destroy(fileItemList[i]);
        }
    }

    

    public static T[] GetAtPath<T>(string path)
    {

        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
        foreach (string fileName in fileEntries)
        {
            int index = fileName.LastIndexOf("/");
            string localPath = "Assets/" + path;

            if (index > 0)
                localPath += fileName.Substring(index);

            Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];

        return result;
    }
}

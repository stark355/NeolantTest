using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFiles : MonoBehaviour {

    public string[] fullAdressList;
    static List<Text> childList;
    public Text parentItem;
    public List <string> truncList;
    //int currentPrefab = -1;
    public GameObject myPrefab;


    // Use this for initialization
    void Start () {
        childList = new List<Text>();
        truncList = new List<string>();
        GetListMethod();
    }
	// Update is called once per frame
	void Update () {
	}

    public void GetListMethod()
    {
        fullAdressList = System.IO.Directory.GetFiles(System.Environment.CurrentDirectory + "\\Assets\\Prefabs", "*.prefab");
        //parentItem = GameObject.Find("PanelTask4/ListContentParentButton").GetComponent<Button>();
        parentItem.gameObject.SetActive(false);
        //генерация укороченных адресов
        for (int i = 0; i < fullAdressList.Length; i++)
        {
            string[] pathArr = fullAdressList[i].Split('\\');
            truncList.Add(pathArr[pathArr.Length - 1]);
        }
        GenerateList();
    }
    public void GenerateList()
    {
        parentItem.gameObject.SetActive(true);
        for(int i = 0; i < fullAdressList.Length; i++)
        {
            Text tmp = Instantiate(parentItem, parentItem.transform.position + new Vector3(0, -25 * (i + 1), 0), Quaternion.identity);
            //tmp.transform.parent = gameObject.transform;
            //tmp.transform.SetParent(gameObject.transform);
            tmp.transform.SetParent(gameObject.transform);
            tmp.GetComponentInChildren<Text>().text = truncList[i];
            //Debug.Log(tmp.GetComponentInChildren<Text>().text + "inside");
            childList.Add(tmp);
        }
        parentItem.gameObject.SetActive(false);
    }
    
    public void GetPrefabFromClick(string s)
    {
        Debug.Log(s);
        for (int i = 0; i < truncList.Count; i++)
        {
            if (s == truncList[i])
            {
                string pref = fullAdressList[i];
                //myPrefab = GameObject.Find(fullAdressList[i]).GetComponent<GameObject>();
                Debug.Log(pref);
            }
        }
        {

        }
        //currentPrefab = i;
    }

    public void ClickOnText(GameObject g)
    {
        //Text txt = GetComponent<Text>();
        //Text txt = g.gameObject.
        //GetPrefabFromClick(txt.text);
    }

        public void Generate()
    {
        //if -1 - error
    }
}

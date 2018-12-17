using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GetFiles : MonoBehaviour {

    public string[] fullAdressList; //массив с полными путями к префабам
    static List<GameObject> prefabLabelList; //список имен префабов
    public Text parentItem; //родительский элемент для имен префабов
    public List <string> truncList; //список с именами перфабов (с расширениями)
    public List <string> prefabNameList; //список с именами префабов (без расширений)
    public List<GameObject> prefabList; //список, содержащий объекты префабов
    public ErrorEngine errEngine;
    string pathToPrefub = "";


    // Use this for initialization
    void Start () {
        prefabLabelList = new List<GameObject>();
        truncList = new List<string>();
        prefabNameList = new List<string>();
        GetListMethod();
        prefabList = new List<GameObject>();
        errEngine = GameObject.Find("ErrorText").GetComponent<ErrorEngine>();
        //GameObject gmobj = Resources.Load("Prefabs\\Cube") as GameObject;
        //Instantiate(gmobj);
    }
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// получить список префабов в папке
    /// </summary>
    public void GetListMethod()
    {
        fullAdressList = System.IO.Directory.GetFiles(System.Environment.CurrentDirectory + "\\Assets\\Resources\\Prefabs", "*.prefab");
        //parentItem = GameObject.Find("PanelTask4/ListContentParentButton").GetComponent<Button>();
        
        //parentItem.gameObject.SetActive(false);
        //генерация укороченных адресов
        for (int i = 0; i < fullAdressList.Length; i++)
        {
            string[] pathArr = fullAdressList[i].Split('\\');
            truncList.Add(pathArr[pathArr.Length - 1]);
        }
        for (int i = 0; i < truncList.Count; i++)
        {
            string[] pathArr = truncList[i].Split('.');
            prefabNameList.Add(pathArr[0]);
        }
        GenerateList();
    }

    /// <summary>
    /// сгенерировать список кнопок для префабов
    /// </summary>
    public void GenerateList()
    {
        //строка для имени префаба
        string tmpName;
        for (int i = 0; i < fullAdressList.Length; i++)
        {
            tmpName = "PrefubNum" + i;
            GameObject tmp = new GameObject(tmpName);
            tmp.transform.SetParent(gameObject.transform);
            RectTransform trans = tmp.AddComponent<RectTransform>();
            //      TODO: сделать относительный сдвиг
            trans.position = new Vector3(parentItem.transform.position.x, parentItem.transform.position.y - (20 * i), parentItem.transform.position.z);
            //кнопка, на которую повесится клик
            Button btn = tmp.AddComponent<Button>();
            //номер кнопки в массиве кнопок (Game Object'ов)
            var ind = i;
            btn.onClick.AddListener(delegate { GetPrefabName((int)ind); });
            //текст для отображения имен префабов
            Text tmpText = tmp.AddComponent<Text>();
            tmpText.text = prefabNameList[i];
            tmpText.fontSize = parentItem.fontSize;
            tmpText.font = parentItem.font;
            tmpText.color = parentItem.color;
            trans.sizeDelta = parentItem.rectTransform.sizeDelta;
            prefabLabelList.Add(tmp);

        }
    }

    /// <summary>
    /// получить имя префаба
    /// </summary>
    /// <param name="index">номер нажатой кнопки</param>
    public void GetPrefabName(int index)
    {
        string searchStr = "PrefubNum" + index;
        Text txt = GameObject.Find(searchStr).GetComponent<Text>();
        pathToPrefub = "Prefabs\\" + txt.text;
    }

    /// <summary>
    /// получить значение из InputBox'а
    /// </summary>
    /// <returns></returns>
    int GetPrefabArraySize()
    {
        int inputSize = int.Parse(GameObject.Find("PanelTask4/InputSizeField").GetComponent<InputField>().text);
        return inputSize;
    }
    /// <summary>
    /// обнулить список
    /// </summary>
    void NullPrefabArray()
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            Destroy(prefabList[i]);
        }
        prefabList = new List<GameObject>();
    }

    /// <summary>
    /// сгенерировать массив объектов
    /// </summary>
    public void Generate()
    {
        //
        //if -1 - error
        
        int size = -1;
        
        try
        {
            size = GetPrefabArraySize();
        }
        catch(System.Exception)
        {
            errEngine.SetError("Wrong value");
        }
        
        if (size < 1 || size > 15)
        {
            errEngine.SetError("Set value between 1 and 15");
        }
        else
        {
            if (pathToPrefub == "")
            {
                errEngine.SetError("Prefab have not chosen");
            }
            //если все в порядке
            else
            {
                NullPrefabArray();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            GameObject gmobj = Resources.Load(pathToPrefub) as GameObject;
                            gmobj = Instantiate(gmobj);
                            //      TODO: сделать относительный сдвиг
                            gmobj.transform.Translate(new Vector3((2 * i), (2 * j), (2 * k)));
                            prefabList.Add(gmobj);
                        }                        
                    }
                }
            }
        }

    }
}

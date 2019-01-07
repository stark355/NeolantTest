using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GetFiles : MonoBehaviour {

    string[] fullAdressList; //массив с полными путями к префабам
    List<GameObject> prefabLabelList; //список имен префабов
    [SerializeField]
    Text parentItem; //родительский элемент для имен префабов
    List <string> truncList; //список с именами перфабов (с расширениями)
    List <string> prefabNameList; //список с именами префабов (без расширений)
    List<GameObject> prefabList; //список, содержащий объекты префабов
    ErrorEngine errEngine;
    string pathToPrefab = "";
    int currentPrefId = -1;
    int oldPrefId = -1;


    // Use this for initialization
    void Start () {
        errEngine = ErrorEngine.Instance;
        prefabLabelList = new List<GameObject>();
        truncList = new List<string>();
        prefabNameList = new List<string>();
        prefabList = new List<GameObject>();
        GetListMethod();
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
            tmpName = "PrefabNum" + i;
            GameObject tmp = new GameObject(tmpName);
            tmp.transform.SetParent(gameObject.transform);
            RectTransform trans = tmp.AddComponent<RectTransform>();
            //      TODO: сделать относительный сдвиг
            trans.position = new Vector3(parentItem.transform.position.x, parentItem.transform.position.y - (20 * i), parentItem.transform.position.z);
            //кнопка, на которую повесится клик
            Button btn = tmp.AddComponent<Button>();
            int ind = i;
            //номер кнопки в массиве кнопок (Game Object'ов)
            btn.onClick.AddListener(delegate { OnPrefabNameClick(btn, ind); });
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
    /// <param name="btn">нажатая кнопка</param>
    public void GetPrefabName(Button btn)
    {
        Text txt = btn.GetComponent<Text>();
        pathToPrefab = "Prefabs\\" + txt.text;
    }

    public void OnPrefabNameClick(Button btn, int ind)
    {
        if (currentPrefId == -1)
        {
            currentPrefId = ind;
        }
        else
        {
            oldPrefId = currentPrefId;
            currentPrefId = ind;
        }
        RecolorButtons(oldPrefId, currentPrefId);
        GetPrefabName(btn);
    }
    public void RecolorButtons(int old, int cur)
    {
        if (old >= 0)
        {
            prefabLabelList[old].GetComponent<Text>().color = new Color32(50, 50, 50, 255);
        }
        prefabLabelList[cur].GetComponent<Text>().color = new Color32(20, 130, 20, 255);
    }

    /// <summary>
    /// получить значение из InputBox'а
    /// </summary>
    /// <returns>размерность массива</returns>
    int GetPrefabArraySize()
    {
        int inputSize = int.Parse(GetComponentInChildren<InputField>().text);
        return inputSize;
    }
    /// <summary>
    /// обнулить список
    /// </summary>
    void SetNullPrefabArray()
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            Destroy(prefabList[i]);
        }
        prefabList.Clear();
    }

    /// <summary>
    /// сгенерировать массив объектов
    /// </summary>
    public void Generate()
    {        
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
            if (pathToPrefab == "")
            {
                errEngine.SetError("Prefab have not chosen");
            }
            //если все в порядке
            else
            {
                SetNullPrefabArray();
                GameObject gmobj = Resources.Load(pathToPrefab) as GameObject;
                GameObject realObject;
                //realObject = Instantiate(gmobj);
                //Collider realObjCollider = realObject.GetComponent<Collider>();
                //Vector3 boundsSize = new Vector3(realObjCollider.bounds.size.x, realObjCollider.bounds.size.y, realObjCollider.bounds.size.z);
                //Destroy(realObject);
                Vector3 boundsSize = new Vector3(0, 0, 0);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            realObject = Instantiate(gmobj);
                            if (i == 0 && j == 0 && k == 0)
                            {
                                Collider realObjCollider = realObject.GetComponent<Collider>();
                                boundsSize = new Vector3(realObjCollider.bounds.size.x, realObjCollider.bounds.size.y, realObjCollider.bounds.size.z);
                            }
                            realObject.transform.Translate(new Vector3(boundsSize.x * 2 * i, boundsSize.y * 2 * j, boundsSize.z * 2 * k));
                            //realObject.transform.Translate(new Vector3((2 * i), (2 * j), (2 * k)));
                            prefabList.Add(realObject);
                        }                        
                    }
                }
            }
        }

    }
}

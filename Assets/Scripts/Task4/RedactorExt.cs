using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public class RedactorExt : EditorWindow
{
    [SerializeField]
    int count = 0;
    public string[] prefabNameArr;
    int index = 0;
    static GameObject itemsContainer;


    string[] fullAdressList; //массив с полными путями к префабам
    static List<string> truncList = new List<string>(); //список с именами перфабов (с расширениями)
    static List<string> prefabNameList = new List<string>(); //список с именами префабов (без расширений)
    static List<GameObject> prefabList = new List<GameObject>(); //список, содержащий объекты префабов
    ErrorEngine errEngine;
    string pathToPrefab = "";
    bool isOk = true;
    GUIStyle textStyle;

    [MenuItem("Window/RedactorExt", false, 10)]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        RedactorExt window = (RedactorExt)EditorWindow.GetWindow(typeof(RedactorExt));
        window.Show();
    }

    private void Awake()
    {
        errEngine = ErrorEngine.Instance;        
        GetListMethod();
        textStyle = new GUIStyle(EditorStyles.textField);
        GUI.contentColor = Color.black;
        GUI.color = Color.black;
    }

    private void OnGUI()
    {
        count = EditorGUILayout.IntField("Array size (1 - 15)", count, textStyle);

        index = EditorGUILayout.Popup(index, prefabNameArr);
        if (GUILayout.Button("Generate"))
        {
            Generate();
        }
        if (GUILayout.Button("Refresh"))
        {
            GetListMethod();
        }
    }

    public void GetListMethod()
    {
        try
        {
            prefabNameList.Clear();
            truncList.Clear();
            fullAdressList = null;
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
            prefabNameArr = new string[prefabNameList.Count];
            prefabNameArr = prefabNameList.ToArray();
        }
        catch (System.Exception)
        {
            Debug.Log("Error");
        }
    }

    int GetPrefabArraySize()
    {
        return count;
    }

    void SetNullPrefabArray()
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            DestroyImmediate(prefabList[i]);
        }
        prefabList.Clear();
    }

    void CreateItemsContainer()
    {
        itemsContainer = new GameObject();
        itemsContainer.name = "Items Container";
    }


    public void Generate()
    {
        int size = -1;

        try
        {
            size = GetPrefabArraySize();
        }
        catch (System.Exception)
        {
            errEngine.SetError("Wrong value");
        }

        if (size < 1 || size > 15)
        {

        }
        else
        {
            try
            {
                pathToPrefab = "Prefabs\\" + prefabNameArr[index];
                SetNullPrefabArray();
                if (itemsContainer == null)
                {
                    CreateItemsContainer();
                }
                GameObject gmobj = Resources.Load(pathToPrefab) as GameObject;
                GameObject realObject;
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
                            prefabList.Add(realObject);
                            realObject.gameObject.transform.SetParent(itemsContainer.transform);
                        }
                    }
                }
            }
            catch(System.Exception)
            {
                Debug.Log("Error");
            }

        }

    }
}

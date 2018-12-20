using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedactorLogic : MonoBehaviour {
    public static RedactorLogic Instance { get; private set; }
    public GameObject parentItem;
    static List<GameObject> childList;
    static ErrorEngine errEngine;
    static ObjectLogic objLogic;
    static ParamPanelLogic paramPanelLogic;
    static int objectId;
    int currentId = -1;
    int oldId = -1;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        childList = new List<GameObject>();
        objectId = 0;

        errEngine = ErrorEngine.Instance;
        paramPanelLogic = ParamPanelLogic.Instance;
    }

    // Update is called once per frame
    void Update () {
    }

    public void AddObject()
    {
        if (childList.Count == 10)
        {
            errEngine.SetError("Error.Max limit.");
        }
        else
        {
            //создание нового объекта префаба
            parentItem.SetActive(true);
            GameObject currentChild = Instantiate(parentItem, parentItem.transform.position + new Vector3(3 * (objectId), 0, 0), parentItem.transform.rotation);
            childList.Add(currentChild);
            objLogic = currentChild.GetComponent<ObjectLogic>();
            objLogic.SetID(objectId);
            Debug.Log("Created " + objectId);

            objectId++;
            parentItem.SetActive(false);
        }
    }

    public void DeleteObject()
    {
        if (currentId == -1)
        {
            //ошибка о том, что объект не выбран
            errEngine.SetError("Object not selected");
        }
        else
        {
            //определение текущего объекта
            for (int i = 0; i < childList.Count; i++)
            {
                //работаем с объектом скрипта, а не с объектом префаба
                objLogic = childList[i].GetComponent<ObjectLogic>();
                if (objLogic.GetID() == currentId)
                {
                    Debug.Log("Removed " + currentId);
                    objLogic.Destroyer();
                    childList.Remove(childList[i]);
                    ResetCurrentID();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// если объект не выбран - currentId = -1x
    /// </summary>
    public void MoveObject()
    {
        if (currentId == -1)
        {
            //ошибка о том, что объект не выбран
            errEngine.SetError("Object not selected");
        }
        else
        {
            try
            {
                Vector3 inputParams = paramPanelLogic.GetParameters();
                //получение параметров из inputField
                for (int i = 0; i < childList.Count; i++)
                {
                    //работаем с объектом скрипта, а не с объектом префаба
                    objLogic = childList[i].GetComponent<ObjectLogic>();
                    if (objLogic.GetID() == currentId)
                    {
                        objLogic.MoveObject(inputParams);
                        Debug.Log("Moved " + currentId + " on " + inputParams.x + " " + inputParams.y + " " + inputParams.z);
                        break;
                    }
                }
            }
            catch(System.Exception)
            {
                errEngine.SetError("Wrong values");
            }
        }
    }
    public void RotateObject()
    {
        if (currentId == -1)
        {
            //ошибка о том, что объект не выбран
            errEngine.SetError("Object not selected");
        }
        else
        {
            try
            {
                Vector3 inputParams = paramPanelLogic.GetParameters();
                for (int i = 0; i < childList.Count; i++)
                {
                    objLogic = childList[i].GetComponent<ObjectLogic>();
                    if (objLogic.GetID() == currentId)
                    {
                        objLogic.RotateObject(inputParams);
                        Debug.Log("Rotated " + currentId + " on " + inputParams.x + " " + inputParams.y + " " + inputParams.z);
                        break;
                    }
                }
            }
            catch (System.Exception)
            {
                errEngine.SetError("Wrong values");
            }
        }
    }
    public void ScaleObject()
    {
        if (currentId == -1)
        {
            //ошибка о том, что объект не выбран
            errEngine.SetError("Object not selected");
        }
        else
        {
            try
            {
                Vector3 inputParams = paramPanelLogic.GetParameters();
                for (int i = 0; i < childList.Count; i++)
                {
                    objLogic = childList[i].GetComponent<ObjectLogic>();
                    if (objLogic.GetID() == currentId)
                    {
                        objLogic.ScaleObject(inputParams);
                        Debug.Log("Scaled " + currentId + " on " + inputParams.x + " " + inputParams.y + " " + inputParams.z);
                        break;
                    }
                }
            }
            catch (System.Exception)
            {
                errEngine.SetError("Wrong values");
            }
        }
    }

    /// <summary>
    /// если объект был удален, то "ссылка" на текущий объект сбрасывается
    /// </summary>
    void ResetCurrentID()
    {
        currentId = -1;
    }
    /// <summary>
    /// для определения предыдущего и текущего объекта
    /// </summary>
    /// <param name="i">получение текущего объекта</param>
    public void SetCurrentID(int i)
    {
        oldId = currentId;
        currentId = i;
        Debug.Log("cur " + currentId + " old " + oldId);
        if(currentId != oldId)
        {
            Recolor();
        }
    }
    /// <summary>
    /// перекраска выбранного объекта и выбранного до этого
    /// </summary>
    public void Recolor()
    {
        for (int i = 0; i < childList.Count; i++)
        {
            objLogic = childList[i].GetComponent<ObjectLogic>();
            if (objLogic.GetID() == oldId)
            {
                objLogic.SetChecking(false);
            }
            if (objLogic.GetID() == currentId)
            {
                objLogic.SetChecking(true);
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour {
    //public static ObjectChanger Instance { get; private set; }
    AxisController axisController;
    GameObject attachedInstance;
    GameObject axisContainer;
    ObjectLogic objLogic;
    Collider axisControllerCollider;
    Vector3 centerShift;
    Collider thisCollider;

    public enum CurrentAxis { nullAxis, xAxis, yAxis, zAxis}
    CurrentAxis currentAxis;

    private void Awake()
    {
        //Instance = this;
    }

    // Use this for initialization
    void Start () {
        axisController = AxisController.Instance;
        axisContainer = axisController.GetAxisControllerGameObject();
        thisCollider = gameObject.GetComponent<Collider>();
        if (gameObject.name == "xAxis")
        {
            currentAxis = CurrentAxis.xAxis;
        }
        else if (gameObject.name == "yAxis")
        {
            currentAxis = CurrentAxis.yAxis;
        }
        else if (gameObject.name == "zAxis")
        {
            currentAxis = CurrentAxis.zAxis;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*public void SetCurrentAxis(int curAxis)
    {
        currentAxis = (CurrentAxis)curAxis;
    }*/

    Vector3 savedPosition;
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 curScreenPoint;
    Vector3 curPosition;
    Vector3 transformer;
    void OnMouseDown()
    {
        attachedInstance = axisController.GetAttachedInstance();
        objLogic = attachedInstance.GetComponent<ObjectLogic>();
        axisControllerCollider = axisContainer.GetComponent<Collider>();
        Debug.Log(gameObject.name);
        if (currentAxis == CurrentAxis.xAxis)
        {
            centerShift = new Vector3(thisCollider.bounds.size.x / 2, 0, 0);
        }
        else if (currentAxis == CurrentAxis.yAxis)
        {
            centerShift = new Vector3(0, thisCollider.bounds.size.y / 2, 0);
        }
        else if (currentAxis == CurrentAxis.zAxis)
        {
            centerShift = new Vector3(0, 0, thisCollider.bounds.size.z / 2);
        }

        savedPosition = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {
        curScreenPoint.x = Input.mousePosition.x;
        curScreenPoint.y = Input.mousePosition.y;
        curScreenPoint.z = screenPoint.z;
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        switch (currentAxis)
        {
            case CurrentAxis.xAxis:
                transformer.x = curPosition.x;
                transformer.y = savedPosition.y;
                transformer.z = savedPosition.z;
                break;
            case CurrentAxis.yAxis:
                transformer.x = savedPosition.x;
                transformer.y = curPosition.y;
                transformer.z = savedPosition.z;
                transform.position = transformer;
                break;
            case CurrentAxis.zAxis:
                transformer.x = savedPosition.x;
                transformer.y = savedPosition.y;
                transformer.z = curPosition.z;
                break;
            default:
                break;
        }
        transform.position = transformer;

        objLogic.CopyPosition(gameObject.transform.position - centerShift);

    }

    void OnMouseUp()
    {
        savedPosition = gameObject.transform.position;
    }
}

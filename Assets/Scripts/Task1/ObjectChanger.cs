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
    int flag;

    public enum CurrentAxis { xAxis, yAxis, zAxis}
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
    Vector3 objScale;
    Vector3 scaleVector;

    Vector3 prevPosition;
    bool isToScale = false;

    void OnMouseDown()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            isToScale = false;
        }
        else
        {
            isToScale = true;
        }
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
        objScale = attachedInstance.transform.localScale;
        scaleVector = objScale;
        curScreenPoint.x = Input.mousePosition.x;
        curScreenPoint.y = Input.mousePosition.y;
        curScreenPoint.z = screenPoint.z;
        prevPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    }
    void OnMouseDrag()
    {
        if (!isToScale)
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
        else
        {
            Debug.Log("space");
            curScreenPoint.x = Input.mousePosition.x;
            curScreenPoint.y = Input.mousePosition.y;
            curScreenPoint.z = screenPoint.z;
            scaleVector = attachedInstance.transform.localScale;
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            switch (currentAxis)
            {
                case CurrentAxis.xAxis:
                    scaleVector.x += curPosition.x - prevPosition.x;
                    if (scaleVector.x < 0.1f)
                        scaleVector.x = 0.1f;
                    break;
                case CurrentAxis.yAxis:
                    scaleVector.y += curPosition.y - prevPosition.y;
                    if (scaleVector.y < 0.1f)
                        scaleVector.y = 0.1f;
                    Debug.Log(scaleVector);
                    break;
                case CurrentAxis.zAxis:
                    scaleVector.z += curPosition.z - prevPosition.z;
                    if (scaleVector.z < 0.1f)
                        scaleVector.z = 0.1f;
                    Debug.Log(scaleVector);
                    break;
                default:
                    break;
            }
            //Debug.Log(scaleVector);
            objLogic.CopyScale(scaleVector);
            prevPosition = curPosition;
        }
    }

    void OnMouseUp()
    {
        savedPosition = gameObject.transform.position;
    }
}

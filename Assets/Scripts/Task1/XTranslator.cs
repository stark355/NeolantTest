using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTranslator : MonoBehaviour
{
    AxisController axisController;
    GameObject attachedInstance;
    GameObject axisContainer;
    ObjectLogic objLogic;
    Collider axisControllerCollider;
    Vector3 centerShift;
    // Use this for initialization
    void Start()
    {
        axisController = AxisController.Instance;
        axisContainer = axisController.GetAxisControllerGameObject();
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 savedPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    Vector3 curScreenPoint;
    Vector3 curPosition;
    Vector3 transformer;
    void OnMouseDown()
    {
        attachedInstance = axisController.GetAttachedInstance();
        objLogic = attachedInstance.GetComponent<ObjectLogic>();
        axisControllerCollider = axisContainer.GetComponent<Collider>();
        centerShift = new Vector3(axisControllerCollider.bounds.size.x / 2, 0, 0);
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
        transformer.x = curPosition.x;
        transformer.y = savedPosition.y;
        transformer.z = savedPosition.z;
        transform.position = transformer;

        objLogic.CopyPosition(gameObject.transform.position - centerShift);
    }

    void OnMouseUp()
    {
        savedPosition = gameObject.transform.position;
    }
}
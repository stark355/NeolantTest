using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour {
    AxisController axisController; //скрипт на родительском объекте
    GameObject attachedInstance; //объект, к которому прикреплены направляющие
    ObjectLogic objLogic; //скрипт логики объекта, к которому прикреплены направляющие
    Vector3 centerShift; //смещение относительно центра направляющих
    Collider thisCollider;
    ErrorEngine errEngine;

    public enum CurrentAxis { xAxis, yAxis, zAxis} 
    CurrentAxis currentAxis; //установить значение оси для конкретной направляющей

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        axisController = AxisController.Instance;
        errEngine = ErrorEngine.Instance;
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

    Vector3 savedPosition; //позиция объекта, которому прикреплены направляющие на предыдущем шаге вычислений

    Vector3 screenPoint; //позиция курсора в единицах мира
    Vector3 offset; //смещение позиции курсора относительно центра конретной направляющей
    Vector3 curScreenPoint; ////позиция курсора в пикселах

    Vector3 curPosition; //позиция курсора в единицах мира с учетом смещения
    Vector3 prevPosition; //позиция курсора в единицах мира с учетом смещения на предыдущем шаге вычислений

    enum CurrentMode { none, move, scale, rotate}
    CurrentMode currentMode = CurrentMode.none; //режим работы при нажатии на направляющую

    Vector3 transformer; //вектор, на который необходимо передвинуть объект, которому прикреплены направляющие
    Vector3 scaleVector; //вектор, который необходимо установить в скалирование объекта, которому прикреплены направляющие
    Vector3 rotateVector; //вектор, на который необходимо повернуть объект, которому прикреплены направляющие

    void OnMouseDown()
    {
        //если зажат "пробел", то объект будет масштабироваться
        if (Input.GetKey(KeyCode.Space))
        {
            currentMode = CurrentMode.scale;
        }
        //если зажать "левый Ctrl", то объект будет вращаться
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentMode = CurrentMode.rotate;
        }
        //если не зажато ничего, то объект будет перемещаться
        else
        {
            currentMode = CurrentMode.move;
        }
        try
        {
            attachedInstance = axisController.GetAttachedInstance();
            objLogic = attachedInstance.GetComponent<ObjectLogic>();
        }
        catch (System.Exception e)
        {
            //errEngine.SetError(e.Message);
            errEngine.SetError("Error. There is no attached object");
        }

        Debug.Log(gameObject.name);
        //для каждой оси смещение относительно центра родительского элемента рассчитывается отдельно
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
        curScreenPoint.x = Input.mousePosition.x;
        curScreenPoint.y = Input.mousePosition.y;
        curScreenPoint.z = screenPoint.z;
        prevPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        rotateVector = attachedInstance.transform.eulerAngles;
    }
    void OnMouseDrag()
    {
        curScreenPoint.x = Input.mousePosition.x;
        curScreenPoint.y = Input.mousePosition.y;
        curScreenPoint.z = screenPoint.z;
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if (currentMode == CurrentMode.move)
        {
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
        else if (currentMode == CurrentMode.scale)
        {
            scaleVector = attachedInstance.transform.localScale;
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
                    break;
                case CurrentAxis.zAxis:
                    scaleVector.z += curPosition.z - prevPosition.z;
                    if (scaleVector.z < 0.1f)
                        scaleVector.z = 0.1f;
                    break;
                default:
                    break;
            }
            //Debug.Log(scaleVector);
            objLogic.CopyScale(scaleVector);
            prevPosition = curPosition;
        }
        else if (currentMode == CurrentMode.rotate)
        {
            rotateVector = Vector3.zero;
            switch (currentAxis)
            {
                case CurrentAxis.xAxis:
                    //двигаем мышь вверх
                    rotateVector.x = (curPosition.y - prevPosition.y) * 10;
                    break;
                case CurrentAxis.yAxis:
                    //двигаем мышь вбок
                    rotateVector.y = -(curPosition.x - prevPosition.x) * 10;
                    break;
                case CurrentAxis.zAxis:
                    //двигаем мышь вверх
                    rotateVector.z = (curPosition.y - prevPosition.y) * 10;
                    break;
                default:
                    break;
            }
            //Debug.Log(rotateVector);
            objLogic.RotateObject(rotateVector);
            prevPosition = curPosition;
        }
    }

    void OnMouseUp()
    {
        savedPosition = gameObject.transform.position;
    }
}

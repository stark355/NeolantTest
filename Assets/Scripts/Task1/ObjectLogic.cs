using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour {

    static RedactorLogic redLog;
    static AxisController axisController;
    int objectId;
    ErrorEngine errEngine;
    // Use this for initialization
    void Start () {
        redLog = RedactorLogic.Instance;
        axisController = AxisController.Instance;
        errEngine = ErrorEngine.Instance;
	}
	// Update is called once per frame
	void Update () {
    }

    private void OnMouseDown()
    {
        redLog.SetCurrentID(objectId);
        Debug.Log("Clicked " + objectId);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
        axisController.SetAttachedInstance(null);
    }
    public void MoveObject(Vector3 v3)
    {
        transform.Translate(v3);
        axisController.Redraw();
    }
    public void RotateObject(Vector3 v3)
    {
        transform.Rotate(v3);
        axisController.Redraw();
    }
    public void ScaleObject(Vector3 v3)
    {
        //абсолютные значения
        //transform.localScale += v3;

        //классический Scaling, отсносительно текущего размера
        if ((v3.x > 0.1f || v3.x < -0.1f) && (v3.y > 0.1f || v3.y < -0.1f) && (v3.z > 0.1f || v3.z < -0.1f))
        {
            transform.localScale = Vector3.Scale(transform.localScale, v3);
            axisController.Redraw();
        }
        else
        {
            errEngine.SetError("Please do not scale object less than Abs(0.1)");
        }
    }
    public void CopyPosition(Vector3 toCloneCoords)
    {
        gameObject.transform.position = toCloneCoords;
        axisController.Redraw();
    }
    public void CopyScale(Vector3 toCloneScale)
    {
        gameObject.transform.localScale = toCloneScale;
        axisController.Redraw();
    }

    public int GetID()
    {
        return objectId;
    }
    public void SetID(int i)
    {
        objectId = i;
    }
    //установить данный объект текущим объектом сцены
    public void SetChecking(bool isChecked)
    {
        //Debug.Log("clicked");
        if(isChecked == true)
        {
            transform.GetComponent<Renderer>().material.color = new Color(0, 128, 129);
            axisController.SetAttachedInstance(gameObject);
        }
        else
        {
            transform.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }   
    }
}

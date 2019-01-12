using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour {

    static RedactorLogic redLog;
    static AxisController axisController;
    int objectId;
    // Use this for initialization
    void Start () {
        redLog = RedactorLogic.Instance;
        axisController = AxisController.Instance;

	}
	// Update is called once per frame
	void Update () {
    }

    private void OnMouseDown()
    {
        redLog.SetCurrentID(objectId);
        Debug.Log("Clicked " + objectId);
    }

    public void Destroyer()
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
        transform.localScale = Vector3.Scale(transform.localScale, v3);
        axisController.Redraw();
    }
    public void CopyPosition(Vector3 toCloneCoords)
    {
        gameObject.transform.position = toCloneCoords;
        axisController.Redraw();
    }
    public void CopyScale(Vector3 toCloneSlale)
    {
        gameObject.transform.localScale = toCloneSlale;
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

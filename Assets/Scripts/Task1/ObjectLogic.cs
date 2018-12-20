using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour {

    static RedactorLogic redLog;
    int objectId;
    // Use this for initialization
    void Start () {
        redLog = RedactorLogic.Instance;
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
        Destroy(this.gameObject);
    }
    public void MoveObject(Vector3 v3)
    {
        this.transform.Translate(v3);
    }
    public void RotateObject(Vector3 v3)
    {
        this.transform.Rotate(v3);
    }
    public void ScaleObject(Vector3 v3)
    {
        this.transform.localScale += v3;
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
        if(isChecked == true)
            transform.GetComponent<Renderer>().material.color = new Color(0, 128, 129);
        else
            transform.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

}

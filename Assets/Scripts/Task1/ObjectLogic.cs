using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour {

    static RedactorLogic redLog;
    int objectId;
    // Use this for initialization
    void Start () {
        redLog = GameObject.Find("GUIPanel").GetComponent<RedactorLogic>();
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
    public void MoveObject(float x, float y, float z)
    {
        this.transform.Translate(new Vector3(x, y, z));
    }
    public void RotateObject(float x, float y, float z)
    {
        this.transform.Rotate(new Vector3(x, y, z));
    }
    public void ScaleObject(float x, float y, float z)
    {
        this.transform.localScale += new Vector3(x, y, z);
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

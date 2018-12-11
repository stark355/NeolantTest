using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour {

    float changeSpeed = 1.0f;
    float startTime;
    bool flag = false;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            flag = false;
            startTime = Time.time;
        }
        if (Input.GetMouseButtonDown(1))
        {
            flag = true;
            startTime = Time.time;
        }
        if (flag == false)
        {
            transform.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, changeSpeed * (Time.time - startTime));
        }
        else
        {
            transform.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, changeSpeed * (Time.time - startTime));

        }
    }
    
}

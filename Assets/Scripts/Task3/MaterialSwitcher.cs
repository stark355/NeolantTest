﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour {

    float changeSpeed = 1.0f;
    float startTime;
    bool flag = false;
    Renderer rend;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        rend = transform.GetComponent<Renderer>();
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
            rend.material.color = Color.Lerp(Color.black, Color.white, changeSpeed * (Time.time - startTime));
        }
        else
        {
            rend.material.color = Color.Lerp(Color.white, Color.black, changeSpeed * (Time.time - startTime));
        }
    }
    
}

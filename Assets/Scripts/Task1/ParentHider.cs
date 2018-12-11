using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHider : MonoBehaviour {

    public GameObject mycube;
	// Use this for initialization
	void Start () {
        mycube.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

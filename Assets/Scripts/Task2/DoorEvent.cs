using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour {

    public bool isOpen;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GameObject.Find("DoorRotator").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen);
        anim.SetBool("isOpen", isOpen);
    }
}

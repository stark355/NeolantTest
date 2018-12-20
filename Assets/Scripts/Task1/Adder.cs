using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adder : MonoBehaviour {
    public GameObject parentItem;
    List<GameObject> childList;
    ErrorEngine errEngine;

    // Use this for initialization
    void Start () {
        errEngine = ErrorEngine.Instance;
        childList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void Add()
    {
        if (childList.Count == 10)
        {
            errEngine.SetError("Error.Max limit.");
        }
        else
        {
            parentItem.SetActive(true);
            childList.Add(Instantiate(parentItem, parentItem.transform.position + new Vector3(3 * (childList.Count + 1), 0, 0), parentItem.transform.rotation));
            parentItem.SetActive(false);
        }
    }

}

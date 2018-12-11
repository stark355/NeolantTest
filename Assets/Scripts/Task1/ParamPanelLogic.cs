using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class ParamPanelLogic : MonoBehaviour {

    RectTransform paramPanel;
    InputField inputX;
    InputField inputY;
    InputField inputZ;

    //float x, y, z;

    // Use this for initialization
    void Start () {
        paramPanel = GameObject.Find("ParametersPanel").GetComponent<RectTransform>();
        inputX = GameObject.Find("ParametersPanel/InputX").GetComponent<InputField>();
        inputY = GameObject.Find("ParametersPanel/InputY").GetComponent<InputField>();
        inputZ = GameObject.Find("ParametersPanel/InputX").GetComponent<InputField>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void GetParameters(out float x, out float y, out float z)
    {
        x = float.Parse(inputX.text, CultureInfo.InvariantCulture);
        y = float.Parse(inputY.text, CultureInfo.InvariantCulture);
        z = float.Parse(inputZ.text, CultureInfo.InvariantCulture);
    }
}

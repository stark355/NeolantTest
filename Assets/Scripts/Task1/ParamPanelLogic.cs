using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class ParamPanelLogic : MonoBehaviour {

    public static ParamPanelLogic Instance { get; private set; }
    Vector3 parameters;
    InputField[] inputFields;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        parameters = new Vector3();
        inputFields = GetComponentsInChildren<InputField>();
    }

    // Update is called once per frame
    void Update () {
	}

    public Vector3 GetParameters()
    {
        parameters.x = float.Parse(inputFields[0].text, CultureInfo.InvariantCulture);
        parameters.y = float.Parse(inputFields[1].text, CultureInfo.InvariantCulture);
        parameters.z = float.Parse(inputFields[2].text, CultureInfo.InvariantCulture);
        return parameters;
    }
}

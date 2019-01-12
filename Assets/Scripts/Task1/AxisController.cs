using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour {
    public static AxisController Instance { get; private set; }
    static GameObject attachedInstance;
    Collider attachedInstanceCollider;
    Collider xyzAxisCollider;
    MeshRenderer[] render;
    [SerializeField]
    ObjectChanger[] objectChangers;
    Collider xCollider;
    ObjectChanger objChanger;
    //Convert Layer Name to Layer Number
    int xyzAxisLayer;
    [SerializeField]
    float xyzScaleDivider;
    //float advScale;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        xyzAxisCollider = gameObject.GetComponent<Collider>();
        xCollider = objectChangers[0].GetComponent<Collider>();
        render = GetComponentsInChildren<MeshRenderer>();
        HideElements();
        xyzAxisLayer = LayerMask.NameToLayer("XYZAxisChilds");
    }
	
	// Update is called once per frame
	void Update () {
        }
    void HideElements()
    {
        for (int i = 0; i < render.Length; i++)
        {
            render[i].enabled = false;
        }
    }
    void ShowElements()
    {
        for (int i = 0; i < render.Length; i++)
        {
            render[i].enabled = true;
        }
    }
    public void SetAttachedInstance(GameObject attachedCube)
    {
        attachedInstance = attachedCube;
        if (attachedInstance == null)
        {
            HideElements();
        }
        else
        {
            ShowElements();
        }
        DrawAxisController();
    }
    void DrawAxisController()
    {
        if(attachedInstance != null)
        {
            CalcScaling();
            CalcPositon();
        }
    }
    void CalcPositon()
    {
        /*
        if (gameObject.transform.localScale.x >= 0 )
        {
            gameObject.transform.position = new Vector3(attachedInstance.transform.position.x + (xCollider.bounds.size.x / 2), attachedInstance.transform.position.y, attachedInstance.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(attachedInstance.transform.position.x - (xCollider.bounds.size.x / 2), attachedInstance.transform.position.y, attachedInstance.transform.position.z);
        }
        */
        gameObject.transform.position = new Vector3(attachedInstance.transform.position.x + (xCollider.bounds.size.x / 2), attachedInstance.transform.position.y, attachedInstance.transform.position.z);

    }
    void CalcScaling()
    {
        gameObject.transform.localScale = attachedInstance.transform.localScale / xyzScaleDivider;
        //advScale = (attachedInstance.transform.localScale.x / 3) + (attachedInstance.transform.localScale.y / 3) + (attachedInstance.transform.localScale.z / 3);
        //gameObject.transform.localScale = new Vector3(attachedInstance.transform.localScale.x / advScale, attachedInstance.transform.localScale.y / advScale, attachedInstance.transform.localScale.z / advScale);
    }
    public void Redraw()
    {
        DrawAxisController();
    }
    public GameObject GetAttachedInstance()
    {
        return attachedInstance;
    }
    public GameObject GetAxisControllerGameObject()
    {
        return gameObject;
    }
}

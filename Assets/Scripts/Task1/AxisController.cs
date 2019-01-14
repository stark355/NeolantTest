using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour {
    public static AxisController Instance { get; private set; }
    static GameObject attachedInstance; //объект, к которому прикреплены направляющие
    MeshRenderer[] render; //массив MeshRender'ов детей родительского объекта для всех направляющих
    [SerializeField]
    ObjectChanger[] objectChangers; //массив ObjectChanger'ов, помещенных на объекты направляющих
    Collider xCollider; //коллайдер xAxis. нужен для расчета смещения напрявляющих относительно объекта
    //int xyzAxisLayer;
    [SerializeField]
    float xyzScaleDivider; //размер напрявляющих относительно размера объекта


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        xCollider = objectChangers[0].GetComponent<Collider>();
        render = GetComponentsInChildren<MeshRenderer>();
        HideElements();
        //xyzAxisLayer = LayerMask.NameToLayer("XYZAxisChilds");
    }
	
	// Update is called once per frame
	void Update () {
        }

    /// <summary>
    /// скрыть направляющие
    /// </summary>
    void HideElements()
    {
        for (int i = 0; i < render.Length; i++)
        {
            render[i].enabled = false;
        }
    }
    /// <summary>
    /// отобразить направляющие
    /// </summary>
    void ShowElements()
    {
        for (int i = 0; i < render.Length; i++)
        {
            render[i].enabled = true;
        }
    }
    /// <summary>
    /// установить объект, к которому прикреплены направляющие
    /// </summary>
    /// <param name="attachedCube">объект, к которому прикреплены направляющие. если null - объект удален</param>
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
    /// <summary>
    /// отрисовка направляющих в нужном месте и с нужным увеличением
    /// </summary>
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

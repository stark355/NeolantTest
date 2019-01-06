using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ErrorEngine : MonoBehaviour
{
    public static ErrorEngine Instance { get; private set; }
    Text errorText;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        errorText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideErrorText()
    {
        errorText.text = "";
        errorText.rectTransform.sizeDelta = new Vector2(0, 0);
    }

    public IEnumerator ShowErrorText()
    {
        errorText.rectTransform.sizeDelta = new Vector2(200, 30);
        yield return new WaitForSeconds(5f);
        HideErrorText();
    }

    public void SetError(string s)
    {
        StartCoroutine(ShowErrorText());
        errorText.text = s;
    }
}

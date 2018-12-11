using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ErrorEngine : MonoBehaviour
{

    Text errorText;
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

    async public void ShowErrorText()
    {
        errorText.rectTransform.sizeDelta = new Vector2(160, 30);
        await Task.Delay(5000);
        HideErrorText();
    }

    public void SetError(string s)
    {
        ShowErrorText();
        errorText.text = s;
    }
}

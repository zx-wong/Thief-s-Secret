using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintText : MonoBehaviour
{
    public static bool textOn = false;
    public static string message;
    TextMeshProUGUI Hint;

    // Start is called before the first frame update
    void Start()
    {
        Hint = GetComponentInChildren<TextMeshProUGUI>();
        Hint.text = "";
        textOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textOn)
        {
            Hint.enabled = true;
            Hint.text = message;
        }
        else
        {
            Hint.enabled = false;
            Hint.text = message;
        }
            
    }
}

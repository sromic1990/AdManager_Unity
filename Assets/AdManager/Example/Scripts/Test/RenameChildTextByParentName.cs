using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenameChildTextByParentName : MonoBehaviour 
{
    private void Awake()
    {
        string name = gameObject.name;
        Text childText = transform.Find("Text").GetComponent<Text>();
        childText.text = name;
    }
}


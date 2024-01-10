using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;
public class SelectCar : MonoBehaviour
{
    TMPro.TMP_Dropdown dropdown;
    
    public void Start()
    {
        TMPro.TMP_Dropdown[] ddList = GetComponentsInChildren<TMPro.TMP_Dropdown>();
        dropdown = ddList[0];        
    }
    public void carSelection()
    {
        PlayerPrefs.SetInt("carSelected", dropdown.value + 1);
    }
}

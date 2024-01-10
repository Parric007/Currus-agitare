using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;
public class SelectCar : MonoBehaviour
{
    public GameObject dropDownGo;
    TMPro.TMP_Dropdown[] dropdown;
    
    public void Start()
    {
        TMPro.TMP_Dropdown[] dropdown = GetComponentsInChildren<TMPro.TMP_Dropdown>();
        //dropdown = ddList[0];
        Debug.Log(dropdown);
    }
    public void carAuswahl(int drop)
    {
        //PlayerPrefs.SetInt("carSelected", drop + 1);
        //PlayerPrefs.SetInt("carSelected", ddList.);
        //carOption = drop;
    }
}

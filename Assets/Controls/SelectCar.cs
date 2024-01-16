using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCar : MonoBehaviour
{
    TMPro.TMP_Dropdown dropdown;
    public TMPro.TMP_InputField inputField;
    
    public void Start()
    {
        try{
            TMPro.TMP_Dropdown[] ddList = GetComponentsInChildren<TMPro.TMP_Dropdown>();
            dropdown = ddList[0];  
        } catch {
            return;
        }
              
    }
    public void carSelection()
    {
        PlayerPrefs.SetInt("carSelected", dropdown.value + 1);
    }
    public void selectRounds() {
        PlayerPrefs.SetInt("rounds", int.Parse(inputField.text));
    }
}

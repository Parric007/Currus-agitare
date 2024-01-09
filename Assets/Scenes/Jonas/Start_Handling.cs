using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class Start_Handling : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Optionen;
    // Start is called before the first frame update
    void Start() {
        Optionen.SetActive(false);
        Menu.SetActive(true);
    }
    

    public void PlayStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OptionenButton()
    {
        Menu.SetActive(false);
        Optionen.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

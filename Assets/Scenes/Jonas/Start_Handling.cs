using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class Start_Handling : MonoBehaviour
{
    public GameObject Start_Menu;
    public GameObject Option_Menu;
    // Start is called before the first frame update
    void Start()
    {
        //PlayStartButton();
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
        Start_Menu.SetActive(false);
        Option_Menu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

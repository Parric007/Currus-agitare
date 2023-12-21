using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Handling : MonoBehaviour
{
    public GameObject Start_Menu;
    public GameObject Option_Menu;
    // Start is called before the first frame update
    void Start()
    {
        PlayStartButton();
    }

    public void PlayStartButton()
    {
        SceneManager.LoadScene("Sample_Scene");
    }

    public void QuitButton()
    {
        Application.Quit();
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

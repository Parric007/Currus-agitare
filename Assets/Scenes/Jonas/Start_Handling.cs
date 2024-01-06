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
<<<<<<< HEAD:Assets/Scenes/Jonas/Start_Handling.cs
=======
        //SceneManager.LoadScene(Sample_Scene);
>>>>>>> af74ae9839c3f4b1bc0c415be1a2f811bf87d75c:Assets/Scenes/Start_Handling.cs
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

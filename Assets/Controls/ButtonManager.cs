using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject Menu;
    public TMP_Text ButtonText;
    public TMP_Text steuerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            if(Menu.activeInHierarchy) {
                Time.timeScale = 1;
                Menu.SetActive(false);
                ButtonText.gameObject.SetActive(true);
                steuerText.gameObject.SetActive(true);
            }
        }
    }

    public void PlayButton()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        Application.Quit(); 
    }

    public void OptionButton()
    {
        SceneManager.LoadScene("Start_Menu");
    }
}

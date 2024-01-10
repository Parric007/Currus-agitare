using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Audio_Managment : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musikpegel"))
        {
            PlayerPrefs.SetFloat("musikpegel", 1);
        }
    }

    // Update is called once per frame
    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }

    private void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("musikpegel");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musikpegel", VolumeSlider.value);
    }



}

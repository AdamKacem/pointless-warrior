using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class MenuButtons : MonoBehaviour
{
    public GameObject MobileButtons;


    public AudioSource Audio;
    public GameObject Menu;
    public GameObject HUD;
    public TextMeshProUGUI text;
    public bool soundOn=true;
    public void Play()
    {
        
        if (soundOn)
        {
            Audio.Play();
            
        }
            
        Menu.SetActive(false);
        HUD.SetActive(true);
    }

    public void Sound()
    {
        
        soundOn = !soundOn;
       if (soundOn)
        {
            text.text = "Sound On";
        }
        else
        {
            text.text = "Sound Off";
        }
    }
}

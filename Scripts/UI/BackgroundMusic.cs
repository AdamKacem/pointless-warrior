using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public MenuButtons MenuButtons;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        if(MenuButtons.soundOn)
            Audio.Play();
    }

 
}

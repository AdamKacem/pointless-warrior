using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RestartScript : MonoBehaviour
{
    public MageScript mageScript; 
    public TextMeshProUGUI restartText;
 
    void Start()
    {
        if (restartText != null)
        {
            restartText.gameObject.SetActive(false);
        }
    }

  
    void Update()
    {
        if (mageScript != null && mageScript.dead)
        {
            
            if (restartText != null && !restartText.gameObject.activeSelf)
            {
                restartText.gameObject.SetActive(true);
            }

           
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartScene();
            }
        }
    }
    private void RestartScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

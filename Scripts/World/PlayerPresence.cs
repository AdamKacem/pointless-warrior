using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresence : MonoBehaviour
{
    public IsPresent IsPresent; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            IsPresent.value = !IsPresent.value;
           
        }
    }
}

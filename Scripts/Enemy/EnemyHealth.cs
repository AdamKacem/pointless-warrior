using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;
    


    void Update()
    {
        //keep health bar updated    
        transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }

    public void TakeDamage(float damage)
    {

        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}

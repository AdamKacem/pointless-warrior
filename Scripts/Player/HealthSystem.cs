using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Health HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle1")) 
        {
            HealthBar.TakeDamage(10);
           
        }

       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthBonus") && HealthBar.CurrentHealth < 100)
        {
            HealthBar.Heal(10);

        }
        if (collision.gameObject.CompareTag("FullHealth") && HealthBar.CurrentHealth < 100)
        {
            HealthBar.Heal(100);

        }
    }
}

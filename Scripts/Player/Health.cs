using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float HealthMax=100;
    public float CurrentHealth=100;
    private Vector3 velocity = Vector3.zero;
    private Vector3 TargetScale;
    // Start is called before the first frame update
    
    public void TakeDamage(float _damage) 
    
    { 
        CurrentHealth -= _damage;
        if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        transform.localScale = new Vector3(CurrentHealth / HealthMax, 1, 1);
        
    }

    public void Heal(float _heal)
    {
        CurrentHealth += _heal;
        if (CurrentHealth > HealthMax)
        {
            CurrentHealth = HealthMax;
        }
        transform.localScale = new Vector3(CurrentHealth / HealthMax, 1, 1);
    }

}

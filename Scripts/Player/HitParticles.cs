using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour
{
    public ParticleSystem Psystem;
    private List<string> validTags;
    // Start is called before the first frame update
    void Start()
    {
        if (Psystem == null) { 
        Psystem = GetComponent<ParticleSystem>();
        }
        validTags = new List<string> { "slideAttack", "swingAttack" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (validTags.Contains(collision.gameObject.tag))
        {
            Psystem.Play();
            Debug.Log("yes");
        }
        
    }
}

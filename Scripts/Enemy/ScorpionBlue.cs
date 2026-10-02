using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionBlue : MonoBehaviour
{


    public EnemyHealth healthBar;
    public GameObject healthBarBackGround;


    public MageScript Mage;
    public Rigidbody2D body;
    private Animator anim;

    public BoxCollider2D collider;

    public float maxDistance;
    private bool SeeingPlayer = false;
    public float speed;
    private bool dead = false;
    
    private bool onGround = false;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        
        //checking for death
        if (healthBar.currentHealth == 0)
        {
            Death();
            if (onGround) {
               
                body.bodyType = RigidbodyType2D.Kinematic;
                collider.enabled = false;
                body.velocity = Vector2.zero;
            }
           
        }

        if (SeeingPlayer && !Mage.dead && !dead)
        {
            anim.SetBool("moving", true);
            MoveToPlayer();
        }

        else
        {
            anim.SetBool("moving", false);

        }
        //keep the health bar on the scorpion
        healthBar.transform.position = body.transform.position;
        healthBarBackGround.transform.position = body.transform.position;

    }


    private void MoveToPlayer()

    {

        
        Vector3 direction = (Mage.transform.position - transform.position).normalized;
        if (Mage.transform.position.x > transform.position.x) {

            
           
            transform.localScale = new Vector3(-7.23f,transform.localScale.y,transform.localScale.z);
            
        }
        else
        {
           
            
            transform.localScale = new Vector3(7.23f, transform.localScale.y, transform.localScale.z);
          

        }
        
        transform.position += direction * speed * Time.deltaTime;
    }



    private void FixedUpdate()
    {
        
        Vector3 direction = Mage.transform.position - transform.position;

        
        direction.y = 0;
        direction = direction.normalized; 

        // Perform the raycast
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction,maxDistance);
        Debug.DrawRay(transform.position, direction, Color.red); // Always draw the ray

        // Debug the ray and check the hit
        if (ray.collider != null)
        {
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red); // Red ray if it hits something
            SeeingPlayer = ray.collider.CompareTag("Player");
            if (SeeingPlayer)
            {
                Debug.DrawRay(transform.position, direction * maxDistance, Color.green);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red); // Green ray if nothing is hit
            SeeingPlayer = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("attack"))
        {
            if (collision.transform.position.x < transform.position.x)
                body.velocity = new Vector2(2, body.velocity.y);
            else body.velocity = new Vector2(-2, body.velocity.y);
            //anim.SetBool("dead",true);
            healthBar.TakeDamage(20 * Mage.strength);
        }

         if (collision.gameObject.CompareTag("swingAttack"))
        {
            
            if (collision.transform.position.x < transform.position.x)
                body.velocity = new Vector2(3, 1);
            else body.velocity = new Vector2(-3, 1);
            healthBar.TakeDamage(35 * Mage.strength);
        }

        if (collision.gameObject.CompareTag("slideAttack"))
        {
            
            if (collision.transform.position.x < transform.position.x)
                body.velocity = new Vector2(8, 4);
            else body.velocity = new Vector2(-8, 4);
            healthBar.TakeDamage(35 * Mage.strength);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
          
        if (collision.gameObject.CompareTag("Ground")) {
            Vector2 collDirection = collision.transform.position - transform.position;
            
            if (collDirection.y < -0.8f)
            {
                
                onGround = true;
            }
        
        }
            

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;

    }

    private void Death()
    {
        anim.SetBool("dead", true);
        healthBarBackGround.SetActive(false);
        dead = true;
        this.tag = "Untagged";
        
        
    }


}

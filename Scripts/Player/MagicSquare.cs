using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSquare : MonoBehaviour
{
    public float speed;
    private float direction;
    private bool hit;
    private Animator anim;
    private float lifeTime=0;
    public BoxCollider2D boxCollider;
    public float WaitBeforeDeath;
    private SpriteRenderer sprite;

    void OnEnable()
    {
        // Initialize components when the object is enabled
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        
        //boxCollider is not null here!
    }

    
    void Update()
    {

        if (hit)return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > 3.5f) gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        { 
        
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        }

    }
    public void SetDirection(float _direction)
    {
         direction = _direction;
        lifeTime = 0;
        
         gameObject.SetActive(true);
        
         
            
        
        
         hit = false;
        
       
       
         boxCollider.enabled = true;
        sprite.enabled = true;
         
         //flip the sprite
         if (direction < 0)
             sprite.flipX = true;

         else
             sprite.flipX = false; 
         /**/
        


    }
    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.1f); 
        gameObject.SetActive(false);          
    }


}

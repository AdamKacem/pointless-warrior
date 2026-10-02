using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MageScript : MonoBehaviour
{
    private float mvtDirection;
    private int jumpCounter = 0;
    public int upStrength = 0;
    public int upStrengthWall = 0;
    public int speed = 0;
    public float wallSlideSpeed = 9.5f;
    private bool jumpingFromWall = false;
    public Rigidbody2D myBody;
    public SpriteRenderer sprite;
    private Animator anim;
    private bool onWall = false;
    private bool onGround = false;
    public BoxCollider2D boxCollider;
    public float horizontalInput;
    //sliding 
    public bool isSliding=false;
    private float slidingTimer;
    
    public float slideSpeed = 9.5f;

    //taking damage
    private bool knocked = false;
    private float knockedTimer = 0;
    //dying
    public Health healthBar;
    public bool dead = false;

    public bool canMove = true;

    //swing slide fix
    public SwingAttack SwingAttack;
    //mobile
    public bool buttonsPressed;
    public bool buttonDash;
    public bool buttonJump=false;

    public float strength = 1;
    void Start()
    {
        //control fps
        Application.targetFrameRate =144;
        QualitySettings.vSyncCount = 0;



        boxCollider = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        canMove = true;
    }


    void Update()
    {


        if (healthBar.CurrentHealth<=0)
        {
            dead = true;
            anim.SetBool("dead",true);
        }

        if (!canMove)
        {
           
            return;
        }
            

        if (!dead)
        {
            //test
            

            // -------------------------------------------------------------------------movement


            //horizontal movement
            if (!buttonsPressed)
            horizontalInput = Input.GetAxis("Horizontal");
            if (!isSliding && !knocked)
            {
                myBody.velocity = new Vector2(horizontalInput * speed, myBody.velocity.y);
                anim.SetBool("run", (horizontalInput != 0) && onGround );

            }


            //flipping the sprite
            if (!onWall && !isSliding && !knocked)
            {
                ;
                //flipping the sprite according to the horizontal input, but only if not on wall
                //5ater sliding on wall changes the flipping of the sprite logic: we will deal with in the collision function

                if (horizontalInput > 0)
                {
                    transform.localScale = new Vector3(4.5f, transform.localScale.y, transform.localScale.z);
                }
                else if (horizontalInput < 0)
                {
                    transform.localScale = new Vector3(-4.5f, transform.localScale.y, transform.localScale.z);
                }

            }

           



            //sliding

            if ((Input.GetKeyDown(KeyCode.LeftShift)||buttonDash) && slidingTimer < 0.6 && !onWall)
            {
                slidingTimer = 0;
                disableAllAnimations();
                mvtDirection = Mathf.Sign(horizontalInput);
                anim.SetBool("isSliding", true);
                isSliding = true;
                myBody.velocity = new Vector2(mvtDirection * slideSpeed, myBody.velocity.y);
                buttonDash = false;

            }

            if (isSliding)
            {


                slidingTimer += Time.deltaTime;
                
                if (SwingAttack.SlideSwinging)
                {
                    isSliding = false;
                    
                    return;
                }
                if (slidingTimer > 0.6)
                {

                    isSliding = false;
                    
                    slidingTimer = 0;
                    anim.SetBool("isSliding", false);
                }

            }



            //knocked by damage
            if (knocked)
            {


                anim.SetBool("knocked", true);
                if (knockedTimer < 0.35f)
                {
                    knockedTimer += Time.deltaTime;

                }
                else
                {
                    knockedTimer = 0;
                    knocked = false;
                    anim.SetBool("knocked", false);
                }

            }



            //jumping
            if ((Input.GetKeyDown(KeyCode.Space)||buttonJump) && jumpCounter < 1 && !isSliding)
            {
                buttonJump = false;
                anim.SetBool("jump", true);
                jumpCounter++;

                //jump strength depends on whether player is on wall

                if (!onWall)
                {
                    myBody.velocity = Vector2.up * upStrength;

                }
                else
                {
                    myBody.velocity = Vector2.up * upStrengthWall;
                    onWall = false;
                    jumpingFromWall = true; //to remove sliding from wall effect so you can jump wihtout directly falling


                }


            }






        }
    }

    //collision with ground and wall
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Ground"))
        {


            
            onGround = true;
            onWall = false;
            anim.SetBool("onWall", false);
            anim.SetBool("jump", false);
            jumpCounter = 0;
            jumpingFromWall = false;

        }

        else if (collision.gameObject.CompareTag("Wall"))
        {


            anim.SetBool("jump", false);
            
            jumpCounter = 0;
            
        }

        //taking damage animation and knock back effect (decreasing health is managed in HealthSystem.cs)
        if (collision.gameObject.CompareTag("Obstacle1"))
        {

            disableAllAnimations();
            knockedTimer = 0;
           
            mvtDirection = Mathf.Sign(horizontalInput);
            knocked=true;
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            if(collision.transform.position.x < transform.position.x)
            {
                myBody.velocity = new Vector2(7.5f * mvtDirection, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(-7.5f * mvtDirection, myBody.velocity.y);
            }
            
        }
    }

    //sliding on walls
    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {

            

            onGround = true;
            onWall = false;
            anim.SetBool("onWall", false);
            anim.SetBool("jump", false);
            jumpCounter = 0;
            jumpingFromWall = false;

        }
        //play on wall animation if in contact with a wall from the right or the left
        if (collision.gameObject.CompareTag("Wall") && !onGround)
        {
            

            foreach (ContactPoint2D contact in collision.contacts)

            {
                Vector2 contactNormal = contact.normal;
                
                if (Mathf.Abs(contactNormal.x) > 0.9f)
                {

                      
                    anim.SetBool("onWall", true);
                    onWall = true;
                    if (myBody.velocity.y <= 0) myBody.velocity = new Vector2(myBody.velocity.x, -wallSlideSpeed); //sliding down effect, only when not moving up so a wall jump is never cancelled
                    
                    else
                    {
                        //jumpingFromWall = false;
                    }
                    
                   
                }
               
                

            }


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {

            jumpingFromWall = false;
            anim.SetBool("onWall", false);
            onWall= false;
            
        }
        if (collision.gameObject.CompareTag("Ground"))
        {

            onGround = false;
        }

    }
    public void disableAllAnimations()
    {
        for(int i=0;i<anim.parameterCount;i++)
        {
            AnimatorControllerParameter parameter = anim.parameters[i];
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(parameter.name, false);
            }
        }
    }


}

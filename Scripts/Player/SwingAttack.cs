using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SwingAttack : MonoBehaviour
{
    public MageScript Mage;
    public Animator anim;

    public GameObject SwingAttackBox;
    public GameObject SlideAttackBox;

    public bool NormalSwinging = false;
    public bool SlideSwinging = false;

    private float swingTimer = 0;
    public float swingMaxTime = 0.45f;
    public float swingMaxTimeSlide = 0.3f;
    public float swingCoolDown;
    private float swingCoolDownTimer = 0;
    private bool onCoolDown = false;

    public GameObject TimeBar;

    public bool buttonSwing;


    public AudioSource Audio;
    public MenuButtons MenuButtons;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mage.isSliding)
        {
            if ((Input.GetKey(KeyCode.C)||buttonSwing) && !onCoolDown && !SlideSwinging && !NormalSwinging  )
            {
                if (MenuButtons.soundOn)
                    Audio.Play();
                buttonSwing = false;
                Swing("SwingSlide", ref SlideSwinging, ref SlideAttackBox);
                
                Mage.isSliding = false;
            }
        }
        else
        {
            if ((Input.GetKey(KeyCode.C) || buttonSwing) && !onCoolDown && !SlideSwinging && !NormalSwinging)
            {
                if(MenuButtons.soundOn)
                Audio.Play();
                buttonSwing = false;
                //Mage.myBody.velocity = new Vector2(0, Mage.myBody.velocity.y);
                Swing("SwingBasic", ref NormalSwinging, ref SwingAttackBox);
            }
        }

  
        if (NormalSwinging)
        {
            if (swingTimer < swingMaxTime)
            {
                swingTimer += Time.deltaTime;
            }
            else
            {
                EndSwing("SwingBasic", ref NormalSwinging, ref SwingAttackBox);
            }
        }
        
        if (SlideSwinging)
        {
            if (swingTimer < swingMaxTimeSlide)
            {
                swingTimer += Time.deltaTime;
            }
            else
            {
                EndSwing("SwingSlide", ref SlideSwinging, ref SlideAttackBox);
               
            }
        }

        
        if (onCoolDown)
        {
            ManageCooldown();
        }
    }


    private void Swing(string animation, ref bool swingingType, ref GameObject attack)
    {
        Mage.disableAllAnimations();
        
        onCoolDown = false;
        Mage.canMove = false;
        anim.SetBool(animation, true);
        swingingType = true;
        swingTimer = 0;
        attack.SetActive(true);
    }

 
    private void EndSwing(string animation, ref bool swingingType, ref GameObject attack)
    {
        swingCoolDownTimer = 0; 
        onCoolDown = true;      
        swingingType = false;   
        anim.SetBool(animation, false);
        Mage.canMove = true;    
        attack.SetActive(false);
        
    }

   
    private void ManageCooldown()
    {
        TimeBar.gameObject.SetActive(true);
        if (swingCoolDownTimer < swingCoolDown)
        {
            
            TimeBar.transform.localScale = new Vector3((1 - swingCoolDownTimer / swingCoolDown) * 0.3f, 0.09613501f, 0);
            swingCoolDownTimer += Time.deltaTime;
        }
        else
        {
          
            TimeBar.gameObject.SetActive(false);
            swingCoolDownTimer = 0;
            onCoolDown = false;
        }
    }
}
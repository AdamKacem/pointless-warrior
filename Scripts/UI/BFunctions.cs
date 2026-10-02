using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BFunctions : MonoBehaviour
{
    public attack Attack;
    public MageScript Mage;
    public SwingAttack Swing;
    public Inventory Inv;
    public void RightPressed()
    {
        Mage.horizontalInput = 1;
        Mage.buttonsPressed = true;  
    }
    public void RightUnpressed()
    {
        Mage.horizontalInput = 0;
        Mage.buttonsPressed = false;
    }

    public void LeftPressed()
    {
        Mage.horizontalInput = -1;
        Mage.buttonsPressed = true;
    }
    public void LeftUnpressed()
    {
        RightUnpressed();
    }

    public void Dash()
    {
        Mage.buttonDash = true;
    }
    public void Jump()
    {
        Mage.buttonJump = true;
    }
    public void V()
    {
        Attack.buttonV = true;
    }
    public void C()
    {
       Swing.buttonSwing = true;
    }
    public void X()
    {
        Inv.buttonX = true;
    }
    public void N()
    {
        Inv.buttonN = true;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attack : MonoBehaviour
{
    private int direction = 1;
    [SerializeField] private float attackCooldown=1.2f;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private GameObject[] magicSqs;
    private float horizontalInput;
    private Animator anim;
    private float coolDown = Mathf.Infinity;
    private bool onWall;
    public MageScript Mage;
    public bool buttonV;

    public MenuButtons MenuButtons;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        onWall = anim.GetBool("onWall");
         

        
            if (Mage.horizontalInput > 0)
            {


                direction = 1;

            }
            else if (Mage.horizontalInput < 0)
            {



                direction = -1;

            
        }
        if (onWall)
        {
            //flip direction of the firing
            direction = -direction;
            //flip the fire point position
            firePoint.transform.position = new Vector3(2 * transform.position.x - firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z);
        }





            if ((Input.GetKeyDown(KeyCode.V)||buttonV) && coolDown > attackCooldown)
        {
            buttonV = false;
            Attack();
            if(MenuButtons.soundOn) 
            Audio.Play();

        }
        coolDown += Time.deltaTime;
    }

    private void Attack()
    {
        coolDown = 0;

        

        magicSqs[FindFreeSquare()].transform.position = firePoint.position;

        //magicSqs[FindFreeSquare()].GetComponent<MagicSquare>().SetDirection(direction);
        MagicSquare magicSquare = magicSqs[FindFreeSquare()].GetComponent<MagicSquare>();
       
        magicSquare.SetDirection(direction);
        
        
    }



    private int FindFreeSquare()
    {
        for (int i = 0; i < magicSqs.Length; i++)
        {
            if (!magicSqs[i].activeInHierarchy) return i;
        }
        return 0;
    }
}
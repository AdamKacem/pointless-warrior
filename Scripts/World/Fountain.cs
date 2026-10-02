using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public GameObject Ground;
    private Animator anim;
    public BoxCollider2D box;
    public GameObject JumpText;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("attack"))
        {
            
            
            anim.SetBool("redFountain",true);
            Ground.gameObject.SetActive(false);
            box.isTrigger=true;
            JumpText.gameObject.SetActive(true);
        }

    }
}

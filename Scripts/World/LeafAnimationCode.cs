using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafAnimationCode : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject[] leafHolder;
    public Transform LeafPoint;
    public MenuButtons MenuButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)

    {   if(MenuButtons.soundOn)
        Audio.Play();

        for (int i = 0; i < leafHolder.Length;i++)
        {
            
            //leafHolder[i].GetComponent<LeafScript>;
            leafHolder[i].transform.position = LeafPoint.position;
            LeafScript leaf = leafHolder[i].GetComponent<LeafScript>();
            leaf.Move(new Vector3(Random.Range(-2f, 2f), 1, 0));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Camera Camera;
    public CameraController cam;
    public Transform nextRoom;
    public Transform prevRoom;
    public CenterOfCameraLogic Mage;
    public CamSize nextRoomSize;
    public CamSize prevRoomSize;
    public int camSize = 0;
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (prevRoom == null)
            {
                Mage.SetCenterOfCamera(false);
            }

            if (nextRoom == null)
            {
                if (camSize != 0)
                Camera.orthographicSize = camSize;

                Mage.SetCenterOfCamera(true);
                
            }
           
            else
            {
                if (collision.transform.position.x < transform.position.x)
                            {
                               
                                cam.ChangeRoom(nextRoom, nextRoomSize.getSize());
                            }

                else if(collision.transform.position.x > transform.position.x)
                        {
                    
                    cam.ChangeRoom(prevRoom, prevRoomSize.getSize());
                        }

            }
            

        }
    }
}

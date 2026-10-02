using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    private float currentX= -2.53f;
    private float currentY= -0.99f;
    private Vector3 velocity = Vector3.zero;
    public CenterOfCameraLogic Mage;
    public Transform MagePos;
    public Camera Camera;
    public float yOffset = 0f; // how far above (+) or below (-) the player the camera sits while following

   
    // Update is called once per frame
    void Update()
    {
        if (Mage.IsCenterOfCamera())
        {
            //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(MagePos.position.x, MagePos.position.y, transform.position.z), ref velocity, 0.1f*speed);
            transform.position = new Vector3(MagePos.position.x, MagePos.position.y + yOffset, transform.position.z);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentX,currentY,transform.position.z),ref velocity, speed);
        }
       
     
        
    }

    public void ChangeRoom(Transform _newRoom,int CamSize)
    {
        currentX = _newRoom.position.x;
        currentY = _newRoom.position.y;
        Camera.orthographicSize = CamSize;
        
    }
}

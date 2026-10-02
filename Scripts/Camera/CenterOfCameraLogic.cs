using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfCameraLogic : MonoBehaviour
{
    public bool CenterOfCamera = false;
    

    public void SetCenterOfCamera(bool _value)
    {
        this.CenterOfCamera = _value;   
    }
    public bool IsCenterOfCamera()
    {
        return CenterOfCamera;
    }
}

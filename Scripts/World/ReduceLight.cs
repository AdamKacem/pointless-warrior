using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ReduceLight : MonoBehaviour
{
    public string lightObjectName = "PlayerLight";
    private Light2D Light;
    public Color inCaveColor;
    // Start is called before the first frame update
    void Start()
    {
        GameObject lightObject = GameObject.Find(lightObjectName);

        Light = lightObject.GetComponent<Light2D>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x > 0)
            {
                Light.intensity = 0.6f;
                Light.color = inCaveColor;
            }
        }
    }
}

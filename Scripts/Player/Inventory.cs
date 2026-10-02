using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public List<char> Items;
    public MageScript Mage;
    public GameObject magicSqs;
    private float effectTimer = 0;
    public TimeBar TimeBar;
    public GameObject TimeBarObject;

    public GameObject SpeedParticles;
    public GameObject StrengthParticles;


    //colors
    public Color SpeedColor;
    public Color StrengthColor;


    
    private bool speedEffect = false;
    private bool damageEffect = false;

    //counters
    public TextMeshProUGUI counterSpeed;
    public TextMeshProUGUI counterStrength;


    public bool buttonX;
    public bool buttonN;
    void Start()
    {
        Debug.Log(SpeedParticles);
    }

    void Update()
    {

        //Effects activation
        if (Input.GetKeyDown(KeyCode.X)|| buttonX)
        {
            buttonX = false;
            if (Items.Contains('x') && !damageEffect && !speedEffect)
            {
                //decrement HUD counter
                AddToText(counterSpeed, -1);

                effectTimer = 0;
                SpeedParticles.SetActive(true);
                var particleSystem = SpeedParticles.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
                else
                {
                    Debug.LogError("SpeedParticles missing ParticleSystem component!");
                }
                TimeBarObject.SetActive(true);
                Items.Remove('x');
                Mage.speed = 15;
                Mage.sprite.color = SpeedColor;
                Mage.slideSpeed = 15;
                speedEffect = true;
                ChangeSquaresSpeed(31);
                TimeBar.Image.color = SpeedColor;
              
            }
        }


        if (Input.GetKeyDown(KeyCode.N)||buttonN)
        {
            buttonN = false;
            if (Items.Contains('n') && !damageEffect && !speedEffect)
            {
                //decrement HUD counter
                AddToText(counterStrength, -1);


                effectTimer = 0;
                StrengthParticles.SetActive(true);
                TimeBarObject.SetActive(true);
                Items.Remove('n');
                Mage.strength = 2;
                Mage.sprite.color = StrengthColor;
                damageEffect = true;
                TimeBar.Image.color = StrengthColor;
                
            }
        }





        //Effects time control
        if (speedEffect)
        {
            if (effectTimer < 5)
            {
                effectTimer = effectTimer + Time.deltaTime;
                ChangeBarSize(effectTimer,5);
                
                
            }
            else
            {
                speedEffect = false;
                SpeedParticles.SetActive(false);
                effectTimer = 0;
                TimeBarObject.SetActive(true);
                Mage.sprite.color = Color.white;
                Mage.speed = 6;
                Mage.slideSpeed = 9.5f;
                ChangeSquaresSpeed(17);
            }
        }

        if (damageEffect)
        {
            if (effectTimer < 7)
            {
                effectTimer = effectTimer + Time.deltaTime;
                ChangeBarSize(effectTimer, 7);


            }
            else
            {
                damageEffect = false;
                StrengthParticles.SetActive(false);
                effectTimer = 0;
                TimeBarObject.SetActive(true);
                Mage.sprite.color = Color.white;
                Mage.strength = 1;
                
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch (collision.gameObject.tag)
        {

            case "SpeedBonus":
                
                Items.Add('x');
            break;
            case "StrengthBonus":

                Items.Add('n');
                break;
            default:
                break;

        }
    }

    private void ChangeSquaresSpeed(int speed)
    {
        foreach (Transform Child in magicSqs.transform)
        {
            MagicSquare Square = Child.GetComponent<MagicSquare>();
            Square.speed = speed;
        }
    }

    private void ChangeBarSize(float timer,float maxTimer)
    {
        float timePercent = timer / maxTimer;
        //TimeBar.transform.localScale = new Vector3 ((1-timePercent)/1.9f, 0.09f, 0.8f);
        TimeBarObject.transform.localScale = new Vector3((1 - timePercent),1,1);
    }



    public void AddToText(TextMeshProUGUI targetText , int amount)
    {
        if (targetText != null)
        {
            // Parse the current text into an integer
            if (int.TryParse(targetText.text, out int currentValue))
            {
                // Update the text with the new value
                targetText.text = (currentValue + amount).ToString();
            }
            else
            {
                Debug.LogWarning("The text is not a valid number!");
            }
        }
        else
        {
            Debug.LogWarning("Target text is not assigned!");
        }
    }


}



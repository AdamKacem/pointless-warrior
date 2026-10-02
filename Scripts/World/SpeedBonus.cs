using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpeedBonus : MonoBehaviour
{
    public TextMeshProUGUI counterSpeed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            gameObject.SetActive(false);
            AddToText(counterSpeed, 1);
            
        }
          

    }

    public void AddToText(TextMeshProUGUI targetText, int amount)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI percentPollinatedText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI colorToPollinateText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = transform.Find("Health").GetComponent<TextMeshProUGUI>();
        percentPollinatedText = transform.Find("Pollinated").GetComponent<TextMeshProUGUI>();
        colorToPollinateText = transform.Find("ColorToPollinate").GetComponent<TextMeshProUGUI>();
        timeText = transform.Find("Time").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareUIForTimeTrial()
    {
        healthText.gameObject.SetActive(false);
        percentPollinatedText.gameObject.SetActive(true);
    }

    public void PrepareUIForSurvival()
    {
        healthText.gameObject.SetActive(true);
        percentPollinatedText.gameObject.SetActive(false);
    }

    public void UpdateColorToPollinate(string color)
    {
        if (colorToPollinateText != null)
        {
            if (color == Tags.RedFlower)
            {
                colorToPollinateText.color = Color.red;
                colorToPollinateText.text = "Pollinate red flowers";
            }
            else if (color == Tags.GreenFlower)
            {
                colorToPollinateText.color = Color.green;
                colorToPollinateText.text = "Pollinate green flowers";
            }
            else if (color == Tags.BlueFlower)
            {
                colorToPollinateText.color = Color.blue;
                colorToPollinateText.text = "Pollinate blue flowers";
            }
            else if (color == Tags.YellowFlower)
            {
                colorToPollinateText.color = Color.yellow;
                colorToPollinateText.text = "Pollinate yellow flowers";
            }
        }
    }

    public void UpdateHealth(float health)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }

    public void UpdatePercentPollinated(int percent)
    {
        if (percentPollinatedText != null)
        {
            percentPollinatedText.text = "% pollinated: " + percent;
        }
    }

    public void UpdateTime(float minutes, float seconds)
    {
        if(timeText != null)
        {
            timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}

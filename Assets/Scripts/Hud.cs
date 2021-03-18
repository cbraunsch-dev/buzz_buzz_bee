using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI percentPollinatedText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = transform.Find("Health").GetComponent<TextMeshProUGUI>();
        percentPollinatedText = transform.Find("Pollinated").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

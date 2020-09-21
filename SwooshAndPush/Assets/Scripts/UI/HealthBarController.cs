using UnityEngine;
using UnityEngine.UI;


public class HealthBarController : MonoBehaviour
{
    // Objects used in health bar
    public Slider slider;
    public Text healthCounter;
    public Image fill;
    public Image heart;

    // Health number percentage
    private float currentPercent;

    private void Update()
    {
        currentPercent = slider.value / slider.maxValue;
        healthCounter.text = slider.value.ToString("0.00");
        fill.color = Color.Lerp(Color.red, Color.green, currentPercent);
        heart.color = Color.Lerp(Color.red, Color.green, currentPercent);
        healthCounter.color = Color.Lerp(Color.white, Color.black, currentPercent);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}

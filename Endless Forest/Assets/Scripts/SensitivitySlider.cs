using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SensitivitySlider : MonoBehaviour
{
    public Slider sensitivitySlider;
    public PlayerMovement playerMovement;
    public TextMeshProUGUI sensitivityText;

    private void Start()
    {
        // Add a listener to the sensitivity slider to update sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    public void UpdateSensitivity(float value)
    {
        // Define minimum and maximum values for the slider and sensitivity
        float minSliderValue = 1f;
        float maxSliderValue = 10f;
        float minSensitivity = 0.2f;
        float maxSensitivity = 2f;

        // Interpolate the sensitivity value based on the slider value
        float sensitivityValue = Mathf.Lerp(minSensitivity, maxSensitivity, Mathf.InverseLerp(minSliderValue, maxSliderValue, value));

        // Update the player's sensitivity and the sensitivity display text
        playerMovement.SetSensitivity(sensitivityValue);
        sensitivityText.text = value.ToString("F1"); // Display sensitivity value with one decimal place
    }
}
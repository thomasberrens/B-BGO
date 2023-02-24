using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private Slider _soundSlider;

    private void Start()
    {
        _soundSlider = GetComponent<Slider>();
        _soundSlider.value = PlayerPrefs.GetFloat("AudioValue", 1f);
        GetComponent<Slider>().onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("AudioValue", value);
        Debug.Log("Slider value changed to: " + value);
    }

    private void OnDestroy()
    {
        // Remove the listener when the script is destroyed to avoid memory leaks
        _soundSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
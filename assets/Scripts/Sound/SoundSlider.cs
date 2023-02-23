using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        GameManager.Instance.SoundManager.volume = value;
        Debug.Log("Slider value changed to: " + value);
    }

    private void OnDestroy()
    {
        // Remove the listener when the script is destroyed to avoid memory leaks
        soundSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
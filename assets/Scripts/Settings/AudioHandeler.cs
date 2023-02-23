using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = AudioManager.volume;
    }
    
    //assign this to the slider in the UI
    public void OnValueChanged()
    {
        AudioManager.volume = slider.value;
    }
}

public static class AudioManager
{
    public static float volume = 1.0f;

    static AudioManager()
    {
        // Set the initial volume for all AudioSources in the scene
        /*foreach (var audioSource in FindObjectsOfType<AudioManager>())
        {
            audioSource.volume = volume;
        }*/
    }
}


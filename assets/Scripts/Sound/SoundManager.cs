using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //this function can be called from other scripts
    public void PlaySound(AudioClip clip)
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = PlayerPrefs.GetFloat("AudioValue", 1f);;
        audioSource.Play();
        Destroy(soundObj, clip.length);
    }
}
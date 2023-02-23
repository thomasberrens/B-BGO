using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] public float volume = 1;
    //this function can be called from other scripts
    public void PlaySound(AudioClip clip)
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(soundObj, clip.length);
    }
}
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource = null;

    public void PlayAudio(AudioClip clip){
        audioSource.clip = clip;  
        audioSource.Play();
    }

    public void stopAudio(){
        audioSource.Stop();
    }
}

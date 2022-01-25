using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public DialogueInfosData dialogueInfo;
    public Action<String> OnchangeAudio = null;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        DialogueManager.Instance.OnchangeDialogue += PlayAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAudio(AudioClip clip){
        audioSource.clip = clip;  
        audioSource.Play();
    }

    void stopAudio(){
        audioSource.Stop();
    }
}

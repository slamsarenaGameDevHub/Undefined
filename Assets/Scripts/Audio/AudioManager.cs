using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer mixer;

    

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.Source=gameObject.AddComponent<AudioSource>();
            s.Source.clip=s.clip;
            s.Source.loop=s.isLoop;
            s.Source.outputAudioMixerGroup = s.mixer;
            s.Source.pitch = s.Pitch;
            s.Source.volume = s.Volume;
            s.Source.bypassReverbZones = s.byPassEffect;
            
           
        }
        
    }


    public void PlaySound(string Name)
    {
        Sound s=Array.Find(sounds,Sound=>Sound.ClipName==Name);
        s.Source.Play();
    }
}

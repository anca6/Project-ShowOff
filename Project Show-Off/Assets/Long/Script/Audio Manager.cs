using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update

    
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play (string name)
    {
       
       Sound sound = Array.Find(sounds, sound => sound.name == name);
       if (sound == null)
       {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
       }
       sound.source.Play();
    }


    
}

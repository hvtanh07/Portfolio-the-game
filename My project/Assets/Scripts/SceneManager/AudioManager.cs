using System.Collections;
using System;
using UnityEngine;

[System.Serializable]
public class Ambient_Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(0f, 3f)]
    public float pitch = 1.0f;
    public bool isLoop;

    [HideInInspector]
    public AudioSource m_source;

    public void SetSource(AudioSource source)
    {
        m_source = source;
    }

    public void Play()
    {
        Debug.Log(m_source);
        m_source.loop = isLoop;
        m_source.Play();
    }
}
public class AudioManager : MonoBehaviour
{
    // Make it a singleton class that can be accessible everywhere
    public static AudioManager instance;

    [SerializeField]
    Ambient_Sound[] m_sounds;

    private void Awake()
    {
        foreach ( Ambient_Sound s in m_sounds){
            s.m_source = gameObject.AddComponent<AudioSource>();
            s.m_source.clip = s.clip;

            s.m_source.volume = s.volume;
            s.m_source.pitch = s.pitch;
            s.m_source.loop = s.isLoop;
        }
    }

    public void PlaySound (string name)
    {
        Ambient_Sound s = Array.Find(m_sounds, sound => sound.name == name);
        if(s != null){
            s.m_source.Play(); 
            return;
        }
        Debug.LogWarning("AudioManager: Sound name not found in list: " + name);
    }
}

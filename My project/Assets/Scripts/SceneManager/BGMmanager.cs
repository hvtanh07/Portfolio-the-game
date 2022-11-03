using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    private AudioManager audioManager;
    public string SongName;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound(SongName);
    }
    
    public void DimAudio(float duration){
        FindObjectOfType<AudioManager>().DimBGM(duration, SongName);
    }
}

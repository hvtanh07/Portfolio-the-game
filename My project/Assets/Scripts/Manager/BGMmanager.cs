using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    private AudioManager_PrototypeHero audioManager;
    public string SongName;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager_PrototypeHero.instance;
        audioManager.PlaySound(SongName);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] Transform destination;
    private AudioSource au;
    public AudioClip launchClip;
    public AnimationCurve ease;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    public void StartFly(){
        au.loop = false;
        au.clip = launchClip;
        
        LeanTween.move(this.gameObject,destination,10f).setEase(ease).setOnComplete(DeleteObject);
    }
    void DeleteObject(){
        Destroy(gameObject);
    }
}

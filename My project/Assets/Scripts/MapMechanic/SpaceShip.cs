using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] ParticleSystem smoke;
    public CameraShake shakeEffect;
    public GameObject dropkey;
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
        smoke.maxParticles = 500;
        shakeEffect.Shake(13f);
        au.Play();
        LeanTween.move(this.gameObject,destination,10f).setEase(ease).setOnComplete(DeleteObject);
    }
    void DeleteObject(){
        Instantiate(dropkey,transform.position,Quaternion.AngleAxis(45f,Vector3.forward));
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private AudioSource au;
    public Vector3 moveDirection;
    public bool unlocked;
    public float speed;
    private Vector3 targetDir;
    [SerializeField] private Key.KeyType keyType;
    private void Start()
    {
        au = GetComponent<AudioSource>();
        unlocked = false;
        targetDir = transform.position + moveDirection;
    }
    public void OpenDoor()
    {
        switch (keyType)
        {
            case Key.KeyType.Red:
                {
                    LeanTween.move(this.gameObject, targetDir, speed).setOnComplete(stopAudio);
                    au.Play();
                    break;
                }
            case Key.KeyType.Blue:
                {
                    LeanTween.rotateAroundLocal(this.gameObject, Vector3.forward, 90, speed);
                    GetComponent<BoxCollider2D>().enabled = false;
                    break;
            }
        }
        
        unlocked = true;


    }
    public void stopAudio()
    {
        au.Stop();
    }
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }
}

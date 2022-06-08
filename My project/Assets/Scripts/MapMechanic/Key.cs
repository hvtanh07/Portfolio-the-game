using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private KeyType keyType;
    private Animator anim;
    private bool Acquired;
    public enum KeyType{
        Red,
        Blue,
        Green
    }
    private void Awake() {
        Acquired = false;
        anim = GetComponent<Animator>();
    }
    public void AcquireKey(){
        anim.SetTrigger("gotKey");
        Acquired = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,5f);
    }
    public KeyType GetKeyType(){
        return keyType;
    }
    private void Update() {
        if(Acquired){
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}

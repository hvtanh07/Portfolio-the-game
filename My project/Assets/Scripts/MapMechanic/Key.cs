using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private KeyType keyType;
    private Animator anim;
    public enum KeyType{
        Red,
        Blue,
        Green
    }
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public void AcquireKey(){
        anim.SetTrigger("gotKey");
        Destroy(gameObject,5f);
    }
    public KeyType GetKeyType(){
        return keyType;
    }

}

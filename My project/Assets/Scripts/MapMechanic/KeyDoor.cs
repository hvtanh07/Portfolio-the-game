using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Vector3 moveDirection;
    public bool unlocked;
    public float speed;
    private Vector3 targetDir;
    [SerializeField] private Key.KeyType keyType;
    private void Start() {
        unlocked = false;
        targetDir = transform.position + moveDirection;
    }
    public void OpenDoor(){
        unlocked = true;
    }
    public Key.KeyType GetKeyType(){
        return keyType;
    }
    private void Update() {
        if (unlocked){
            transform.position = Vector3.MoveTowards(transform.position, targetDir, speed * Time.deltaTime); 
        }   
    }
    
}

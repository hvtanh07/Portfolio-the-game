using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAutoMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().automove = false;
            Destroy(gameObject);
        }
    }
}

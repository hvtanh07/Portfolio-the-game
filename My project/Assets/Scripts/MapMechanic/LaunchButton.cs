using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    private AudioSource au;
    [SerializeField] SpaceShip spaceShip;
    [SerializeField] Sprite clickedSprite;
    private bool triggered = false;
    private void Start() {
        au = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            au.Play();
            GetComponent<SpriteRenderer>().sprite = clickedSprite;
            spaceShip.StartFly();
        }
    }
}

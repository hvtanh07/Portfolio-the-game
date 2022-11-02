using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    private AudioSource au;
    private Rigidbody2D rb;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            au.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 dir = (this.transform.position - other.transform.position).normalized;
            rb.AddForce(dir * Force, ForceMode2D.Impulse);
        }
    }
}

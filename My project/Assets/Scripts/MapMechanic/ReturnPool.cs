using System.Collections;
using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    private Vector3 destination;
    public float MinSpeed,Maxspeed;
    private float speed;
    private void OnEnable() {
        StartCoroutine(waiter());
        //LeanTween.move
    }
    IEnumerator waiter(){
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }

    public void SetDestination(Vector3 destination){
        this.destination = destination;
        speed = Random.Range(MinSpeed, Maxspeed);
    }
}

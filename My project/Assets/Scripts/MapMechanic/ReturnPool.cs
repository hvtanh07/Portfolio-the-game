using System.Collections;
using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    private Vector3 destination;
    public float MinSpeed,Maxspeed;
    private float speed;
    private void OnEnable() {
        LeanTween.move(this.gameObject,destination,speed).setOnComplete(returnPool);
    }
    public void returnPool(){
        gameObject.SetActive(false);
    }

    public void SetDestination(Vector3 destination, bool flip){
        this.destination = destination;
        speed = Random.Range(MinSpeed, Maxspeed);
         Vector3 theScale = transform.localScale;
        if (flip){        
            if(theScale.x > 0)
            {		        
                theScale.x *= -1;
		        transform.localScale = theScale;
            }
        }else{
            if(theScale.x < 0)
            {		        
                theScale.x *= -1;
		        transform.localScale = theScale;
            }
        }  
    }
}

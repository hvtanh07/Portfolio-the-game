using System.Collections;
using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    private Vector3 destination;
    private float moveTime;
    private void OnEnable() {
        LeanTween.move(this.gameObject,destination,moveTime).setOnComplete(returnPool);
    }
    public void returnPool(){
        gameObject.SetActive(false);
    }

    public void SetDestination(Vector3 destination, bool flip, float timetoMove){
        this.destination = destination;
        moveTime = timetoMove;
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

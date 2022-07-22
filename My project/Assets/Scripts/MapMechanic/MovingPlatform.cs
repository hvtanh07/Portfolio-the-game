using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveTime;
    public bool moveStart;
    public bool loop;
    public int startingPoint;
    public Transform[] points;
    

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = startingPoint + 1;
        transform.position = points[startingPoint].position;
        if (moveStart){
            MovePlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {       
        if (!moveStart){
            MovePlatform();
            moveStart = true;
        }
        other.transform.SetParent(transform);     
    }
    private void OnTriggerExit2D(Collider2D other) {
        other.transform.SetParent(null);
    }
    private void MovePlatform(){
        LeanTween.move(this.gameObject, points[i].transform.position,moveTime).setEaseInOutQuart().setOnComplete(nextPoint);
    }
    public void nextPoint(){
        i++;
        if(i == points.Length){           
            i = 0;                                
        }
        if(loop)
            MovePlatform();
        else
            moveStart = false;
    }
}

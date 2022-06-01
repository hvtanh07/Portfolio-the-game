using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public bool activated;
    public int startingPoint;
    public Transform[] points;

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated){
            if(Vector2.Distance(transform.position, points[i].position) < 0.02f){
                i++;
                if(i == points.Length){
                    i = 0;
                }
            }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime); 
        }             
    }

    private void OnTriggerEnter2D(Collider2D other) {       
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D other) {
        other.transform.SetParent(null);
    }
}

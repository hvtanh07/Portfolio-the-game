using UnityEngine;

public class GetfromPool : MonoBehaviour
{
    public float minTime,maxTime;
    public float minTimeToMove, maxTimeToMove;
    public Transform endpoint;
    public bool flip;
    public int sortingLayer;

    private void Start() {
        Invoke("SpawnVehicle", 0.2f);        
    }
    void SpawnVehicle(){     
        float randomTime = Random.Range (minTime, maxTime);
        Invoke ("SpawnVehicle", randomTime);    
        InstantiateObject ();                     
    }

    public void InstantiateObject (){
        GameObject obj = ObjectPool.SharedInstance.GetPooledObject(); 
        if (obj != null) {
            float timetoMove = Random.Range(minTimeToMove, maxTimeToMove);
            //set position & rotation
            obj.GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
            obj.transform.position = transform.position;
            obj.transform.rotation  = transform.rotation;                    
            obj.GetComponent<ReturnPool>().SetDestination(endpoint.position,flip,timetoMove);
            obj.SetActive(true);
        }
    }
}

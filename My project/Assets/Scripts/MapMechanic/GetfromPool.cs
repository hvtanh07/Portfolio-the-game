using UnityEngine;

public class GetfromPool : MonoBehaviour
{
    public float minTime,maxTime;
    public Transform endpoint;
    public bool flip;

    private void Start() {
        Invoke("SpawnVehicle", 0f);        
    }
    void SpawnVehicle(){     
        float randomTime = Random.Range (minTime, maxTime);
        Invoke ("SpawnVehicle", randomTime);    
        InstantiateObject ();                     
    }
    //public Vector3 destination;
    public void InstantiateObject (){
        GameObject obj = ObjectPool.SharedInstance.GetPooledObject(); 
        if (obj != null) {
            //set position & rotation
            obj.transform.position = transform.position;
            obj.transform.rotation  = transform.rotation;                    
            obj.GetComponent<ReturnPool>().SetDestination(endpoint.position,flip);
            obj.SetActive(true);
        }
    }
}

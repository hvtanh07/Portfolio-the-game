using UnityEngine;

public class GetfromPool : MonoBehaviour
{
    //public Vector3 destination;
    public void InstantiateObject (){
        GameObject obj = ObjectPool.SharedInstance.GetPooledObject(); 
        if (obj != null) {
            //set position & rotation
            obj.transform.position = transform.position;
            obj.transform.rotation  = transform.rotation; 
            obj.SetActive(true);
            //set destination to go
            //obj.GetComponent<ReturnPool>().SetDestination(destination);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<Sprite> spritelist;
    private int spriteindex;
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
        spriteindex = 0;
    }

    
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.transform.SetParent(this.transform);
            //set srite
            //tmp.GetComponent<SpriteRenderer>().sprite = getaSprite();
            pooledObjects.Add(tmp);
        }
    }

    private Sprite getaSprite(){
        if (spriteindex >= spritelist.Count){
            spriteindex = 0;
        }
        return spritelist[spriteindex++];
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

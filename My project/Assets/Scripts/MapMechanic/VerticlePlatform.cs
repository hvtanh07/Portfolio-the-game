using UnityEngine;

public class VerticlePlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    void Start(){
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.S)){
            waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.S)){
            if(waitTime <=0){
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }else{
                waitTime -= Time.deltaTime;
            }
        }

        if(Input.GetKey(KeyCode.Space)){
            if(effector.rotationalOffset != 0f)
                effector.rotationalOffset = 0f;
        }
    }
}

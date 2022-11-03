using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator anim;
    [SerializeField] AudioManager au;
    [SerializeField] BGMmanager[] bgmDimList;
    public float timeBeforeLoadNextScene = 3f;
    public string sceneName;
    public bool autoMove;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().automove = autoMove;
            foreach (BGMmanager bgm in bgmDimList){
                bgm.DimAudio(timeBeforeLoadNextScene);
            }
            StartCoroutine(LoadingScene());
        }
    }
    

    IEnumerator LoadingScene(){
        anim.SetTrigger("end");
        yield return new WaitForSeconds(timeBeforeLoadNextScene);
        SceneManager.LoadScene(sceneName);
    }
}

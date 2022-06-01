using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator anim;
    public float timeBeforeLoadNextScene = 3f;
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().automove = true;
            StartCoroutine(LoadingScene());
        }
    }
    public void LoadNewScene(){
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene(){
        anim.SetTrigger("end");
        yield return new WaitForSeconds(timeBeforeLoadNextScene);
        SceneManager.LoadScene(sceneName);
    }
}

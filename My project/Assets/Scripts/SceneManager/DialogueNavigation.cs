using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNavigation : MonoBehaviour
{
    [SerializeField]
    private DialogueManager manager;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            manager.DisplayNextSentence();
        }
    }
}

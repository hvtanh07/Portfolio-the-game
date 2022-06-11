using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public enum triggerType{
        startDialogue,
        stopDialogue
    }
    public triggerType type;
    public Dialogue dialogue;
    public void TriggerDialogue(){
        if (type == triggerType.startDialogue){
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }else if (type == triggerType.stopDialogue){
            FindObjectOfType<DialogueManager>().StopDialogue();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){          
            TriggerDialogue();
            Destroy(gameObject);
        }
       
    }
}

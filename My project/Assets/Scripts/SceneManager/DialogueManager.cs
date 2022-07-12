using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject diaogueBG;
    public Text dialogueText;
    public Queue<string>  sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue){
        sentences.Clear();
        diaogueBG.SetActive(true);
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void StopDialogue(){
        diaogueBG.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            DisplayNextSentence();
        }
    }
    
    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            StopDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
}

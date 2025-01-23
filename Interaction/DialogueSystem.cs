using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
	public Queue<string> sentences;
	[SerializeField] TMP_Text _dialogueText;
	[SerializeField] Animator dialogueAnimator;
	void Start()
	{
		sentences=new Queue<string>();
	}
	public void StartDialogue(Dialogue dialogue)
	{
		
	dialogueAnimator.SetBool("isTalking",true);
		sentences.Clear();
		foreach(string sentence in dialogue.Sentence)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();

	}
	public void DisplayNextSentence()
	{
		if(sentences.Count==0)
		{
			EndDialogue();
			return;
		}
	
	string sentence=sentences.Dequeue();
	_dialogueText.text="";
	DisplaySentence(sentence);
	}
	public void DisplaySentence(string line)
	{
		_dialogueText.text=line;
	}
	void EndDialogue()
	{
		dialogueAnimator.SetBool("isTalking", false);
	
		//Disable text or call some kinda of animation
	}

}
[System.Serializable]
public class Dialogue
{
	
	[TextArea(4,10)]
	public string[] Sentence;
}

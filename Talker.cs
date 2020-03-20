using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : WalkerPNJ {

	public CanvasRenderer conversation;
	public string whatToSay;

	// Use this for initialization
	void Start () {
		this.setIncrement((int)Mathf.Sqrt (velocity.x * velocity.x + velocity.z * velocity.z));
	}

	override public void talkTo() {
		this.conversation.gameObject.SetActive (true);
		this.conversation.gameObject.transform.GetChild (0).GetComponent<Text> ().text = whatToSay;
		this.setTalking (true);
	}

	override public void finishTalk() {
		this.conversation.gameObject.SetActive (false);
		this.setTalking (false);
	}
}

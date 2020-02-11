using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : WalkerPNJ {

	public CanvasRenderer conversation;

	// Use this for initialization
	void Start () {
		this.setIncrement((int)Mathf.Sqrt (velocity.x * velocity.x + velocity.z * velocity.z));
	}

	override public void talkTo() {
		this.conversation.gameObject.SetActive (true);
		this.setTalking (true);
	}

	override public void finishTalk() {
		this.conversation.gameObject.SetActive (false);
		this.setTalking (false);
	}
}

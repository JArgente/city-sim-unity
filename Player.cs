using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	private Enemy opponent;

	// Use this for initialization
	void Start () {
		this.updateHealth(this.maxHealth);
	}

	public void setOpponent(Enemy opponent){
		this.opponent = opponent;
	}
	
	// Update is called once per frame
	void Update () {
		base.writeStats ();
		if (Input.GetKey ("left")) {
			this.transform.position = new Vector3 (
				this.transform.position.x-this.velocity*Time.deltaTime, 
				this.transform.position.y, 
				this.transform.position.z);
		}
		if (Input.GetKey ("right")) {
			this.transform.position = new Vector3 (
				this.transform.position.x+this.velocity*Time.deltaTime, 
				this.transform.position.y, 
				this.transform.position.z);
		}
		if (Input.GetKey ("up")) {
			this.transform.position =new Vector3 (
				this.transform.position.x, 
				this.transform.position.y,
				this.transform.position.z+this.velocity*Time.deltaTime);
		}
		if (Input.GetKey ("down")) {
			this.transform.position = new Vector3 (
				this.transform.position.x, 
				this.transform.position.y,
				this.transform.position.z-this.velocity*Time.deltaTime);
		}
		if (Input.GetKeyDown ("space")) {
			if (this.isEngaged()) {
				opponent.sufferAttack (this.attack);
			}
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "PNJ") {
			WalkerPNJ pnj = other.collider.GetComponent<WalkerPNJ> ();
			pnj.talkTo ();
		}
		if (other.gameObject.tag == "Item") {
			other.collider.GetComponent<Item> ().consume (this);
			Destroy (other.gameObject);
		}
	}

	private void OnCollisionExit(Collision other) {
		if (other.gameObject.name == "PNJ") {
			WalkerPNJ pnj = other.collider.GetComponent<WalkerPNJ> ();
			pnj.finishTalk ();
		}
	}

	override public void sufferAttack(float attack){
		this.updateHealth( - (attack - this.armour));
		if (this.getHealth() <= 0) {
			this.setEngaged (false);
			this.opponent = null;
			Destroy (this.gameObject);
		}
	}
}

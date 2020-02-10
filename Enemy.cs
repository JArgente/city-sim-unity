using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public Player player;

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (this.transform.position, player.transform.position);
		if (dist < 1) {
			this.setEngaged(true);
			player.setEngaged(true);
			player.setOpponent(this);
			if (getTimeSinceLastAttack() >= attackCadence) {
				setTimeSinceLastAttack(0);
				player.sufferAttack (this.attack);
			}
			setTimeSinceLastAttack (getTimeSinceLastAttack() + Time.deltaTime);
		} else if (dist < 10) {
			this.setEngaged(false);
			player.setEngaged(false);
			Vector3 newPosition = Vector3.MoveTowards (this.transform.position, player.transform.position, velocity*Time.deltaTime);
			this.transform.position = newPosition;
		}
	}

	override public void sufferAttack(float attack){
		this.health = this.health - (attack - this.armour);
		if (this.health <= 0) {
			player.setEngaged (false);
			player.setOpponent (null);
			Destroy (this.gameObject);
		}
	}
}

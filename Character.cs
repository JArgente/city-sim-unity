using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {
	
	public float velocity;
	public float maxHealth;
	public float attack;
	public float armour;
	public float attackCadence;
	public Text displayStats;
    private float timeSinceLastAttack = 0;
	private float health;

	private bool engaged = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void writeStats(){
		displayStats.GetComponent<Text>().text = "Health: " + health + "/" + maxHealth + "\n"
			+ "Attack: " + attack + "\n"
			+ "Defense: " + armour + "\n";
	}

	public void setTimeSinceLastAttack(float timeSinceLastAttack){
		this.timeSinceLastAttack = timeSinceLastAttack;
	}

	public float getTimeSinceLastAttack(){
		return this.timeSinceLastAttack;
	}

	public void updateHealth(float increase){
		this.health = Mathf.Min(this.health+increase, this.maxHealth);
	}

	public float getHealth(){
		return this.health;
	}

	public bool isEngaged(){
		return this.engaged;
	}

	public void setEngaged(bool engaged){
		this.engaged = engaged;
	}

	abstract public void sufferAttack (float attack);
}

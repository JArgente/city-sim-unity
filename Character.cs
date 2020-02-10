using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	
	public float velocity;
	public float health;
	public float attack;
	public float armour;
	public float attackCadence;
    private float timeSinceLastAttack = 0;

	private bool engaged = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTimeSinceLastAttack(float timeSinceLastAttack){
		this.timeSinceLastAttack = timeSinceLastAttack;
	}

	public float getTimeSinceLastAttack(){
		return this.timeSinceLastAttack;
	}

	public bool isEngaged(){
		return this.engaged;
	}

	public void setEngaged(bool engaged){
		this.engaged = engaged;
	}

	abstract public void sufferAttack (float attack);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

	public int damage=50;

	public override string getName ()
	{
		return "Weapon";
	}

	override public void consume(Character character){
		character.attack=damage;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

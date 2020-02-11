using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Item {

	public int defense=200;

	public override string getName ()
	{
		return "Armour";
	}

	override public void consume(Character character){
		character.armour=defense;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

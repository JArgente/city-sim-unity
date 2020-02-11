using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item {

	public int healthRestore=200;

	public override string getName ()
	{
		return "Potion";
	}

	override public void consume(Character character){
		character.updateHealth (healthRestore);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkerPNJ : MonoBehaviour {

	public int maxDistance=10;
	public int forward = 1;
	public Vector3 velocity= new Vector3(0,0,0);

	private int increment;
	private float distance=0;
	private bool talking = false;

	void Start () {
		
	}

	public void setIncrement(int increment){
		this.increment=increment;
	}

	abstract public void talkTo ();

	abstract public void finishTalk ();

	public bool isTalking(){
		return talking;
	}

	public void setTalking(bool talking){
		this.talking=talking;
	}

	// Update is called once per frame
	void Update () {
		if (!talking) {
			if (distance >= maxDistance) {
				distance = 0;
				forward = -1 * forward;
			}
			transform.position = transform.position + forward * velocity * Time.deltaTime;
			distance = distance + increment* Time.deltaTime;
		}
	}
}

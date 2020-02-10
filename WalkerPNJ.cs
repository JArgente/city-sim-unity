using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerPNJ : MonoBehaviour {

	public int maxDistance=10;
	public int forward = 1;
	public Vector3 velocity= new Vector3(0,0,0);

	private int increment;
	private float distance=0;
	private bool talking = false;

	void Start () {
		this.increment = (int) Mathf.Sqrt (velocity.x * velocity.x + velocity.z * velocity.z);
	}

	public void talkTo() {
		this.talking = true;
	}

	public void finishTalk() {
		this.talking = false;
	}

	// Update is called once per frame
	void Update () {
		if (!this.talking) {
			if (this.distance >= this.maxDistance) {
				this.distance = 0;
				this.forward = -1 * this.forward;
			}
			this.transform.position = this.transform.position + this.forward * this.velocity * Time.deltaTime;
			this.distance = this.distance + this.increment* Time.deltaTime;
		}
	}
}

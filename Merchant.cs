using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : WalkerPNJ {

	public CanvasRenderer merchantPanel;
	public GameObject buttonPrefab;
	public Player player;

	public List<Item> stock;
	// Use this for initialization
	void Start () {
		this.setIncrement((int)Mathf.Sqrt (velocity.x * velocity.x + velocity.z * velocity.z));
	}


	override public void talkTo() {
		this.setTalking (true);
		Vector3 originalPos = merchantPanel.transform.position;
		float i = -20;
		int size = stock.ToArray ().Length;
		foreach(Item element in stock){
			GameObject button = (GameObject)Instantiate (buttonPrefab);
			button.transform.SetParent (merchantPanel.transform);
			button.transform.position = new Vector3(originalPos.x, originalPos.y+i, originalPos.z);
			button.GetComponent<Button> ().onClick.AddListener (delegate{buyItem(element);});
			button.transform.GetChild (0).GetComponent<Text> ().text = element.getName ();
			i = i + 40;
		}
		merchantPanel.gameObject.SetActive (true);
	}

	override public void finishTalk() {
		this.setTalking (false);
		merchantPanel.gameObject.SetActive (false);
		foreach (Transform child in merchantPanel.transform) {
			Destroy (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void buyItem (Item item) {
		item.consume (player);
	}
}

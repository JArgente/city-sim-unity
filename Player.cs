using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character {

	public Canvas menu;
	public Canvas contextual;
	public GameObject togglePrefab;
	public int money;
	private Enemy opponent;
	private List<Weapon> weapons= new List<Weapon>();
	private List<Armour> armours= new List<Armour>();
	private List<Potion> potions= new List<Potion>();
	private double lastAttack=0.0;

	private Toggle activeWeapon;
	private Toggle activeArmour;

	// Use this for initialization
	void Start () {
		this.updateHealth(this.maxHealth);
		menu.gameObject.SetActive (false);
	}

	public void setOpponent(Enemy opponent){
		this.opponent = opponent;
	}
	
	// Update is called once per frame
	void Update () {
		lastAttack = lastAttack + Time.deltaTime;
		base.writeStats ();
		displayStats.GetComponent<Text>().text = displayStats.GetComponent<Text>().text +"\n"
			+ "Money: " + money + "\n";
		if (!menu.gameObject.activeSelf) {
			if (Input.GetKey ("left")) {
				this.transform.position = new Vector3 (
					this.transform.position.x - this.velocity * Time.deltaTime, 
					this.transform.position.y, 
					this.transform.position.z);
			}
			if (Input.GetKey ("right")) {
				this.transform.position = new Vector3 (
					this.transform.position.x + this.velocity * Time.deltaTime, 
					this.transform.position.y, 
					this.transform.position.z);
			}
			if (Input.GetKey ("up")) {
				this.transform.position = new Vector3 (
					this.transform.position.x, 
					this.transform.position.y,
					this.transform.position.z + this.velocity * Time.deltaTime);
			}
			if (Input.GetKey ("down")) {
				this.transform.position = new Vector3 (
					this.transform.position.x, 
					this.transform.position.y,
					this.transform.position.z - this.velocity * Time.deltaTime);
			}
			if (Input.GetKeyDown ("space")) {
				if (this.isEngaged () && lastAttack >= attackCadence) {
					lastAttack = 0.0;
					opponent.sufferAttack (this.attack);
				}
			}
		}
		if (Input.GetKeyDown (".")) {
			contextual.gameObject.SetActive (!contextual.gameObject.activeSelf);
			menu.gameObject.SetActive (!menu.gameObject.activeSelf);
		}

	}

	public void addItem (Item item){
		int index = 0;
		int value = 0;
		int length=0;
		if (item.getName () == "Weapon") {
			index = 0;
			weapons.Add ((Weapon)item);
			value = ((Weapon)item).damage;
			length = weapons.ToArray().Length;
		}else if (item.getName () == "Armour") {
			index = 1;
			armours.Add ((Armour)item);
			value = ((Armour)item).defense;
			length = armours.ToArray().Length;
		}else if (item.getName () == "Potion") {
			index = 2;
			potions.Add ((Potion)item);
			value = ((Potion)item).healthRestore;
			length = potions.ToArray().Length;
		}
		GameObject toggle = (GameObject)Instantiate (togglePrefab);
		toggle.transform.SetParent (menu.transform.GetChild(0).transform.GetChild(index));
		Vector3 parentPosition = menu.transform.GetChild(0).transform.GetChild (index).transform.position;
		toggle.GetComponent<Toggle> ().group=menu.transform.GetChild(0).transform.GetChild (index).GetComponent<ToggleGroup>();
		toggle.GetComponent<Toggle> ().onValueChanged.AddListener(delegate{item.consume(this); if(item.getName()=="Potion"){Destroy (toggle.GetComponent<Toggle> ().gameObject);}});
		toggle.transform.GetChild (1).GetComponent<Text> ().text = item.getName () + "("+value+")";
		toggle.transform.position = new Vector3(parentPosition.x, parentPosition.y+150-(35*length), parentPosition.z);
	}

	private void potionConsume(Potion potion, Toggle toggle){
		potion.consume (this);
		Destroy (toggle.gameObject);
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "PNJ") {
			WalkerPNJ pnj = other.collider.GetComponent<WalkerPNJ> ();
			pnj.talkTo ();
		}
		if (other.gameObject.tag == "Item") {
			Item otherItem = other.collider.GetComponent<Item> ();
			addItem (otherItem);
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

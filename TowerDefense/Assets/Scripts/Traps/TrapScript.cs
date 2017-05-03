using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapScript : MonoBehaviour {

	public string currentTrap;
	Renderer rend;
	Text sellText;

	void Awake () {
		currentTrap = "none";
		rend = GetComponent<Renderer> ();
		rend.enabled = false;
		sellText = GameObject.FindGameObjectWithTag ("SellText").GetComponent<Text>();
		sellText.enabled = false;
	}

	void Update () {
		
	}

	public void Light (Material mat) {
		//!!! Add a visible part to the weapon square that is above the surface to turn red and green.  Only change if currently active is different from request.
		rend.sharedMaterial = mat;
		rend.enabled = true;
	}

	public void displaySellMessage () {
		sellText.enabled = true;
	}

	public void deselect () {
		//perform actions that should be done when this trap square is deselected.  This includes:
		//removing the lighting up
		//removing the sell message
		//these things may be readded within the same processing frame, but it's not a big deal.
		rend.enabled = false;
		sellText.enabled = false;
	}

	public void construct (string trap) {
		if (trap == "spike") {
			currentTrap = "spike";
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		}
	}

	public void sell () {
		if (currentTrap == "spike") {
			currentTrap = "none";
			this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		}
	}
}

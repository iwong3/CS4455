using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float hitPoint = 100f;
	float currentHitPoints;
	// Use this for initialization
	void Start () {
		currentHitPoints = currentHitPoints;
	}

	public void TakeDamage(float amt){
		currentHitPoints -= amt;

		if(currentHitPoints <= 0){
			Die();
		}
	}

	void Die(){
		Destroy (gameObject); 
	}


	// Update is called once per frame
	void Update () {
		
	}
}

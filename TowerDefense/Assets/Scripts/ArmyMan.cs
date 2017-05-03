using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmyMan : MonoBehaviour {

	NavMeshAgent agent;
	Animator anim;
	string state; //default is heading for exit, aggressive is attacking player
	int HP;
	float timer;

	Transform player;
	PlayerHealth playerHealth;
	NexusStats nexusStats;
	Transform goal;

	public int maxHP = 100;
	public float aggrodist = 150; //distance at which enemy targets players instead
	public float leashdist = 200; //distance at which enemy returns to default behaviour
	public int power = 10;
	public int nexusAttack = 1;
	public float timeBetweenAttacks = 1.5f;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		anim.SetFloat ("Forward", 1);
		state = "default";
		HP = maxHP;
		timer = 0;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerHealth> ();
		goal = GameObject.FindGameObjectWithTag ("Nexus").GetComponent<Transform>();
		nexusStats = GameObject.FindGameObjectWithTag ("Nexus").GetComponent<NexusStats> ();
		agent.destination = goal.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dist = Vector3.Distance (transform.position, player.position);
		if (dist <= aggrodist) {
			state = "aggressive";
		} else if (dist >= leashdist) {
			state = "default";
			agent.destination = goal.position;
		}
		if (state == "aggressive") {
			agent.destination = player.position;
		} else {
			agent.destination = goal.position;
		}
		timer += Time.deltaTime;

	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Player" && timer >= timeBetweenAttacks) {
			playerHealth.TakeDamage (power);
			timer = 0;
		}
		if (other.gameObject.tag == "Nexus") {
			nexusStats.TakeDamage (nexusAttack);
			Destroy (gameObject);
		}
	}

	void OnCollisionStay (Collision other) {
		if (other.gameObject.tag == "Player" && timer >= timeBetweenAttacks) {
			playerHealth.TakeDamage (power);
			timer = 0;
		}
		if (other.gameObject.tag == "Nexus") {
			nexusStats.TakeDamage (nexusAttack);
			Destroy (gameObject);
		}
	}

	public void TakeDamage (int damage) {
		HP -= damage;
		if (HP <= 0) {
			Destroy (gameObject);
		}
	}
}

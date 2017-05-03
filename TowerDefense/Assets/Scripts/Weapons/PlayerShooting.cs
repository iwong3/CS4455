using UnityEngine;
using System.Collections;

//also handles trap placement and records money
public class PlayerShooting : MonoBehaviour {

	public float timeBetweenBullets = 0.3f;
	float timer;
	public int damage = 13;
	public float range = 100000f;
	int attackableMask, trapMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
	public GameObject player;
	Animator playerAnim;
	Animator gunAnim;
	public int money;

	//trap related variables
	public string selectedTrap;
	int spikePrice = 200;
	TrapScript trapScript;
	public Material red;
	public Material green;

	GameManager1 gameManager;

	void Awake ()
	{
		attackableMask = LayerMask.GetMask ("Attackable");
		trapMask = LayerMask.GetMask ("Traps");
		//gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		//gunLight = GetComponent <Light> ();
		playerAnim = player.GetComponent<Animator> ();
		//gunAnim = GetComponentInParent<Animator> ();
		selectedTrap = "none";
		money = 0;
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager1> ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//if(WeaponSwitcher.gunEquipped && Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			// Player wants to shoot...so. Shoot.
			Fire ();
		} else if (!Input.GetButton ("Fire1")) {
			playerAnim.SetBool ("Shoot", false);
		} 

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}

		//call on trap square checker.  When something that isn't a trap is selected, set selectedTrap to none.
		if (selectedTrap != "none") {
			SeekTrap ();
		}


	}

	public void DisableEffects ()
	{
		gunLine.enabled = false;
	}
		
	void Fire() {

		playerAnim.SetBool ("Shoot", true);


		timer = 0f;

		gunAudio.Play();



		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		Ray shootRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit shootHit;

		//		Transform hitTransform;
		//		Vector3   hitPoint;
		//
		//		hitTransform = FindClosestHitObject(shootRay, out hitPoint);

		if(Physics.Raycast (shootRay, out shootHit, range, attackableMask))
		{
			Debug.Log ("We hit: " + shootHit.collider.name);
			ArmyMan armyMan = shootHit.collider.GetComponent<ArmyMan> ();

			if(armyMan != null)
			{
				armyMan.TakeDamage (damage);
			}

			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			Debug.Log ("no hit");
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}

		//		if(hitTransform != null) {
		//			Debug.Log ("We hit: " + hitTransform.name);
		//
		//			// We could do a special effect at the hit location
		//			// DoRicochetEffectAt( hitPoint );
		//
		//			Health h = hitTransform.GetComponent<Health>();
		//
		//			while(h == null && hitTransform.parent) {
		//				hitTransform = hitTransform.parent;
		//				h = hitTransform.GetComponent<Health>();
		//			}
		//
		//			// Once we reach here, hitTransform may not be the hitTransform we started with!
		//
		//			if(h != null) {
		//				h.TakeDamage( damage );
		//			}
		//
		//
		//		}
		//
		//		cooldown = fireRate;
	}

	void SeekTrap() {
		trapScript.deselect ();
		trapScript = null;

		Ray shootRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit shootHit;

		if(Physics.Raycast (shootRay, out shootHit, range, trapMask)) {
			trapScript = shootHit.collider.GetComponent<TrapScript> ();

			if(trapScript != null) {
				if (trapScript.currentTrap == "none") {
					if (selectedTrap == "spike" && money >= spikePrice) {//add cases per trap added
						trapScript.Light (green);
					} else {
						trapScript.Light (red);
					}
				} else if (gameManager.getState() == 3) {
					trapScript.displaySellMessage ();
				}

				if (Input.GetButton ("Fire1")) {
					if (trapScript.currentTrap == "none") {
						if (selectedTrap == "spike" && money >= spikePrice) {//add cases per trap added
							money -= spikePrice; //probably should add some call to money ui too
							trapScript.construct("spike");
						}
					}
				}
				if (gameManager.getState() == 3 && Input.GetButton ("Sell")) {
					//should add call to money ui
					if (trapScript.currentTrap == "spike") {
						money += spikePrice / 2;
					}
					trapScript.sell ();
				}
			}


		}
	}
//
//	Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint) {
//
//		RaycastHit[] hits = Physics.RaycastAll(ray);
//
//		Transform closestHit = null;
//		float distance = 0;
//		hitPoint = Vector3.zero;
//
//		foreach(RaycastHit hit in hits) {
//			if(hit.transform != this.transform && ( closestHit==null || hit.distance < distance ) ) {
//				// We have hit something that is:
//				// a) not us
//				// b) the first thing we hit (that is not us)
//				// c) or, if not b, is at least closer than the previous closest thing
//
//				closestHit = hit.transform;
//				distance = hit.distance;
//				hitPoint = hit.point;
//			}
//		}
//
//		// closestHit is now either still null (i.e. we hit nothing) OR it contains the closest thing that is a valid thing to hit
//
//		return closestHit;
//
//	}
}

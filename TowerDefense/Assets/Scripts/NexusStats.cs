using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour {

    public static int nexusHealth;
    public int startHealth = 5;
    public int damage = 1;

    [SerializeField]
    public AudioSource nexusDamageAudio;

	// Use this for initialization
	void Awake () {
        nexusHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage (int damage) {
		nexusHealth -= damage;
        nexusDamageAudio.Play();
	}

}

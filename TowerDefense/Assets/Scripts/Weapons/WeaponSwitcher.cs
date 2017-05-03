using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {
	[Header("-Sword First,Wand Second-")]
	public GameObject[] weapons;
	public int holding = 0;
//    public static bool swordEquipped;

//	public GameObject wand;
//    public static bool gunEquipped;

    // Use this for initialization
    void Start () {
		if (weapons.Length!=2) {
			Debug.Log ("[WeaponSwitcher]Weapon not assigned!");
		}
		for (int i=0; i < weapons.Length; i++) {
			if (i == holding) {
				weapons [i].SetActive (true);
			} else {
				weapons [i].SetActive (false);
			}
		}

//		wand.SetActive(true);
//      gunEquipped = true;
//      sword.SetActive(false);
//      swordEquipped = false;
    }
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			holding++;
			if (holding == weapons.Length) {
				holding = 0;
			}
			ChangeWeapon ();
//			weapons[holding].SetActive= 
		}
//		if (Input.GetKeyDown("1"))
//        {
//			wand.SetActive(true);
//            gunEquipped = true;
//            sword.SetActive(false);
//            swordEquipped = false;
//        }
//        if (Input.GetKeyDown("2"))
//        {
//			wand.SetActive(false);
//            gunEquipped = false;
//            sword.SetActive(true);
//            swordEquipped = true;
//        }
    }
	void ChangeWeapon(){
		for (int i=0; i < weapons.Length; i++) {
			if (i == holding) {
				weapons [i].SetActive (true);
			} else {
				weapons [i].SetActive (false);
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayCredits : MonoBehaviour {
	public MovieTexture movie;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;	
		movie.Play();
		StartCoroutine(ReturnMenu ());
	}

	IEnumerator ReturnMenu (){
		yield return new WaitForSeconds (33);
		SceneManager.LoadScene ("Menu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

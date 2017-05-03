using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;


    Animator anim;
    //AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    //bool damaged;

    [SerializeField]
    public AudioSource playerTakeDamageAudio;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        //playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        /*if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;*/
    }


    public void TakeDamage (int amount)
    {
        //damaged = true;

        currentHealth -= amount;
        playerTakeDamageAudio.Play();

        //healthSlider.value = currentHealth;

        //playerAudio.Play ();

        if (currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        //anim.SetTrigger ("Die"); !!!doesn't exist yet, should add.

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}

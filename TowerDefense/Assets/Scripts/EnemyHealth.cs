using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;    //When the enemy dies, it will sink through the floor to be less funky.
    public int scoreValue = 50;
    public AudioClip deathClip;

	Transform tf;
    Animator anim;
    //AudioSource enemyAudio;
//    ParticleSystem hitParticles;
	BoxCollider boxCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
		tf = GetComponent <Transform> ();
//        anim = GetComponent <Animator> ();
//        enemyAudio = GetComponent <AudioSource> ();
//        hitParticles = GetComponentInChildren <ParticleSystem> ();
        boxCollider = GetComponent <BoxCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
		{	
			//Translate is different from the Movement. It doesn't go along with physics. And since when the enemy dies, the animation is just an animation, not following physics, we can use this function.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); 
		}
    }


	public void TakeDamage (int amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        if(isDead)
            return;

//        enemyAudio.Play ();

		tf.position = tf.position + hitDirection;

        currentHealth -= amount;
            
//        hitParticles.transform.position = hitPoint;
//        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        boxCollider.isTrigger = true;  //So that the collider is no longer physically present.
		Destroy (gameObject, 2f);
//        anim.SetTrigger ("Dead");

//        enemyAudio.clip = deathClip;
//        enemyAudio.Play ();
    }


//    public void StartSinking ()
//    {
////		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; //enabled for components; setActive for game object
////        GetComponent <Rigidbody> ().isKinematic = true;
////        isSinking = true;
//        Destroy (gameObject, 2f);
//    }
}

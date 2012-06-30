using UnityEngine;
using System.Collections;

public class ZombieRoamingAIController : MonoBehaviour {

	public float speed = 1;
	public AudioClip[] sounds;
	
	private CharacterController movementController;
	private AudioSource audioSource;
	
	void Awake() {
		movementController = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();
	}
		
	void Start() {
		ChangeDirection();
		RandomSoundPlayer();
	}
	
	public void Update () {
		movementController.SimpleMove(transform.rotation * Vector3.forward * speed);
	}
	
	private void ChangeDirection() {
		transform.rotation = Quaternion.Euler(0, Random.RandomRange(0F, 360F), 0);
		Invoke("ChangeDirection", Random.RandomRange(5, 10));
	}
	
	private void RandomSoundPlayer() {
		audioSource.clip = sounds[Random.Range(0,sounds.Length - 1)];
		audioSource.Play();
		
		Invoke("RandomSoundPlayer", Random.Range(4,10));
	}
	
	public void OnChangeAIStrategy( string strategy ) {
		enabled = (strategy == "Roaming");
		
		if(enabled) {	//Initialize direction change loop
			gameObject.animation.Play("Walking");
			ChangeDirection();
			RandomSoundPlayer();
		} else {
			CancelInvoke();
		
		}
	}

}
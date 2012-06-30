using UnityEngine;
using System.Collections;

public class ZombieChasingAIController : MonoBehaviour {
	public float speed = 2;
	public AudioClip[] sounds;
	
	private CharacterController movementController;
	private AudioSource audioSource;
	
	void Awake() {
		movementController = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();
	}
	
	public void Update () {
		transform.LookAt(GameController.instance.player.transform);
		
		Vector3 angles = transform.localEulerAngles;
		angles.x = 0;
		transform.localEulerAngles = angles;
	
		movementController.SimpleMove(transform.rotation * Vector3.forward * speed);
	}
	
	private void RandomSoundPlayer() {
		audioSource.clip = sounds[Random.Range(0,sounds.Length - 1)];
		audioSource.Play();
		
		Invoke("RandomSoundPlayer", Random.Range(4,10));
	}
	
	public void OnChangeAIStrategy( string strategy ) {
		enabled = (strategy == "Chasing");
		
		if(enabled) {
			gameObject.animation.Play("Walking");
			RandomSoundPlayer();
		} else {
			CancelInvoke();
		}
	}
}

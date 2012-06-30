using UnityEngine;
using System.Collections;

public class ZombieAttackingAIController : MonoBehaviour {
	public int damage = 1;
	public AudioClip[] sounds;
	
	private EntityHealthController playerHealthController;
	private AudioSource audioSource;
	
	void Awake() {
		playerHealthController = GameController.instance.player.GetComponent<EntityHealthController>();
		audioSource = GetComponent<AudioSource>();
	}
	
	public void Attack() {
		playerHealthController.Damage(damage);
		RandomSoundPlayer();
	}
	
	private void RandomSoundPlayer() {
		audioSource.clip = sounds[Random.Range(0,sounds.Length - 1)];
		audioSource.Play();
	}
	
	public void OnChangeAIStrategy( string strategy ) {
		enabled = (strategy == "Attacking");
		
		if(enabled) {
			gameObject.animation.Play("Eating");
			InvokeRepeating("Attack", 0, 1);
		} else {
			CancelInvoke();
		}
	}
}

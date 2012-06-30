using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject weapon;
	public AudioClip gunshot;

	private bool isReloading = false;
	private AudioSource audioSource;


	public void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = gunshot;
		
		UpdateWeaponState();
	}

	public void Update() {
	
		if( !isReloading && Input.GetMouseButton(0) ) {
			StartReloading();
			audioSource.Play();
	
			Vector3 direction = transform.TransformDirection(Vector3.forward);
			RaycastHit hit;
			
			if( Physics.Raycast(transform.position, direction, out hit) ) {
				EntityHealthController healthController = hit.transform.GetComponent<EntityHealthController>();
				
				if( healthController != null ) {
					healthController.Damage(Random.RandomRange(15,35));
				}
				
				
				if( hit.collider.rigidbody != null ) {
					hit.collider.rigidbody.AddForce(transform.rotation * Vector3.forward);
				}
			}
			
		}
	}
	
	#region "Animation controls"

	private void StartReloading() {
		isReloading = true;
		UpdateWeaponState();
	
		Invoke("FinishReloading", 2);
	}

	private void FinishReloading() {
		isReloading = false;
		UpdateWeaponState();
	}

	private void UpdateWeaponState() {
		weapon.animation.Play(isReloading?"Disabled":"Idle");
	}
	
	#endregion

	public void Die() {
		GameController.instance.KillPlayer();
	}
}

using UnityEngine;
using System.Collections;

public class ZombieStunnedAIController : MonoBehaviour {

	public IEnumerator OnChangeAIStrategy( string strategy ) {
		enabled = (strategy == "Stunned");
		
		if(enabled) {
			gameObject.animation.Play("Stunned");
			
			yield return new WaitForSeconds(2);
			
			SendMessage("OnRecover", SendMessageOptions.DontRequireReceiver);
		}
	}
}

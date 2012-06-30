using UnityEngine;
using System.Collections;

public class EntityHealthController : MonoBehaviour {

	public int health = 100;
	
	public void Damage( int amount ) {
		health -= amount;
		health = Mathf.Clamp(health, 0, 100);
		
		if(health == 0) {
			SendMessage("Die", SendMessageOptions.DontRequireReceiver);
		} else {
			SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
		}
	}
}

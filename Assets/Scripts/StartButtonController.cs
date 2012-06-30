using UnityEngine;
using System.Collections;

public class StartButtonController : MonoBehaviour {
	public Texture texture;

	public void OnGUI() {
		Vector2 origin = new Vector2(Screen.width / 2 + 150, Screen.height / 2 - 150);
		Vector2 size = new Vector2(250, 60);
		
		if (GUI.Button(new Rect(origin.x,origin.y,size.x,size.y),texture))
	        GameController.instance.StartGame();
	}
}

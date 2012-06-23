#pragma strict

var texture : Texture;

function OnGUI() {
	var origin = new Vector2(Screen.width / 2 + 150, Screen.height / 2 - 150);
	var size = new Vector2(250, 60);
	
	if (GUI.Button(Rect(origin.x,origin.y,size.x,size.y),texture))
        GameController.instance.StartGame();
}
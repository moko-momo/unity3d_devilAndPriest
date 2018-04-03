using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class UserGUI : MonoBehaviour  
{  
	private IUserAction action;  
	// Use this for initialization  
	void Start () {  
		action = Director.getInstance().currentSceneControl as IUserAction;  
	}  

	private void OnGUI()  
	{  
		if (GUI.Button(new Rect(700, 100, 90, 90), "GO") && action.getGameState() == GameState.NOT_ENDED)  
		{  
			action.MoveBoat();  
		}  
		if (action.getGameState() == GameState.WIN)  
		{  
			GUI.Label(new Rect(700, 300, 400, 400), "you win");  
			GUI.color = Color.red;  
		}  
		if ((action.getGameState() == GameState.FAILED)) {  
			action.GameOver();  
		}  
	}  


}  

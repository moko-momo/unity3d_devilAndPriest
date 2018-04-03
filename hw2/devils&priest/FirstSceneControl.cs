using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class FirstSceneControl : MonoBehaviour, ISceneControl, IUserAction {  

	// Use this for initialization  

	public enum BoatState { MOVING, STOPLEFT,STOPRIGHT}  
	public GameObject Shore_l;  
	public GameObject Shore_r;  
	public GameObject boat;  
	public Dictionary<int, GameObject> On_Boat = new Dictionary<int, GameObject>();  
	public Dictionary<int, GameObject> On_Shore_r = new Dictionary<int, GameObject>();  
	public Dictionary<int, GameObject> On_Shore_l = new Dictionary<int, GameObject>();  

	public GameState game_state;  
	float gab = 1.5f;  
	public int boat_capicity;  
	public BoatState b_state;  
	void Awake () {  
		Director director = Director.getInstance();  
		director.currentSceneControl = this;  
		director.currentSceneControl.GenGameObjects();  

	}  


	public void GenGameObjects()  
	{  
		Shore_l = GameObject.CreatePrimitive(PrimitiveType.Cube);  
		Shore_l.name = "Shore_l";  
		Shore_l.transform.localScale = new Vector3(10, 2, 1);  
		Shore_l.transform.position = new Vector3(-17, -9, 0);  
		Shore_r = Instantiate<GameObject>(Shore_l, new Vector3(17, -9, 0), Quaternion.identity);  
		Shore_r.name = "Shore_r";  

		boat = GameObject.CreatePrimitive(PrimitiveType.Cube); ;  
		boat.transform.localScale = new Vector3(3, 1, 1);  
		boat.transform.position = new Vector3(10.4f, -9.5f, 0);  
		boat.name = "boat";  

		GameObject temp_priest = GameObject.CreatePrimitive(PrimitiveType.Sphere);  
		temp_priest.transform.localScale = new Vector3(1, 1, 1);  
		temp_priest.AddComponent<On_Off>();  

		GameObject temp_devil = GameObject.CreatePrimitive(PrimitiveType.Cube);  
		temp_devil.transform.localScale = new Vector3(1, 1, 1);  
		temp_devil.AddComponent<On_Off>();  


		for (int i = 0; i < 3; i++)  
		{  
			
			On_Shore_r.Add(i, Instantiate<GameObject>(temp_priest, new Vector3(12.5f + i * gab, -7.5f, 0), Quaternion.identity));  
			On_Shore_r[i].name = i.ToString();  
		}  

		for (int i = 3; i < 6; i++)  
		{  
			
			GameObject tmp = Instantiate<GameObject>(temp_devil, new Vector3(12.5f + i * gab, -7.5f, 0), Quaternion.identity);  
			tmp.name = i.ToString();  
			tmp.GetComponent<Renderer>().material.color = Color.red;  
			On_Shore_r.Add(i, tmp);  
		}  

		boat_capicity = 2;  
		b_state = BoatState.STOPLEFT;  
		Destroy(temp_devil);  
		Destroy(temp_priest);  

	}  

	private void Update()  
	{  
		game_state = check();  
		if (game_state == GameState.NOT_ENDED)  
		{  
			if (boat.transform.position.x < 10.2f && boat.transform.position.x > -10.2f)  
			{  
				b_state = BoatState.MOVING;  
			}  
			else if (boat.transform.position.x > 10.2f)  
			{  
				b_state = BoatState.STOPRIGHT;  
			}  
			else  
			{  
				b_state = BoatState.STOPLEFT;  
			}  

			for (int i = 0; i < 6; i++)  
			{  
				if (On_Shore_l.ContainsKey(i)) On_Shore_l[i].transform.position = new Vector3(-12.5f - i * gab, -7.5f, 0);  

				if (On_Shore_r.ContainsKey(i)) On_Shore_r[i].transform.position = new Vector3(12.5f + i * gab, -7.5f, 0);  
			}  
			int signed = 1;  
			for (int i = 6; i < 12; i++)  
			{  
				if (On_Boat.ContainsKey(i))  
				{  
					On_Boat[i].transform.localPosition = new Vector3(signed * 0.3f, 1, 0);  
					signed = -signed;  
				}  
			}  
		}  
	}  

	public void MoveBoat()  
	{  
		if (On_Boat.Count != 0)  
		{  
			if (b_state == BoatState.STOPLEFT)  
			{  
				
				boat.transform.position = new Vector3(10.3f, -9.5f, 0);  
				  
			}  

			if (b_state == BoatState.STOPRIGHT)  
			{  
				  
				boat.transform.position = new Vector3(-10.3f, -9.5f, 0);  

			}  
		}  

	}  



	public void GameOver()  
	{  
		GUI.color = Color.red;  
		GUI.Label(new Rect(700, 300, 400, 400), "GAMEOVER");  

	}  

	public int get_num(Dictionary<int, GameObject> dict, int ch)  
	{  
		var keys = dict.Keys;  
		int d_num = 0;  
		int p_num = 0;  
		foreach(int i in keys)  
		{  
			if (i < 3 || (i >= 6 && i <= 8))  
			{  
				p_num++;  
			}  
			else  
			{  
				d_num++;  
			}  
		}  
		return (ch == 1 ? p_num : d_num);  
	}  

	GameState check()  
	{  

		if (On_Shore_l.Count == 6)  
		{  
			return GameState.WIN;  
		}  

		else if (b_state == BoatState.STOPLEFT)  
		{  

			if (get_num(On_Boat, 1) + get_num(On_Shore_l, 1) != 0  
				&& get_num(On_Boat, 1) + get_num(On_Shore_l, 1) < (get_num(On_Boat, -1) + get_num(On_Shore_l, -1)))  
			{  

				return GameState.FAILED;  
			}  
			if(get_num(On_Shore_r, 1) != 0 && get_num(On_Shore_r, 1) < get_num(On_Shore_r, -1))  
			{  

				return GameState.FAILED;  
			}  
		}  

		else if (b_state == BoatState.STOPRIGHT)  
		{  
			if (get_num(On_Boat, 1) + get_num(On_Shore_r, 1) != 0  
				&& get_num(On_Boat, 1) + get_num(On_Shore_r, 1) < (get_num(On_Boat, -1) + get_num(On_Shore_r, -1)))  
			{  

				return GameState.FAILED;  
			}  
			if (get_num(On_Shore_l, 1) != 0 && get_num(On_Shore_l, 1) < get_num(On_Shore_l, -1))  
			{  

				return GameState.FAILED;  
			}  
		}  
		return GameState.NOT_ENDED;  

	}  

	public GameState getGameState()  
	{  
		return game_state;  
	}  
}  
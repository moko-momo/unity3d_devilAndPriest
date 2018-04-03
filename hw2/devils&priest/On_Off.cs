using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class On_Off : MonoBehaviour  
{  


	private FirstSceneControl firstSceneControl;  
	enum Pos { ON_BOAT, ON_SHORE}  

	Pos find_Pos(int id)  
	{  
		if (id >= 6) return Pos.ON_BOAT;  
		return Pos.ON_SHORE;  
	}  

	void Start()  
	{  
		firstSceneControl = (FirstSceneControl)Director.getInstance().currentSceneControl;  
	}  


	private void OnMouseDown()  
	{  
		if (firstSceneControl.game_state == GameState.NOT_ENDED)  
		{  
			if (firstSceneControl.b_state == FirstSceneControl.BoatState.MOVING) return;  

			int id = Convert.ToInt32(this.name);  
			if (firstSceneControl.b_state == FirstSceneControl.BoatState.STOPRIGHT)  
			{  
				if (firstSceneControl.On_Shore_r.ContainsKey(id))  
				{  
					if (find_Pos(id) == Pos.ON_SHORE && firstSceneControl.boat_capicity != 0)  
					{  
						firstSceneControl.On_Boat.Add(id + 6, firstSceneControl.On_Shore_r[id]);  
						firstSceneControl.On_Shore_r.Remove(id);  
						this.name = (id + 6).ToString();  
						this.transform.parent = firstSceneControl.boat.transform;  
						firstSceneControl.boat_capicity--;  
					}  
				}  


				if (find_Pos(id) == Pos.ON_BOAT)  
				{  

					firstSceneControl.On_Shore_r.Add(id - 6, firstSceneControl.On_Boat[id]);  
					firstSceneControl.On_Boat.Remove(id);  
					this.name = (id - 6).ToString();  
					this.transform.parent = null;  
					firstSceneControl.boat_capicity++;  
				}  
			}  
			if (firstSceneControl.b_state == FirstSceneControl.BoatState.STOPLEFT)  
			{  

				if (find_Pos(id) == Pos.ON_SHORE && firstSceneControl.boat_capicity != 0)  
				{  
					if (firstSceneControl.On_Shore_l.ContainsKey(id))  
					{  
						firstSceneControl.On_Boat.Add(id + 6, firstSceneControl.On_Shore_l[id]);  
						firstSceneControl.On_Shore_l.Remove(id);  
						this.name = (id + 6).ToString();  
						this.transform.parent = firstSceneControl.boat.transform;  
						firstSceneControl.boat_capicity--;  
					}  

				}  

				if (find_Pos(id) == Pos.ON_BOAT)  
				{  
					firstSceneControl.On_Shore_l.Add(id - 6, firstSceneControl.On_Boat[id]);  
					firstSceneControl.On_Boat.Remove(id);  
					this.name = (id - 6).ToString();  
					this.transform.parent = null;  
					firstSceneControl.boat_capicity++;  
				}  
			}  


		}  
	}      
}  


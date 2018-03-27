using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {


	public bool turn = true; 
	public bool gameOver = false;
	public string End = " ";

	public int[,] winStep = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

	public class piece{
		public bool isFill = false;
		public string label = " ";
	};
		
	public List<piece> XMB = new List<piece> ();

	void initial(){
		for (int i = 0; i < 9; i++) {
			piece temp = new piece();
			temp.isFill = false;

			temp.label = " ";
			XMB.Add (temp);
		}

	}



	// Use this for initialization
	void Start () {
		initial ();
	}

	void OnGUI(){
		if (gameOver) {
			if (GUI.Button (new Rect (100, 350, 50, 50), "Reset")) {
				gameOver = false;
				reset ();
			}
			for (int i = 0; i < 9; i++) {
				GUI.Button (new Rect (i % 3 * 100, i / 3 * 100, 100, 100), XMB [i].label);
			}
			GUI.color = Color.red;
			GUI.Label (new Rect (100, 300, 50, 50), checkWin ());

		}
		else{
			for (int i = 0; i < 9; i++) {			
				if (XMB[i].isFill) {
					GUI.Button (new Rect (i % 3 * 100, i / 3 * 100, 100, 100), XMB[i].label);
				}
				else{
					if (GUI.Button (new Rect (i % 3 * 100, i / 3 * 100, 100, 100), " ")) {
						XMB[i].isFill = true;
						if (turn) {
							XMB [i].label = "X";
							turn = false;
						} else {
							XMB[i].label = "O";
							turn = true;
						}
						GUI.Button (new Rect (i % 3 * 100, i / 3 * 100, 100, 100), XMB[i].label);

						if (checkWin ().Equals (null)) {
							
						} else {
							gameOver = true;
						}




					}
				}

			}
		}




	}


	// Update is called once per frame
	void Update () {
		
	}

	string checkWin() {
		for (int i = 0; i < 8; i++) {			
			if (XMB [winStep [i, 0]].isFill && XMB [winStep [i, 1]].isFill && XMB [winStep [i, 2]].isFill) {
				if (XMB [winStep [i, 0]].label.Equals (XMB [winStep [i, 1]].label) && XMB [winStep [i, 0]].label.Equals (XMB [winStep [i, 2]].label)) {
					return (XMB [winStep [i, 0] ].label + " win!");
				}
			}
		}

		int counter = 0;
		for (int i = 0; i < 9; i++) {
			if (XMB [i].isFill.Equals(true)) {
				counter = counter + 1;
			}
		}
		if (counter.Equals (9)) {
			return "Draw!";
		} 

		return null;

	}

	void reset(){
		for (int i = 0; i < 9; i++) {
			XMB [i].isFill = false;
			XMB[i].label = " ";
		}
	}



}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager> 
{
	#region Fields
	private List<Player> players = new List<Player>();
	private Player currentPlayer;
	#endregion

	#region Properties
	public Player CurrentPlayer { get { return currentPlayer; } }
	#endregion

	protected GameManager(){}
	
	void Awake () 
	{
		GameObject playersObject = (GameObject)GameObject.Find("Players");
		for (int i = 0; i < playersObject.transform.childCount; i++)
		{
			
			players.Add(playersObject.transform.GetChild(i).gameObject.GetComponent<Player>());
		}
	}


}
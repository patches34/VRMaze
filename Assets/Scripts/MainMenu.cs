using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
	bool matchCreated;
	NetworkMatch networkMatch;

	public SearchListPanel searchPanel;

	public LevelBuilder builder;

	public NetworkManager networkMan;

	MatchInfo currentMatch;

	void Awake()
	{
		networkMatch = gameObject.AddComponent<NetworkMatch>();
	}

	public void CreateRoom()
	{
		string matchName = "Test Room";
		uint matchSize = 4;
		bool matchAdvertise = true;
		string matchPassword = "";

		networkMatch.CreateMatch(matchName, matchSize, matchAdvertise, matchPassword, "", "", 0, 0, OnMatchCreate);
	}

	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Create match succeeded");
			matchCreated = true;
			NetworkServer.Listen(matchInfo, 9000);
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);

			builder.gameObject.SetActive(true);

			networkMan.StartHost(matchInfo);
		}
		else
		{
			Debug.LogError("Create match failed: " + extendedInfo);
		}
	}

	public void ListRooms()
	{
		networkMatch.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success && matches != null && matches.Count > 0)
		{
			searchPanel.Setup(matches);
		}
		else if (!success)
		{
			Debug.LogError("List match failed: " + extendedInfo);
		}
	}

	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(matchInfo);
			currentMatch = matchInfo;
		}
		else
		{
			Debug.LogError("Join match failed " + extendedInfo);
		}
	}

	public void OnConnected(NetworkMessage msg)
	{
		Debug.Log("Connected!");

		builder.gameObject.SetActive(true);

		networkMan.StartClient(currentMatch);
	}

	public void JoinGame(MatchInfoSnapshot info)
	{
		networkMatch.JoinMatch(info.networkId, "", "", "", 0, 0, OnMatchJoined);
	}
}
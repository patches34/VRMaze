using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CBGNetworkManager : NetworkManager
{
	public NetworkStartPosition playerSpawnPos;

	public GameObject currentPlayerObj;

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		Debug.Log("player spawn");

		base.OnServerAddPlayer (conn, playerControllerId, extraMessageReader);
	}
}

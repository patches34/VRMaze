using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour, RealTimeMultiplayerListener
{
	public LevelBuilder builder;

	private bool showingWaitingRoom = false;

	// Use this for initialization
	void Start ()
	{
		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			//builder.gameObject.SetActive(success);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateGame()
	{
		const int MinOpponents = 1, MaxOpponents = 3;
		const int GameVariant = 0;
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
			GameVariant, this);
	}

	/// <summary>
	/// Called during room setup to notify of room setup progress.
	/// </summary>
	/// <param name="percent">The room setup progress in percent (0.0 to 100.0).</param>
	public void OnRoomSetupProgress(float percent)
	{
		
	}

	/// <summary>
	/// Notifies that room setup is finished. If <c>success == true</c>, you should
	/// react by starting to play the game; otherwise, show an error screen.
	/// </summary>
	/// <param name="success">Whether setup was successful.</param>
	public void OnRoomConnected(bool success)
	{
		
	}

	/// <summary>
	/// Notifies that the current player has left the room. This may have happened
	/// because you called LeaveRoom, or because an error occurred and the player
	/// was dropped from the room. You should react by stopping your game and
	/// possibly showing an error screen (unless leaving the room was the player's
	/// request, naturally).
	/// </summary>
	public void OnLeftRoom()
	{

	}

	/// <summary>
	/// Raises the participant left event.
	/// This is called during room setup if a player declines an invitation
	/// or leaves.  The status of the participant can be inspected to determine
	/// the reason.  If all players have left, the room is closed automatically.
	/// </summary>
	/// <param name="participant">Participant that left</param>
	public void OnParticipantLeft(Participant participant)
	{

	}

	/// <summary>
	/// Called when peers connect to the room.
	/// </summary>
	/// <param name="participantIds">Participant identifiers.</param>
	public void OnPeersConnected(string[] participantIds)
	{

	}

	/// <summary>
	/// Called when peers disconnect from the room.
	/// </summary>
	/// <param name="participantIds">Participant identifiers.</param>
	public void OnPeersDisconnected(string[] participantIds)
	{

	}

	/// <summary>
	/// Called when a real-time message is received.
	/// </summary>
	/// <param name="isReliable">Whether the message was sent as a reliable message or not.</param>
	/// <param name="senderId">Sender identifier.</param>
	/// <param name="data">Data.</param>
	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
	{

	}
}

using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour, IGvrGazeResponder
{
	public bool isSafe;

	public Material inactiveMaterial;
	public Material gazedAtMaterial;

	PlayerMove player;

	void Start() 
	{
		SetGazedAt(false);

		player = FindObjectOfType<PlayerMove>();
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
			return;
		}
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
	}

	public void TeleportHere() {
		player.WarpTo(transform.position, isSafe);
	}

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() 
	{
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() 
	{
		SetGazedAt(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
	public void OnGazeTrigger() {
		TeleportHere();
	}

	#endregion
}
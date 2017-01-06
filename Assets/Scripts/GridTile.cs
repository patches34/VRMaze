using UnityEngine;
using System.Collections.Generic;
using System;

[System.Flags]
public enum CardinalCoordinates
{
	None = 0,
	North = 1,
	South = 2,
	East = 4,
	West = 8
}

public class GridTile : MonoBehaviour
{
	Player player;

	public List<WallGaze> sideWalls;

	public Renderer rendGaze;
	public Material inactiveMaterial;
	public Material gazedAtMaterial;

	public GameObject selectedLight;

	public GameObject Ui;

	void Awake()
	{
		player = FindObjectOfType<Player>();

		sideWalls = new List<WallGaze>();
		for(int i = 2; i < transform.childCount; ++i)
		{
			sideWalls.Add(transform.GetChild(i).GetComponent<WallGaze>());
		}
	}

	public void SetWalls(CardinalCoordinates coordMask)
	{
		sideWalls[0].SetIsWall((coordMask & CardinalCoordinates.North) != CardinalCoordinates.None);

		sideWalls[1].SetIsWall((coordMask & CardinalCoordinates.South) != CardinalCoordinates.None);

		sideWalls[2].SetIsWall((coordMask & CardinalCoordinates.East) != CardinalCoordinates.None);

		sideWalls[3].SetIsWall((coordMask & CardinalCoordinates.West) != CardinalCoordinates.None);
	}

	public void SetGazedAt(bool gazedAt)
	{
		//selectedLight.SetActive(gazedAt);

		if (inactiveMaterial != null && gazedAtMaterial != null)
		{
			rendGaze.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
			return;
		}
		else
		{
			Debug.LogError("Materials not set");
		}
	}

	public void MoveTo()
	{
		ShowUi(false);

		player.MoveTo(this);
	}

	public void FinishMoveTo()
	{
		ShowUi(true);
	}

	void ShowUi(bool value)
	{
		if(Ui != null)
		{
			Ui.SetActive(value);
		}
	}

	public void Reset()
	{
		ShowUi(false);
	}
}
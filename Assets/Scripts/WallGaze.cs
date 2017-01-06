using UnityEngine;
using System.Collections;

public class WallGaze : MonoBehaviour
{
	GridTile rootTile;

	public GameObject front, back;

    // Use this for initialization
    void Awake()
    {
		rootTile = GetComponentInParent<GridTile>();
    }

	public void SetIsWall(bool value)
	{
		front.SetActive(value);

		back.SetActive(!value);
	}
}
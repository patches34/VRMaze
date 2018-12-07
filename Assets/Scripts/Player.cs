using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float warpTime;
	Vector3 startPos;
	bool isMoving = false;

	public GridTile currentTile;

	public GameObject rayBlocker;

    [SerializeField]
    Image hudIcon;

	public void MoveTo(GridTile tile)
	{
		if(isMoving)
		{
			Debug.LogWarning("Can't do that yet");
			return;
		}

		isMoving = true;

		currentTile.Reset();
		currentTile = tile;

		StartCoroutine(WarpLerp(new Vector3(tile.transform.position.x, 0, tile.transform.position.z)));
	}

	IEnumerator WarpLerp(Vector3 pos)
	{
		float timer = 0;
		startPos = transform.position;
		pos.y = startPos.y;

		do
		{
			timer += Time.deltaTime;

			transform.position = Vector3.Lerp(startPos, pos, timer / warpTime);

			if (timer < warpTime)
				yield return null;
			else
				break;
		} while (true);

		isMoving = false;

		currentTile.FinishMoveTo();
	}

	public void Setup(Vector3 position, Quaternion lookAt)
	{
		transform.position = position;

		transform.rotation = lookAt;
	}

    public void SetPLayerColor(Color color)
    {
        hudIcon.color = color;

        hudIcon.enabled = true;
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class FloorTile : MonoBehaviour
{
	public List<GridTile> neighborTile;
	//	0 = North
	//	1 = South
	//	2 = East
	//	3 = West

    public GameObject Player;

    public float warpTime;
    bool isMoving = false;

    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void MoveTo(Vector3 pos)
    {
        if(isMoving)
        {
            Debug.LogWarning("Can't do that yet");
            return;
        }

        isMoving = true;
        StartCoroutine(WarpLerp(new Vector3(pos.x, 0, pos.z)));
    }

    IEnumerator WarpLerp(Vector3 pos)
    {
        float timer = 0;

        do
        {
            transform.position = Vector3.Lerp(Vector3.zero, -2 * pos, timer / warpTime);

            timer += Time.deltaTime;

            if (timer < warpTime)
                yield return null;
            else
                break;
        } while (true);

        transform.localPosition = Vector3.zero;
        isMoving = false;
    }
}

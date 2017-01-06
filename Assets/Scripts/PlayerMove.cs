using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
	public float warpTime;

	public Vector3 startPos, resetPos;

	public Rigidbody rBody;

	public float resetTime;

	// Use this for initialization
	void Start () {
		resetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WarpTo(Vector3 pos, bool isSafe)
	{
		startPos = transform.position;

		StartCoroutine(WarpLerp(pos, isSafe));
	}

	IEnumerator WarpLerp(Vector3 targetPos, bool isSafe)
	{
		float timer = 0;

		do
		{
			timer += Time.deltaTime;

			transform.position = Vector3.Lerp(startPos, targetPos, timer / warpTime);

			if(timer >= warpTime)
			{
				break;
			}

			yield return null;
		}while(true);

		if(!isSafe)
		{
			rBody.useGravity = true;

			StartCoroutine(ResetWait());
		}
	}

	IEnumerator ResetWait()
	{
		float timer = 0;

		do
		{
			timer += Time.deltaTime;

			if(timer >= resetTime)
				break;

			yield return null;
		}while(true);

		rBody.useGravity = false;
		rBody.velocity = Vector3.zero;
		transform.position = resetPos;
	}
}

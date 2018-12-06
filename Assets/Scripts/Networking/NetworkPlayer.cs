using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent (typeof(SpriteRenderer))]
public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField]
	Transform player;

	SpriteRenderer sprite;

    [SyncVar]
    Color playerColor;

	void Start()
	{
		if(!isLocalPlayer)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);
		}
        else
        {
            playerColor = new Color(Random.value, Random.value, Random.value);
        }

        sprite = GetComponent<SpriteRenderer>();
    }

	public override void OnStartLocalPlayer ()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");

		transform.SetParent(p.transform);

		sprite.enabled = false;

		p.GetComponent<Player>().rayBlocker.SetActive(false);

		base.OnStartLocalPlayer();
	}

	void Update()
	{
        //  Set the player's icon color
        if(sprite.color != playerColor)
        {
            sprite.color = playerColor;
        }

        //  Update the other player's icon
		if(!isLocalPlayer && player != null)
		{
            //  hide it if they are on the same tile as you
			if(transform.position == player.position)
			{
				sprite.enabled = false;
			}
            //  Show it and make it point towards you if not on your tile
			else
			{
				sprite.enabled = true;

				transform.LookAt(player, Vector3.up);
			}
		}
	}
}
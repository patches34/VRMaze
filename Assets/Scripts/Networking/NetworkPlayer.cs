using UnityEngine;
using System.Collections.Generic;
using Mirror;

[RequireComponent (typeof(SpriteRenderer))]
public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField]
	Transform player;

	SpriteRenderer sprite;

    [SyncVar]
    Color playerColor;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
	{
		if(!isLocalPlayer)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);
		}
        //  Only set the color for the local player
        else
        {
            playerColor = new Color(Random.value, Random.value, Random.value);

            transform.GetComponentInParent<Player>().SetPLayerColor(playerColor);
        }
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
﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
	public Transform player;
	public SpriteRenderer sprite;

	public float yOffset;

    [SerializeField]
    int id;

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
    }

	public override void OnStartLocalPlayer ()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");

		transform.SetParent(p.transform);

		transform.Translate(0, yOffset, 0);

		sprite.enabled = false;

		p.GetComponent<Player>().rayBlocker.SetActive(false);

		base.OnStartLocalPlayer();
	}

	void Update()
	{
        if(sprite.color != playerColor)
        {
            sprite.color = playerColor;
        }

		if(!isLocalPlayer && player != null)
		{
			if(transform.position == player.position)
			{
				sprite.enabled = false;
			}
			else
			{
				sprite.enabled = true;

				transform.LookAt(player, Vector3.up);
			}
		}
	}
}
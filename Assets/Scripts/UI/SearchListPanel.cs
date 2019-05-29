using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;

public class SearchListPanel : MonoBehaviour
{
    [SerializeField]
	GameObject roomBtnPrefab;

    [SerializeField]
    Transform gamesTransform;

	public MainMenu main;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable()
	{
		for(int i = 0; i < gamesTransform.childCount; ++i)
		{
			Destroy(gamesTransform.GetChild(i).gameObject, 0.1f);
		}
	}

	/*public void Setup(List<MatchInfoSnapshot> matches)
	{
		foreach(MatchInfoSnapshot match in matches)
		{
			GameObject newMatch = (GameObject)Instantiate(roomBtnPrefab, gamesTransform);

			roomInfo room = newMatch.GetComponent<roomInfo>();

			room.label.text = match.name;
			//room.info = match;

			newMatch.transform.localScale = Vector3.one;
			newMatch.transform.localPosition = Vector3.zero;
		}
	}

	public void JoinGame(MatchInfoSnapshot info)
	{
		main.JoinGame(info);

		gameObject.SetActive(false);
	}*/
}
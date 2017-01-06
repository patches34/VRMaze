using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class roomInfo : MonoBehaviour, IPointerClickHandler
{
	public MatchInfoSnapshot info;

	public Text label;

	public void OnPointerClick(PointerEventData data)
	{
		GetComponentInParent<SearchListPanel>().JoinGame(info);
	}
}
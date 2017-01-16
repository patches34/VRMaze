using UnityEngine;
using System.Collections;

/// <summary>
/// Bass drives the composition
/// </summary>
public class Bass : Instrument 
{

	void Awake()
	{
		Init ("Bass");

		base.Awake ();
	}

	protected override void Update()
	{
		if (bend != 0f)
		{
			GetAudioSource.pitch += bend * Time.deltaTime;

			foreach (string key in SoundManager.Instance.instruments.Keys) 
			{
				SoundManager.Instance.instruments [key].GetAudioSource.pitch = GetAudioSource.pitch;
			}
		}
	}

	public override void Play()
	{
		bend = 0f;
		GetAudioSource.pitch = 1f;
		mute = false;

		//Bass will always play, will never mute
		if (Random.Range (0, 3) == 1) 
		{
			currentTrackIndex = Random.Range (0, tracks.Count);
		}

		//Apply a pitch bend to the entire composition, cannot do to type C because it is too long
		if (Random.Range(0, (15 - SoundManager.Instance.maxIntensity)) == 1 && GetCurrentTrack.Type != MusicType.C)
		{
			StartBend ();
		}

		base.Play ();
	}
}

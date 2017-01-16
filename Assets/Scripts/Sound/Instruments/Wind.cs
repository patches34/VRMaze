using UnityEngine;
using System.Collections;

/// <summary>
/// Wind is background and basic so should be played often
/// </summary>
public class Wind : Instrument
{
	void Awake()
	{
		Init ("Wind");

		base.Awake ();
	}

	public override void Play()
	{
		bend = 0f;
		GetAudioSource.pitch = 1f;
		mute = false;
		bool flag = false;

		if (Random.Range (0, 2) == 1) 
		{
			currentTrackIndex = Random.Range (0, tracks.Count);
			RandomPan ();

			while (!flag)
			{
				//Piano should always match bass type
				if (GetCurrentTrack.Intensity > SoundManager.Instance.maxIntensity)
				{
					currentTrackIndex = Random.Range (0, tracks.Count);
				} 
				else
				{
					flag = true;
				}
			}
		} 
		else 
		{
			mute = true;
		}

		base.Play ();
	}
}

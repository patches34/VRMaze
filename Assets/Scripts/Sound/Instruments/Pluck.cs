using UnityEngine;
using System.Collections;

/// <summary>
/// Pluck should be rare
/// </summary>
public class Pluck : Instrument
{
	void Awake()
	{
		Init ("Pluck");

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
				//Account for intensity, do not need to account for type
				if (GetCurrentTrack.Intensity > SoundManager.Instance.maxIntensity)
				{
					currentTrackIndex = Random.Range (0, tracks.Count);
				} 
				else
				{
					flag = true;
				}
			}

			//Pluck should bend 50% of the time if bass is not bending
			if (SoundManager.Instance.instruments["Bass"].Bend == 0f && Random.Range(0, 5) == 1 && GetCurrentTrack.Type != MusicType.C)
			{
				StartBend ();
			}
		} 
		else 
		{
			mute = true;
		}

		base.Play ();
	}
}

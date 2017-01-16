using UnityEngine;
using System.Collections;

/// <summary>
/// Oh?
/// </summary>
public class Ohs : Instrument
{
	void Awake()
	{
		Init ("Ohs");

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
				if (SoundManager.Instance.instruments ["Bass"].GetCurrentTrack.Type != GetCurrentTrack.Type)
				{
					currentTrackIndex = Random.Range (0, tracks.Count);
				} 
				else
				{
					if (GetCurrentTrack.Intensity > SoundManager.Instance.maxIntensity)
					{
						mute = true;
					} 

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

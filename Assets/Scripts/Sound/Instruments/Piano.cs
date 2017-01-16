using UnityEngine;
using System.Collections;

/// <summary>
/// Piano the most recognizable and disruptive. So should be used sparingly 
/// </summary>
public class Piano : Instrument
{
	void Awake()
	{
		Init ("Piano");

		base.Awake ();
	}

	public override void Play()
	{
		bend = 0f;
		GetAudioSource.pitch = 1f;
		mute = false;
		bool flag = false;

		if (Random.Range (0, 6) == 1) 
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

			//Piano is already rare, so a bend is awesome
			if (SoundManager.Instance.instruments["Bass"].Bend == 0f && Random.Range(0, 12) == 1 && GetCurrentTrack.Type != MusicType.C)
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

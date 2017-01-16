using UnityEngine;
using System.Collections;

/// <summary>
/// Celesta should be played often
/// </summary>
public class Celesta : Instrument 
{
	void Awake()
	{
		Init ("Celesta");

		base.Awake ();
	}

	public override void Play()
	{
		bend = 0f;
		GetAudioSource.pitch = 1f;
		mute = false;
		bool flag = false;

		//bool flag;
		if (Random.Range (0.0f, 2.0f) > 1.3f) 
		{
			while (!flag)
			{
				//Match the bass
				if (SoundManager.Instance.instruments ["Bass"].GetCurrentTrack.Type != GetCurrentTrack.Type)
				{
					currentTrackIndex = Random.Range (0, tracks.Count);
				} 
				else
				{
					flag = true;

					//Random intensity check, cannot check for type and intensity at one time because of endless loops
					if (GetCurrentTrack.Intensity > SoundManager.Instance.maxIntensity)
					{
						mute = true;
					} 
				}
			}

			//Bend
			if (SoundManager.Instance.instruments["Bass"].Bend == 0f && Random.Range(0, (17 - SoundManager.Instance.maxIntensity)) == 1)
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

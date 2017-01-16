using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager> 
{
	#region Fields
	//Assigned
	public int maxIntensity; //1-10
	public AudioMixerGroup musicMixerGroup;

	//Constants
	public const string musicLocation = "Sound/Music/";
	public const string effectsLocation = "Sound/Effects/";
	const string musicOnString = "MusicOn";
	const string effectOnString = "EffectOn";
	const string defaultSnapshotStr = "Snapshot";
	const string muteSnapshotStr = "Mute";

	//Music
	public Dictionary<string, Instrument> instruments = new Dictionary<string, Instrument>();


	//Effects
	public AudioSource UIAudioSource;
	Dictionary<string , AudioClip> soundEffects = new Dictionary<string, AudioClip>();

	//On/off sound
	public bool isMusicOn{ get; internal set; }
	public bool isEffectOn { get; internal set; }
	#endregion

	protected SoundManager(){}
	
	void Awake () 
	{
		//Load sound effects
		Object[] effects = Resources.LoadAll(effectsLocation);
		foreach(Object obj in effects)
		{
			soundEffects.Add(obj.name, (AudioClip)obj);
		}

		//Create instruments
		instruments.Add("Bass", GetComponent<Bass>());
		instruments.Add("Piano", GetComponent<Piano>());
		instruments.Add ("Celesta", GetComponent<Celesta> ());
		instruments.Add ("Wind", GetComponent<Wind> ());
		instruments.Add ("Pluck", GetComponent<Pluck> ());
		instruments.Add ("Ohs", GetComponent<Ohs> ());
	}
	
	void Start()
	{
		SetIsMusicOn(System.Convert.ToBoolean(PlayerPrefs.GetInt(musicOnString, System.Convert.ToInt16(true))));
		SetIsEffectOn(System.Convert.ToBoolean(PlayerPrefs.GetInt(effectOnString, System.Convert.ToInt16(true))));
	}

	void Update()
	{
		//The driving track is the bass, so if that's not playing then new tracks need selected
		if (!instruments["Bass"].GetAudioSource.isPlaying)
		{
			foreach (string key in instruments.Keys) 
			{
				instruments[key].Play ();
				instruments [key].GetAudioSource.pitch = instruments ["Bass"].GetAudioSource.pitch;
			}
		}
	}

	#region Sound On/Off
	public void SetIsMusicOn(bool value)
	{
		isMusicOn = value;

		/*if(isMusicOn)
			musicMixer.FindSnapshot(defaultSnapshotStr).TransitionTo(0);
		else
			musicMixer.FindSnapshot(muteSnapshotStr).TransitionTo(0);*/

		PlayerPrefs.SetInt(musicOnString, System.Convert.ToInt16(isMusicOn));
	}

	public void SetIsEffectOn(bool value)
	{
		isEffectOn = value;

		/*if(isEffectOn)
			effectsMixer.FindSnapshot(defaultSnapshotStr).TransitionTo(0);
		else
			effectsMixer.FindSnapshot(muteSnapshotStr).TransitionTo(0);*/

		PlayerPrefs.SetInt(effectOnString, System.Convert.ToInt16(isEffectOn));
	}
	#endregion
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instrument : MonoBehaviour 
{
	#region Fields
	//Tracks
	protected List<Track> tracks = new List<Track> ();
	protected int currentTrackIndex = 0;
	protected bool mute;
	protected float bend = 0f; //-1f - 1f
	GvrAudioSource audioSource;

	//Getters
	public Track GetCurrentTrack { get { return tracks[currentTrackIndex]; } }
	public GvrAudioSource GetAudioSource { get { return audioSource; } }
	public float Bend { get { return bend; } }
	#endregion

	/// <summary>
	/// Inits instrument and loads all of its tracks
	/// </summary>
	/// <param name="name">Name.</param>
	public void Init(string name)
	{
		//load tracks
		tracks.Clear();
		Track temp;
		Object[] clips = Resources.LoadAll(SoundManager.musicLocation + name + "/");
		foreach(Object obj in clips)
		{
			temp = new Track ();
			string[] info = obj.name.Split ('_');
			temp.Init ((AudioClip)obj, (MusicType)System.Enum.Parse(typeof(MusicType), info[1], true), int.Parse(info [2]));
			tracks.Add (temp);
		}
	}

	protected void Awake()
	{
		audioSource = gameObject.AddComponent<GvrAudioSource>();
		audioSource.playOnAwake = false;
		//audioSource.audioMixerGroup = SoundManager.Instance.musicMixerGroup;
	}

	protected virtual void Update()
	{
		if (bend != 0f)
		{
			GetAudioSource.pitch += bend * Time.deltaTime;
		}
	}

	/// <summary>
	/// Provides the audio source with a random stereo pan for instruments that use it
	/// </summary>
	protected void RandomPan()
	{
		GetAudioSource.spread = Random.Range (-1.00f, 1.00f);
	}

	/// <summary>
	/// Starts a bend: a gradual change of pitch until the next measure
	/// </summary>
	protected void StartBend()
	{
		bend = Random.Range (-1.00f * (SoundManager.Instance.maxIntensity / 10.00f), (SoundManager.Instance.maxIntensity / 10.00f));
	}

	/// <summary>
	/// The play function meant to be overriden by child classes
	/// </summary>
	public virtual void Play()
	{
		Debug.Log (GetCurrentTrack.Clip);

		if (!mute)// && !GetAudioSource.isPlaying) //Might need to remove that second part
		{
			audioSource.PlayOneShot(GetCurrentTrack.Clip);
		}
	}
}

using UnityEngine;
using System.Collections;

public enum MusicType
{
	A,
	B,
	C,
	D,
	None
}

public class Track
{
	#region Fields
	//data
	private AudioClip clip;
	private int intensity;
	private MusicType type;

	//getters
	public AudioClip Clip { get { return clip; } }
	public MusicType Type { get { return type; } }
	public int Intensity { get { return intensity; } }
	#endregion

	public void Init(AudioClip clip, MusicType type, int intensity)
	{
		this.clip = clip;
		this.type = type;
		this.intensity = intensity;
	}
}

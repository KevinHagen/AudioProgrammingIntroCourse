﻿using UnityEngine;

[CreateAssetMenu(menuName = "AudioProgrammingIntro/New Audio Event", fileName = "Audio Event", order = 0)]
public class ScriptableAudioEvent : ScriptableObject
{
	[Header("Clips")]
	[Tooltip("The audio clips used for the sfx")]
	[SerializeField] private AudioClip[] _clips = default;

	[Header("Volume")]
	[Tooltip("Toggles whether the sfx is played at a fixed or randomized volume")]
	[SerializeField] 
	private bool _randomizeVolume = false;
	[Tooltip("The fixed volume at which the sfx will be played")]
	[SerializeField] 
	private float _volume = 1f;
	[Tooltip("The minimum volume at which the sfx will be played, if randomized")]
	[SerializeField] 
	private float _volumeMin = 0.2f;
	[Tooltip("The maximum volume at which the sfx will be played, if randomized")]
	[SerializeField] 
	private float _volumeMax = 1.0f;
	
	[Header("Pitch")]
	[Tooltip("Toggles whether the sfx is played at a fixed or randomized pitch")]
	[SerializeField] 
	private bool _randomizePitch = false;
	[Tooltip("The fixed pitch at which the sfx will be played")]
	[SerializeField] 
	private float _pitch = 1f;
	[Tooltip("The minimum pitch at which the sfx will be played, if randomized")]
	[SerializeField] 
	private float _pitchMin = 0.2f;
	[Tooltip("The maximum pitch at which the sfx will be played, if randomized")]
	[SerializeField] 
	private float _pitchMax = 1.0f;

	/// <summary>
	///		Plays SFX on the audio source.
	/// </summary>
	/// <param name="source">The audio source to play the event on.</param>
	public void Play(AudioSource source)
	{
		if ((_clips.Length == 0) || (source == null))
		{
			return;
		}

		source.clip = _clips[0];
		source.volume = _volume;
		source.pitch = _pitch;
		
		source.Play();
	}
}
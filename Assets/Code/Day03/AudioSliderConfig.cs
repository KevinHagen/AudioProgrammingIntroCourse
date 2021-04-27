using System;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioProgrammingIntro/New Audio Slider Config", fileName = "AudioSliderConfig", order = 0)]
public class AudioSliderConfig : ScriptableObject
{
	[SerializeField] private AudioMixerGroups _group;

	public string VolumeKey
	{
		get
		{
			switch (_group)
			{
				case AudioMixerGroups.Master:
					return AudioMixerParams.MasterVolumeKey;
				case AudioMixerGroups.Music:
					return AudioMixerParams.MusicVolumeKey;
				case AudioMixerGroups.SFX:
					return AudioMixerParams.SfxVolumeKey;
				case AudioMixerGroups.VO:
					return AudioMixerParams.VoiceOverVolumeKey;
			}
			
			throw new ArgumentOutOfRangeException();
		}
	}
}
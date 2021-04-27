using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.Day03
{
	public class SettingsPreviewAudio : MonoBehaviour
	{
		[SerializeField] private float _maxPlayTime = 2f;

		private AudioSource _source;
		private float _currentPlaytime;

		private void Awake()
		{
			_source = GetComponent<AudioSource>();
		}

		public void Play(AudioClip clip, AudioMixerGroup mixerGroup)
		{
			if (_source.isPlaying)
			{
				return;
			}

			_source.outputAudioMixerGroup = mixerGroup;
			_source.clip = clip;
			_source.Play();
			_currentPlaytime = 0f;
		}

		private void Update()
		{
			if (!_source.isPlaying)
			{
				return;
			}

			_currentPlaytime += Time.deltaTime;
			if (_currentPlaytime >= _maxPlayTime)
			{
				_source.Stop();
				_currentPlaytime = 0f;
			}
		}
	}
}
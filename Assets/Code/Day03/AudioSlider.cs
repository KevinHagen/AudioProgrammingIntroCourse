using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Code.Day03
{
	public class AudioSlider : MonoBehaviour
	{
		[Header("Slider Setup")]
		[Tooltip("The slider driving this audio slider")]
		[SerializeField]
		private Slider _slider = default;
		[SerializeField] private AudioSliderConfig _config;

		[Tooltip("Text to output the sliders current value.")]
		[SerializeField] 
		private Text _currentValueText = default;

		[SerializeField] private AudioMixerGroup _mixerGroup;
		
		[Header("Audio Preview")] [SerializeField]
		private AudioClip _previewClip;
		[SerializeField] private SettingsPreviewAudio _preview;
		[SerializeField] private float _minValue = -40f;
		private float _maxValue;
		private float _minMaxDelta;
		
		private void Awake()
		{
			_slider.onValueChanged.AddListener(OnSliderValueChanged);

			_mixerGroup.audioMixer.GetFloat(_config.VolumeKey, out _maxValue);
			_minMaxDelta = _maxValue - _minValue;

			UpdateMixerAndUI(_slider.value);
		}

		private void OnDestroy()
		{
			_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
		}

		private void OnSliderValueChanged(float value)
		{
			UpdateMixerAndUI(value);
			PlayPreviewAudio();
		}

		private void PlayPreviewAudio()
		{
			_preview.Play(_previewClip, _mixerGroup);
		}

		private void UpdateMixerAndUI(float value)
		{
			// _maxValue: +20 dB
			// _minValue: -40 dB
			// _minMaxDelta = _maxValue - _minValue = +20 - (-40) = 60 dB
			// _maxValue - ((1 - value) * _minMaxDelta) = +20 - ((1 - 0.75) * 60) = +20 - (0.25 * 60) = +20 - 15 = 5 dB
			// +20 - ((1 - 1) * 60) = +20 - (0 * 60) = +20 - 0 = 20 dB
			// +20 - ((1 - 0) * 60) = +20 - (1 * 60) = +20 - 60 = -40 dB
			float mixerVolume = _maxValue - ((1 - value) * _minMaxDelta);
			float displayValue = value * 100f;
			_currentValueText.text = displayValue.ToString("000") + "%";
			_mixerGroup.audioMixer.SetFloat(_config.VolumeKey, mixerVolume);
		}
	}
}
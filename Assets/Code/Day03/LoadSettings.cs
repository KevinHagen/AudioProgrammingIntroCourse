using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Code.Day03
{
	public class LoadSettings : MonoBehaviour
	{
		[SerializeField] private AudioSlider _masterSlider;
		[SerializeField] private AudioSlider _musicSlider;
		[SerializeField] private AudioSlider _sfxSlider;
		[SerializeField] private AudioSlider _voSlider;

		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(LoadFromSettings);
		}

		private void LoadFromSettings()
		{
			_masterSlider.Load = true;
			_musicSlider.Load = true;
			_sfxSlider.Load = true;
			_voSlider.Load = true;
			
			if (PlayerPrefs.HasKey(PlayerPrefsKeys.MasterVolumeKey))
			{
				float masterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolumeKey, 0);
				// (+20 - 0) / 60 = 20 / 60 = 1/3
				// (+20 - (-10)) / 60 = 30 / 60 = 1/2
				float sliderValue = 1 - ((_masterSlider.MaxValue - masterVolume) / _masterSlider.MinMaxDelta);
				_masterSlider.Slider.value = sliderValue;
			}
			
			if (PlayerPrefs.HasKey(PlayerPrefsKeys.MusicVolumeKey))
			{
				float musicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolumeKey, 0);
				float sliderValue = 1 - ((_musicSlider.MaxValue - musicVolume) / _musicSlider.MinMaxDelta);
				_musicSlider.Slider.value = sliderValue;
			}
			
			if (PlayerPrefs.HasKey(PlayerPrefsKeys.SfxVolumeKey))
			{
				float sfxVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SfxVolumeKey, 0);
				float sliderValue = 1 - ((_sfxSlider.MaxValue - sfxVolume) / _sfxSlider.MinMaxDelta);
				_sfxSlider.Slider.value = sliderValue;
			}
			
			if (PlayerPrefs.HasKey(PlayerPrefsKeys.VoiceOverVolumeKey))
			{
				float voiceOverVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.VoiceOverVolumeKey, 0);
				float sliderValue = 1 - ((_voSlider.MaxValue - voiceOverVolume) / _voSlider.MinMaxDelta);
				_voSlider.Slider.value = sliderValue;
			}
			
			_masterSlider.Load = false;
			_musicSlider.Load = false;
			_sfxSlider.Load = false;
			_voSlider.Load = false;
		}
	}
}
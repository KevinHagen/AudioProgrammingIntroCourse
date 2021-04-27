using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Code.Day03
{
	public class SaveSettings : MonoBehaviour
	{
		[SerializeField] private AudioMixer _settingsMixer;
		
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(SaveToSettings);
		}

		private void SaveToSettings()
		{
			_settingsMixer.GetFloat(AudioMixerParams.MasterVolumeKey, out float masterVolume);
			_settingsMixer.GetFloat(AudioMixerParams.MusicVolumeKey, out float musicVolume);
			_settingsMixer.GetFloat(AudioMixerParams.SfxVolumeKey, out float sfxVolume);
			_settingsMixer.GetFloat(AudioMixerParams.VoiceOverVolumeKey, out float voiceOverVolume);
			
			PlayerPrefs.SetFloat(PlayerPrefsKeys.MasterVolumeKey, masterVolume);
			PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolumeKey, musicVolume);
			PlayerPrefs.SetFloat(PlayerPrefsKeys.SfxVolumeKey, sfxVolume);
			PlayerPrefs.SetFloat(PlayerPrefsKeys.VoiceOverVolumeKey, voiceOverVolume);
			
			PlayerPrefs.Save();
		}
	}
}
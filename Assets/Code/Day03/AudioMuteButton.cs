using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Day03
{
	public class AudioMuteButton : MonoBehaviour
	{
		[SerializeField] private Toggle _toggle;

		private void Awake()
		{
			_toggle.onValueChanged.AddListener(OnMuteToggle);
		}

		private void OnDestroy()
		{
			_toggle.onValueChanged.RemoveListener(OnMuteToggle);
		}

		private void OnMuteToggle(bool active)
		{
			AudioListener.volume = active ? 0f : 1f;
		}
	}
}
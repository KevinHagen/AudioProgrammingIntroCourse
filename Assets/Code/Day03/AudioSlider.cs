using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Day03
{
	public class AudioSlider : MonoBehaviour
	{
		[Header("Slider Setup")]
		[Tooltip("The slider driving this audio slider")]
		[SerializeField]
		private Slider _slider = default;

		[Tooltip("Text to output the sliders current value.")]
		[SerializeField] 
		private Text _currentValueText = default;

		private void Awake()
		{
			_slider.onValueChanged.AddListener(OnSliderValueChanged);
		}

		private void OnDestroy()
		{
			_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
		}

		private void OnSliderValueChanged(float value)
		{
			float displayValue = value * 100f;
			_currentValueText.text = displayValue.ToString("000") + "%";
		}
	}
}
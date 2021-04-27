using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Code.Day03
{
	public class CutsceneController : MonoBehaviour
	{
		[SerializeField] private PlayableDirector _playableDirector;

		private bool _isPaused;
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_playableDirector.Play();
			}
			
			// if the timeline is playing: pause it - if not continue!
			if (Input.GetKeyDown(KeyCode.L))
			{
				if (_isPaused)
				{
					_playableDirector.Resume();
				}
				else
				{
					_playableDirector.Pause();
				}
				_isPaused = !_isPaused;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				_playableDirector.Play();
			}
		}
	}
}
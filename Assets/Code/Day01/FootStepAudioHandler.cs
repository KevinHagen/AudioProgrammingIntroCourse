using UnityEngine;

/// <summary>
///		Class that forward an animation event to a <see cref="ScriptableAudioEvent"/>.
/// </summary>
public class FootStepAudioHandler : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource = default;
	
	public void DoFootStepSound(AnimationEvent animationEvent)
	{
		// simple play of sfx on an audio source:
		// AudioClip clip = animationEvent.objectReferenceParameter as AudioClip;
		// _audioSource.clip = clip;
		// _audioSource.Play();
		
		// Usage of scriptable audio events:
		ScriptableAudioEvent footStepAudioEvent = animationEvent.objectReferenceParameter as ScriptableAudioEvent;
		if (footStepAudioEvent != null)
		{
			footStepAudioEvent.Play(_audioSource);
		}
	}
}
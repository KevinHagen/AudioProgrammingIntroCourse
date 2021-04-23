using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableAudioEvent))]
public class ScriptableAudioEventEditor : Editor
{
	[SerializeField] private AudioSource _previewAudioSource = null;
	
	private SerializedProperty _clipsProperty;

	private SerializedProperty _randomizeVolumeProperty;
	private SerializedProperty _volumeProperty;
	private SerializedProperty _minVolumeProperty;
	private SerializedProperty _maxVolumeProperty;
	
	private SerializedProperty _randomizePitchProperty;
	private SerializedProperty _pitchProperty;
	private SerializedProperty _minPitchProperty;
	private SerializedProperty _maxPitchProperty;

	private void OnEnable()
	{
		_previewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview Source", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
		
		_clipsProperty = serializedObject.FindProperty("_clips");

		_randomizeVolumeProperty = serializedObject.FindProperty("_randomizeVolume");
		_volumeProperty = serializedObject.FindProperty("_volume");
		_minVolumeProperty = serializedObject.FindProperty("_volumeMin");
		_maxVolumeProperty = serializedObject.FindProperty("_volumeMax");

		_randomizePitchProperty = serializedObject.FindProperty("_randomizePitch");
		_pitchProperty = serializedObject.FindProperty("_pitch");
		_minPitchProperty = serializedObject.FindProperty("_pitchMin");
		_maxPitchProperty = serializedObject.FindProperty("_pitchMax");
	}

	private void OnDisable()
	{
		DestroyImmediate(_previewAudioSource.gameObject);
	}

	public override void OnInspectorGUI()
	{
		ScriptableAudioEvent evnt = (ScriptableAudioEvent) target;
		
		serializedObject.Update();
		
		EditorGUILayout.PropertyField(_clipsProperty, true);

		EditorGUILayout.PropertyField(_randomizeVolumeProperty);
		if (evnt.RandomizeVolume)
		{
			EditorGUILayout.PropertyField(_minVolumeProperty);
			EditorGUILayout.PropertyField(_maxVolumeProperty);
		}
		else
		{
			EditorGUILayout.PropertyField(_volumeProperty);
		}

		EditorGUILayout.PropertyField(_randomizePitchProperty);
		if (evnt.RandomizePitch)
		{
			EditorGUILayout.PropertyField(_minPitchProperty);
			EditorGUILayout.PropertyField(_maxPitchProperty);
		}
		else
		{
			EditorGUILayout.PropertyField(_pitchProperty);
		}
		
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Preview Sound"))
		{
			evnt.Play(_previewAudioSource);
		}
	}
}
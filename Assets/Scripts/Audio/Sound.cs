using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f,1f)] public float volume;
	[Range(0.1f, 3f)] public float pitch;
	[Range(0f, 1f)] public float blend;
	[Range(0f, 15f)] public float maxDistance;
	[Range(0f, 15f)] public float minDistance;
	public AudioRolloffMode rolloffMode;

	[HideInInspector] public AudioSource source;
}
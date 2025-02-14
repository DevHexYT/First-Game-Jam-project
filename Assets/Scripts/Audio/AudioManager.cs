using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	void Awake() {
		foreach(Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.spatialBlend = s.blend;
			s.source.maxDistance = s.maxDistance;
			s.source.minDistance = s.minDistance;
			s.source.rolloffMode = s.rolloffMode;
		}
	}

	public void Play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) return;
		s.source.Play();
	}
	public void Play(string name, float pitch) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.pitch = pitch;
		if (s == null) return;
		s.source.Play();
	}
}

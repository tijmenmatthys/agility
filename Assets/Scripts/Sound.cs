﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 1f;
	[Range(0f, 1f)]
	public float volumeVariance = 0f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = 0f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;
}

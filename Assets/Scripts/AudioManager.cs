using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[SerializeField] private Sound[] _sounds;

    private Dictionary<string, Sound> _soundDict = new Dictionary<string, Sound>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitSounds();
        InitMusic();
        PlayMenu();
    }

    private void InitSounds()
    {
        foreach (Sound s in _sounds)
        {
            _soundDict[s.name] = s;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    public void PlaySound(string sound)
	{
		Sound s = _soundDict[sound];
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

    // everything below is specific to this game

    private void InitMusic()
    {
        PlaySound("Menu");
        PlaySound("Layer1");
        PlaySound("Layer2");
        PlaySound("Layer3");
    }

    public void PlayMenu()
    {
        MuteSound("Menu", false);
        MuteSound("Layer1", true);
        MuteSound("Layer2", true);
        MuteSound("Layer3", true);
    }

    public void PlayEasy()
    {
        MuteSound("Menu", true);
        MuteSound("Layer1", false);
        MuteSound("Layer2", true);
        MuteSound("Layer3", true);
    }

    public void PlayMedium()
    {
        MuteSound("Menu", true);
        MuteSound("Layer1", false);
        MuteSound("Layer2", false);
        MuteSound("Layer3", true);
    }

    public void PlayHard()
    {
        MuteSound("Menu", true);
        MuteSound("Layer1", false);
        MuteSound("Layer2", false);
        MuteSound("Layer3", false);
    }

    private void MuteSound(string sound, bool mute)
    {
        Sound s = _soundDict[sound];
        s.source.mute = mute;
    }

}

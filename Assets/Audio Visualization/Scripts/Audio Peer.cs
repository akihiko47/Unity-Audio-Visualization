using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {

    public static float[] _spectrum = new float[256];

    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        GetSpectrumAudioSource();
    }

    private void GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.Blackman);
    }

}

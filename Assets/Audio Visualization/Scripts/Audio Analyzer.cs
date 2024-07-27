using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour {

    [SerializeField, Range(0f, 1f)]
    private float _smoothTime = 0.1f;

    public enum Bands {
        SubBass,     // 20   - 50     hertz
        Bass,        // 60   - 250    hertz  
        LowMid,      // 250  - 500    hertz
        Mid,         // 500  - 2000   hertz
        UpperMid,    // 2000 - 4000   hertz
        Presence,    // 4000 - 6000   hertz
        Brilliance,  // 6000 - 12000  hertz
    }

    // Ranges in samples space (0 - 512)
    private Dictionary<Bands, int[]> _bandsRanges = new Dictionary<Bands, int[]>() {
        {Bands.SubBass,    new int[2] {0,     1} },
        {Bands.Bass,       new int[2] {1,     6} },
        {Bands.LowMid,     new int[2] {6,    11} },
        {Bands.Mid,        new int[2] {11,   43} },
        {Bands.UpperMid,   new int[2] {43,   86} },
        {Bands.Presence,   new int[2] {86,  128} },
        {Bands.Brilliance, new int[2] {128, 512} },
    };

    private float[] _spectrum      = new float[512];
    private float[] _bands         = new float[7];
    private float[] _bandsSmoothed = new float[7];

    // Value for SmoothDamp
    private float[] _bandsVelocity = new float[7];

    private AudioSource _audioSource;

    private void Start() {
        CheckPrefs();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        GetSpectrumAudioSource();
        CreateBands();
        SmoothBands();
    }

    private void GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.Blackman);
    }

    private void CreateBands() {
        for (int i = 0; i < 7; i++) {
            _bands[i] = SampleRange(
                _bandsRanges[(Bands)i][0],
                _bandsRanges[(Bands)i][1]
            );
        }
    }

    private void SmoothBands() {
        for (int i = 0; i < 7; i++) {
            if (_bands[i] > _bandsSmoothed[i]) {
                _bandsSmoothed[i] = _bands[i];
            } else {
                _bandsSmoothed[i] = Mathf.SmoothDamp(_bandsSmoothed[i], _bands[i], ref _bandsVelocity[i], _smoothTime);
            }
        }
    }

    private void CheckPrefs() {
        if (AudioSettings.outputSampleRate != 48000) {
            Debug.LogWarning("Your audio sample sate != 48000. It can cause incorrect operation of AudioAnalyzer. Please check your Audio settings.");
        }
    }

    private float SampleRange(int a, int b) {
        if (a > b) {
            Debug.LogError("Range should ascend from a to b!");
            return 0;
        }

        float avrg = 0f;
        for (int i = a; i < b; i++) {
            avrg += _spectrum[i];
        }
        avrg = avrg / (b - a);
        return avrg;
    }

    public float GetBand(AudioAnalyzer.Bands band) {
        return _bands[(int)band];
    }

    public float GetBandSmoothed(AudioAnalyzer.Bands band) {
        return _bandsSmoothed[(int)band];
    }

}

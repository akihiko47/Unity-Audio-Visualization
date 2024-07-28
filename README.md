# 🎶 Unity Audio Visualization 🎶

![banner](https://github.com/akihiko47/Unity-Audio-Visualization/blob/main/Images/example.gif)

This repository contains a script that allows you to get frequency data from a sound source. The script allows you to get normalized, smoothed data for further usage in visualization. 
On the gif above you can see an example of its usage.

## Installation 🔧
There are 2 ways:
- import `AudioAnalyzer.unitypackage` via *Assets-Import* Package
- clone/download this repository and move the `Assets/Audio Analyzer` folders to your Unity project's Assets folder

## Frequency Bands 🎛️
You can get information about the desired frequencies using `GetBand(AudioAnalyzer.Bands band)` method. This method will return float in range 0 to 1. Method accepts the following bands:
1) **SubBass**      20   - 50     hertz
2) **Bass**         60   - 250    hertz  
3) **LowMid**       250  - 500    hertz
4) **Mid**          500  - 2000   hertz
5) **UpperMid**     2000 - 4000   hertz
6) **Presence**     4000 - 6000   hertz
7) **Brilliance**   6000 - 24000  hertz

## Amplitude 〰️
You can get amplitude (average of all bands) using `GetAmplitude()` method.

## Usage 🎮
1) Attach `AudioSource` to your game object.
2) Attach `AudioAnalyzer` to your game object.
3) Reference `AudioAnalyzer` component from other scripts and get frequency data using method `GetBand()`.

You can see an example of usage in this project.

Thank you for reading this 😊!
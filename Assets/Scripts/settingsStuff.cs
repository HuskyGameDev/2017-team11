using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsStuff : MonoBehaviour {

    public AudioMixer audioMixer;

	public void  setVolume( float vol )
    {
        audioMixer.SetFloat("volume", vol);
    }

    public void setFullscreen( bool isFull)
    {
        Screen.fullScreen = isFull;
    }
}

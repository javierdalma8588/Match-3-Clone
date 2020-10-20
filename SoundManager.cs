 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;

    public void PlayRandomDestroyNoise()
    {
        //chose Random Number
        int clipToPLay = Random.Range(0, destroyNoise.Length);
        //play that clip
        destroyNoise[clipToPLay].Play();
    }
}

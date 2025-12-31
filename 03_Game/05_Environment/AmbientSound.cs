using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private BgmName _ambientSoundName;
    
    [SerializeField] private int _clipIndex = 0;
    [SerializeField] private float _customVolume = 1f;
    
    void Start()
    {
        SoundManager.Instance?.PlayBgmAtAudioSource(_audioSource,_ambientSoundName,_clipIndex, _customVolume);
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    
    private AudioSource _audio = null;

    //오타 입력 방지를 위해 const로 지정함
    public const string TITLE = "TITLE";
    public const string PLAYER_SELECT = "PLAYER_SELECT";
    public const string TUTORIAL = "TUTORIAL";
    public const string WINTER_FELL = "WINTER_FELL";
    public const string DUNGEON_1 = "DUNGEON_1";
    public const string DUNGEON_2 = "DUNGEON_2";

    public const float BGM_VOLUME = 0.5f;


    public void Create()
    {
        _audio = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _audio.playOnAwake = false;
    }

    public void Delete()
    {
        StopAudioClip();
    }

    public void PlayAudioClip(AudioClip clip, float speed)
    {
        _audio.clip = clip;
        _audio.pitch = speed;
        _audio.Play();
    }

    public void StopAudioClip()
    {
        _audio.Stop();
        _audio.clip = null;
    }

    public bool IsPlaying()
    {
        return _audio.isPlaying;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneBase : MonoBehaviour
{
    public enum Status
    {
        RUN,
        FINISH_GOTO_TITLE,
        FINISH_GOTO_SELECT,

        FINISH_GOTO_TUTORIAL,       
        FINISH_GOTO_WINTERFELL,
        FINISH_GOTO_DUNGEON_1,
        FINISH_GOTO_DUNGEON_2,

        FINISH_GOTO_NEXT,
        FINISH_GOTO_BACK,
        FINISH_GOTO_RETRY
    }
    public Status CurStatus { get; protected set; } = Status.RUN;

    public const string SCENE_TITLE = "Title";
    public const string SCENE_PLAYER_SELECT = "PlayerSelect";
    public const string SCENE_TUTORIAL = "Tutorial";
    public const string SCENE_WINTERFELL = "Winterfell";
    public const string SCENE_DUNGEON_1 = "Dungeon_1";
    public const string SCENE_DUNGEON_2 = "Dungeon_2";

    public object CurStatusParameter { get; protected set; } = null;
    public virtual void Load(Action onFinished)
    {
       
        CurStatusParameter = null;
        Debug.Log("SceneBase.Load.0");

        CanvasScaler[] css = GetComponentsInChildren<CanvasScaler>();
        float ref_rate = css[0].referenceResolution.x / css[0].referenceResolution.y;
        float dei_rate = (float)Display.main.systemWidth / Display.main.systemHeight;
        int match = ref_rate >= dei_rate ? 0 : 1;

        foreach (CanvasScaler cs in css)
        {
            cs.matchWidthOrHeight = match;
        }

        onFinished?.Invoke();
    }

    public virtual void Unload(Action onFinished)
    {
        onFinished?.Invoke();
    }

    public virtual void Play(Action parameter)
    {
    }

    public virtual void Stop()
    {

    }







}

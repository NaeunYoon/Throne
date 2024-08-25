using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAct_PlayerSelect : SceneBase
{
    public Camera cam = null;
    public UIAct_PlayerSelect Ui_Mgr = null;
    public SceneActivity_PlayerSelect Activity_Mgr = null;
    private Coroutine _corPlaySequence = null;

    public override void Load(Action onFinished)
    {
        base.Load(onFinished);
        Debug.Log("SceneAct_PlayerSelect.Load.0");

        StartCoroutine(LoadSequence(onFinished));
    }
    private IEnumerator LoadSequence(Action onFinished)
    {
        Debug.Log("SceneAct_PlayerSelect.LoadSequence.1");
        Ui_Mgr.Create();
        yield return null;

        onFinished.Invoke();
    }

    public override void Play(Action parameter)
    {
        Debug.Log("SceneAct_PlayerSelect.Play.1");
        base.Play(parameter);

        _corPlaySequence = StartCoroutine(PlaySequence(parameter));
    }

    private IEnumerator PlaySequence(Action onFinished)
    {
        Debug.Log("SceneAct_PlayerSelect.PlaySequence.1");
        CurStatus = Status.RUN;

        var dirCnt = 0;
        ++dirCnt; App.inst.uiMgr.fader.Hide(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

 
        Ui_Mgr.MENU.onClick = (_click) =>
        {
            Debug.Log("SceneAct_PlayerSelect.PlaySequence.2");

            switch (_click)
            {
                case Scene_Menu_PlayerSelect.Click.Start:
                    {
                        CurStatus = Status.FINISH_GOTO_TUTORIAL;
                    }
                    break;

                case Scene_Menu_PlayerSelect.Click.Option:
                    {



                    }
                    break;
            }
        };

        while (CurStatus == Status.RUN)
        {
            yield return null;
        }

        Ui_Mgr.MENU.onClick = null;
        Debug.Log("SceneAct_PlayerSelect.PlaySequence.3");

        ++dirCnt; App.inst.uiMgr.fader.Show(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        _corPlaySequence = null;
        CurStatusParameter = null;
        Debug.Log("SceneAct_PlayerSelect.PlaySequence.4 END");

    }
    public override void Unload(Action onFinished)
    {
        StartCoroutine(UnloadSequence(onFinished));
    }
    private IEnumerator UnloadSequence(Action onFinished)
    {
        yield return null;

        Ui_Mgr.Delete();

        base.Unload(null);

        onFinished?.Invoke();
    }

    public override void Stop()
    {
        if (_corPlaySequence != null)
        {
            StopCoroutine(_corPlaySequence);
            _corPlaySequence = null;
        }

        base.Stop();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAct_Title : SceneBase
{
    public Camera cam = null;
    public UIAct_Title ui_Title = null;

    private Coroutine _corPlaySequence = null;

    public override void Load(Action onFinished)
    {
        base.Load(onFinished);
        Debug.Log("SceneAct_Title.Load.0");

        StartCoroutine(LoadSequence(onFinished));
    }
    private IEnumerator LoadSequence(Action onFinished)
    {
        Debug.Log("SceneAct_Title.LoadSequence.1");
        ui_Title.Create();
        yield return null;

        onFinished.Invoke();
    }

    public override void Play(Action parameter)
    {
        Debug.Log("SceneAct_Title.Play.1");
        base.Play(parameter);

        _corPlaySequence = StartCoroutine(PlaySequence(parameter));
    }

    private IEnumerator PlaySequence(Action onFinished)
    {
        Debug.Log("SceneAct_Title.PlaySequence.1");
        CurStatus = Status.RUN;

        var dirCnt = 0;
        ++dirCnt; App.inst.uiMgr.fader.Hide(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        ui_Title.MENU.onClick = (_click) =>
        {
            Debug.Log("SceneAct_Title.PlaySequence.2");

            switch (_click)
            {
                case Scene_Menu_Title.Click.Start:
                    {
                        App.inst.uiMgr.fader.OnOneShotFader(() =>
                        {
                            Debug.Log("GameStart");
                            CurStatus = Status.FINISH_GOTO_SELECT;
                        },null);
                    }
                 break;

                case Scene_Menu_Title.Click.Option:
                    {
                        Debug.Log("OptionClick");
                        if(ui_Title.MENU.OptionPanel.gameObject.activeSelf == true)
                        {
                            ui_Title.MENU.OptionPanel.gameObject.SetActive(false);
                        }
                        else
                        {
                            ui_Title.MENU.OptionPanel.gameObject.SetActive(true);
                        }


                    }
                    break;

                case Scene_Menu_Title.Click.Exit:
                    {
                        Debug.Log("OptionClick");
                    }
                    break;
            }
        };

        while (CurStatus == Status.RUN)
        {
            yield return null;
        }

        ui_Title.MENU.onClick = null;
        Debug.Log("SceneAct_Title.PlaySequence.3");

        ++dirCnt; App.inst.uiMgr.fader.Show(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        _corPlaySequence = null;
        CurStatusParameter = null;
        Debug.Log("SceneAct_Title.PlaySequence.4 END");

    }
    public override void Unload(Action onFinished)
    {
        StartCoroutine(UnloadSequence(onFinished));
    }
    private IEnumerator UnloadSequence(Action onFinished)
    {
        //ui 안에 있는 애들 hide 시켜줌

        yield return null;

        ui_Title.Delete();

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAct_Tutorial : SceneBase
{
    public Camera cam = null;
    public UIAct_Tutorial Ui_Mgr = null;
    public SceneActivity_Tutorial Activity_Mgr = null;

    private Coroutine _corPlaySequence = null;
    private PlayerManager _playerManager = null;
    public override void Load(Action onFinished)
    {
        base.Load(onFinished);
        Debug.Log("SceneAct_Tutorial.Load.0");

        StartCoroutine(LoadSequence(onFinished));
    }
    private IEnumerator LoadSequence(Action onFinished)
    {
        Debug.Log("SceneAct_Tutorial.LoadSequence.1");
        Ui_Mgr.Create();
        yield return null;

        onFinished.Invoke();
    }

    public override void Play(Action parameter)
    {
        Debug.Log("SceneAct_Tutorial.Play.1");
        base.Play(parameter);

        _corPlaySequence = StartCoroutine(PlaySequence(parameter));
    }

    private IEnumerator PlaySequence(Action onFinished)
    {
        Debug.Log("SceneAct_Tutorial.PlaySequence.1");
        CurStatus = Status.RUN;
        
        var dirCnt = 0;
        ++dirCnt; App.inst.uiMgr.fader.Hide(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;
        
        
        
        Ui_Mgr.MENU.onClick = (_click) =>
        {
            Debug.Log("SceneAct_Tutorial.PlaySequence.2");
            switch (_click)
            {
                case Scene_Menu_PlayerSelect.Click.Start:
                {
                    Ui_Mgr.MENU.Hide();
                    var go = Instantiate<PlayerManager>(App.inst.characterMgr.chracters[0],Activity_Mgr.transform.GetChild(3).transform);
                    App.inst.uiMgr.joystick.Show();
                    App.inst.uiMgr.joystick.GetPlayer(go);
                    App.inst.uiMgr.userInfo.Show();
                    App.inst.uiMgr.userInfo.InitUserInfo
                    (App.inst.uiMgr.monologue.portraitArr[0], 
                        App.inst.dataMgr.playerName, 
                        App.inst.dataMgr.playerLevel, 
                        App.inst.dataMgr.playerJob);
                    
                }
                    break;
            case Scene_Menu_PlayerSelect.Click.Next:
                {
                    Ui_Mgr.MENU.Hide();
                    CurStatus = Status.FINISH_GOTO_WINTERFELL;
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
        Debug.Log("SceneAct_Tutorial.PlaySequence.3");

        ++dirCnt; App.inst.uiMgr.fader.Show(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        _corPlaySequence = null;
        CurStatusParameter = null;
        Debug.Log("SceneAct_Tutorial.PlaySequence.4 END");

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

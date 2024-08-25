using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SceneAct_Winterfell : SceneBase
{
    public Camera cam = null;
    public UIAct_Winterfell Ui_Mgr = null;
    public SceneActivity_Winterfell Activity_Mgr = null;
    public VirtualJoystick joystick = null;
    private Coroutine _corPlaySequence = null;
    private PlayerManager _playerManager = null;

    public NPCController _npc = null;
    public override void Load(Action onFinished)
    {
        base.Load(onFinished);
        Debug.Log("SceneAct_Winterfell.Load.0");

        StartCoroutine(LoadSequence(onFinished));
    }
    private IEnumerator LoadSequence(Action onFinished)
    {
        Debug.Log("SceneAct_Winterfell.LoadSequence.1");
        //App.inst.uiMgr.inventory.Show();
        
        Ui_Mgr.Create();
        _playerManager = App.inst.characterMgr.chracters[0].GetComponent<PlayerManager>();
        _playerManager.Create();
        yield return null;

        onFinished.Invoke();
    }

    public override void Play(Action parameter)
    {
        Debug.Log("SceneAct_Winterfell.Play.1");
        base.Play(parameter);

        _corPlaySequence = StartCoroutine(PlaySequence(parameter));
    }

    private IEnumerator PlaySequence(Action onFinished)
    {
        Debug.Log("SceneAct_Winterfell.PlaySequence.1");
        CurStatus = Status.RUN;

        var dirCnt = 0;
        ++dirCnt; App.inst.uiMgr.fader.Hide(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        var go = Instantiate<PlayerManager>(App.inst.characterMgr.chracters[0],Activity_Mgr.transform.GetChild(0).transform);
        Debug.Log(go);
        go.transform.localPosition = new Vector3(-0.42f, 0.069f, 1.89f);
        App.inst.uiMgr.joystick.Show();
        App.inst.uiMgr.joystick.GetPlayer(go);
        App.inst.uiMgr.userInfo.Show();
        App.inst.uiMgr.userInfo.InitUserInfo
            (App.inst.uiMgr.monologue.portraitArr[0], 
                App.inst.dataMgr.playerName, 
                App.inst.dataMgr.playerLevel, 
                App.inst.dataMgr.playerJob);
        go._playercam = Activity_Mgr.playerCam;
        go._playercam.Follow = go.camPos;
        go._playercam.LookAt = go.camPos;
        _npc.player = go;
        _npc.isFollowing = true;
        Ui_Mgr.MENU.onClick = (_click) =>
        {
            Debug.Log("SceneAct_Winterfell.PlaySequence.2");
            switch (_click)
            {
                case Scene_Menu_Winterfell.Click.Start:
                {
                    Ui_Mgr.MENU.Hide();
                }
                    break;
            case Scene_Menu_Winterfell.Click.Next:
                {
                    Ui_Mgr.MENU.Hide();
                    CurStatus = Status.FINISH_GOTO_WINTERFELL;
                }
                break;

            case Scene_Menu_Winterfell.Click.Option:
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
        Debug.Log("SceneAct_Winterfell.PlaySequence.3");

        ++dirCnt; App.inst.uiMgr.fader.Show(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        _corPlaySequence = null;
        CurStatusParameter = null;
        Debug.Log("SceneAct_Winterfell.PlaySequence.4 END");

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

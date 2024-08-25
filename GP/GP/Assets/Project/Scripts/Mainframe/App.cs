using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    //app 을 싱글톤으로 만듬
    public static App inst { get; private set; } = null;
    [Header("Managers")][Space(5f)]
    public UICommonMgr uiMgr = null;
    public AudioMgr audioMgr = null;
    public SceneStateMgr sceneStateMgr = null;
    public DataMgr dataMgr = null;
    public CharacterMgr characterMgr = null;
    [Header("Etc..")][Space(5f)]
    private SceneBase _curScene = null;
    
    public void Start()
    {
        inst = this;
        inst.Create();
    }

    public void Create()
    {
        Debug.Log("App.Create.0");
        Application.targetFrameRate = 60;

        OnCreste();
    }

    public void OnCreste()
    {
        uiMgr.Create();
        audioMgr.Create();
        sceneStateMgr.Create();

        //시작하자마자 타이틀로 이동
        App.inst.uiMgr.fader.OnOneShotFader(() =>
        {
            sceneStateMgr.InitTitleScene();
        },null);

        Input.multiTouchEnabled = false;
    }

    public void SetActivatedSceneMgr(SceneBase scene)
    {
        Debug.Log("App.SetActivatedSceneMgr.0  scene: " + scene);
        _curScene = scene;
    }

    /// <summary>
    /// app에 저장되어있는 씬을 가져옴
    /// </summary>
    /// <returns></returns>
    public SceneBase GetActivatedScene()
    {
        return _curScene;
    }



    public void OnApplicationQuit()
    {
        inst.Delete();
        inst = null;
    }
    public void Delete()
    {
        audioMgr.Delete();
        sceneStateMgr.Delete();
        uiMgr.Delete();
    }

}

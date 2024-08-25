using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState_Act_Title : SceneStateBase
{
    public static Hashtable MakeParam()
    {
        return null;
    }

    public override void Enter()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        Debug.Log("SceneState_Act_Title.Sequence.0");
        var dirCnt = 0;
        var sceneName = SceneBase.SCENE_TITLE;
        SceneBase.Status sceneStatus = SceneBase.Status.RUN;

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        App.inst.SetActivatedSceneMgr(FindObjectOfType<SceneBase>(true));

        ++dirCnt;
        App.inst.GetActivatedScene().Load(() => { --dirCnt; });
        while (dirCnt != 0)
        yield return null;

        App.inst.GetActivatedScene().Play(null);

        while (true)
        {
            sceneStatus = App.inst.GetActivatedScene().CurStatus;

            if(sceneStatus != SceneBase.Status.RUN)
            {
                break;
            }
            yield return null;
        }
        App.inst.GetActivatedScene().Stop();


        ++dirCnt; App.inst.GetActivatedScene().Unload(() => { --dirCnt; });
        while (dirCnt != 0) yield return null;

        App.inst.SetActivatedSceneMgr(null);

        yield return SceneManager.UnloadSceneAsync(sceneName);

        if (GotoDirectNextState(sceneStatus) == false)
        {
            switch (sceneStatus)
            {
                case SceneBase.Status.FINISH_GOTO_TITLE:
                    {
                        App.inst.sceneStateMgr.ChangeState<SceneState_Act_Selection>(SceneState_Act_Selection.MakeParam(GetType().Name, string.Empty));
                    }
                    break;

                default:
                    {
                        App.inst.sceneStateMgr.ChangeState<SceneState_Act_Title>(SceneState_Act_Title.MakeParam());
                    }
                    break;
            }
        }


    }

}

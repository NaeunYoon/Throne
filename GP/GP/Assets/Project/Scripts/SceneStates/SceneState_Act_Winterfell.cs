using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState_Act_Winterfell : SceneStateBase
{
    public static Hashtable MakeParam(string fromSceneStateName, string scoreKey)
    {
        var hashTable = new Hashtable();
        hashTable.Add("from_scene_state_name", fromSceneStateName);
        hashTable.Add("score_key", scoreKey);
        return hashTable;
    }

    public override void Enter()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        Debug.Log("SceneState_Act_Selection.Sequence.0");
        var dirCnt = 0;
        var sceneName = SceneBase.SCENE_WINTERFELL;
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

            if (sceneStatus != SceneBase.Status.RUN)
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
                case SceneBase.Status.FINISH_GOTO_DUNGEON_1:
                    {
                        //App.Inst.sceneStateMgr.ChangeState<SceneState_StudyChoice>(SceneState_StudyChoice.MakeParameter(GetType().Name, string.Empty));
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

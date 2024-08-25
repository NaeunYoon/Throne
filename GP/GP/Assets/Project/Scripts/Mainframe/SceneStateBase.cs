using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateBase : MonoBehaviour
{
    public Hashtable parameter = null;
    public virtual void Enter()
    {

    }
    public virtual void Leave()
    {

    }

    protected bool GotoDirectNextState(SceneBase.Status s_stt)
    {
        bool t_changed = false;

        switch (s_stt)
        {
            case SceneBase.Status.FINISH_GOTO_SELECT:
                App.inst.sceneStateMgr.ChangeState<SceneState_Act_Selection>(SceneState_Act_Selection.MakeParam(GetType().Name, string.Empty));
                t_changed = true;
                break;

            //case SceneBase.Status.FINISH_WINTERFELL:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_8_1_Balloon>(SceneState_Act_8_1_Balloon.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            //case SceneBase.Status.FINISH_DUNGEON_1:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_9_1_HandWriting>(SceneState_Act_9_1_HandWriting.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            //case SceneBase.Status.FINISH_DUNGEON_2:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_10_1_CardBalloon>(SceneState_Act_10_1_CardBalloon.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            //case SceneBase.Status.FINISH_GOTO_BACK:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_11_1_LineMatch>(SceneState_Act_11_1_LineMatch.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            //case SceneBase.Status.FINISH_GOTO_NEXT:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_12_1_WordMatch>(SceneState_Act_12_1_WordMatch.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            //case SceneBase.Status.FINISH_GOTO_RETRY:
            //    App.Inst.sceneStateMgr.ChangeState<SceneState_Act_13_1_Speak>(SceneState_Act_13_1_Speak.MakeParameter(GetType().Name, string.Empty));
            //    t_changed = true;
            //    break;

            case SceneBase.Status.FINISH_GOTO_TITLE:
                App.inst.sceneStateMgr.ChangeState<SceneState_Act_Title>(SceneState_Act_Title.MakeParam());
                t_changed = true;
                break;

        }

        return t_changed;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateMgr : MonoBehaviour
{
    private List<SceneStateBase> _state = new List<SceneStateBase>();

    private SceneStateBase _activeState = null;
    public void Create()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var state = transform.GetChild(i).GetComponent<SceneStateBase>();

            if(state == null)
                continue;

            state.gameObject.SetActive(false);
            _state.Add(state);
        }
    }

    public void InitTitleScene()
    {
        ChangeState<SceneState_Act_Title>(SceneState_Act_Title.MakeParam());
    }
    public void Delete()
    {
        if(_activeState != null)
        {
            _activeState.Leave();
            _activeState.gameObject.SetActive(false);
            _activeState = null;
        }
        _state.Clear();
    }

    /// <summary>
    /// 현재 씬을 해제하고 새로운 씬을 찾아 할당해준다
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public void ChangeState<T>(Hashtable parameter) where T : SceneStateBase
    {
        if (_activeState != null)
        {
            var activestate = _activeState;
            _activeState = null;

            activestate.Leave();
            activestate.parameter = null;
            activestate.gameObject.SetActive(false);
        }

        var state = _state.Find((x) => x.GetType().Equals(typeof(T)));
        Debug.Log("SceneStateMgr.ChangeState.1  state: " + state + "  typeof(T): " + typeof(T));

        if (state != null)
        {
            _activeState = state;

            _activeState.gameObject.SetActive(true);
            _activeState.parameter= parameter;
            _activeState.Enter();
        }
    }





}

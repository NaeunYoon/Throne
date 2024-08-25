using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : UIBase
{
     public void OnOneShotFader(Action onStartAction, Action onFinishAction)
    {
        StartCoroutine(ShotFader(onStartAction, onFinishAction));
    }

    private IEnumerator ShotFader(Action onStartAction ,Action onFinishAction)
    {
        var dirCount = 0;
        ++dirCount; Show(()=> --dirCount);
        while (dirCount != 0) yield return null;
        onStartAction?.Invoke();
        ++dirCount; Hide(()=> --dirCount);
        while (dirCount != 0) yield return null;
        onFinishAction?.Invoke();
    }
}

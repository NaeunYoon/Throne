using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologueMgr : UIBase
{
    public Image portrait = null;
    public TextMeshProUGUI text = null;
    private Tweener sequence = null;
    public Sprite[] portraitArr = new Sprite[3];
    
    public void TextEffect(Action onFinished,string str,Sprite img, float duration)
    {
        text.text = str;
        text.maxVisibleCharacters = 0;
        portrait.sprite = img;
        sequence = DOTween.To(x => text.maxVisibleCharacters = (int)x,
                0f,
                text.text.Length, duration)
            .SetEase(Ease.Linear)
            .SetDelay(1f)
            .OnComplete(() =>
            {
                text.text = string.Empty;
                if (onFinished != null)
                    onFinished?.Invoke();
            });;
    }

    public void ClearText()
    {
        portrait.sprite = null;
        text.text = null;
    }
    
}


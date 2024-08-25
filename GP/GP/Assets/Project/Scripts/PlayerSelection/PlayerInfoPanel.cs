using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : UIBase
{
    public TextMeshProUGUI jobText = null;
    public Button selectBtn = null;
    public Button startBtn = null;
    public TextMeshProUGUI input = null;

    public void OnConfirmBtnClick()
    {
        //TODO : 데이터매니저 만들어서 관리해야함 일단은 플레이어 프리팹에 저장함
        PlayerPrefs.SetString("playerName", input.text);
        App.inst.dataMgr.playerName = input.text;
        selectBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        
    }

    public void OnCloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }

}

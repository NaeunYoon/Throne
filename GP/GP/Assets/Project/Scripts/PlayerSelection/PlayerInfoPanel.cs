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
        //TODO : �����͸Ŵ��� ���� �����ؾ��� �ϴ��� �÷��̾� �����տ� ������
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Menu_PlayerSelect : UICommonMenu
{
    public CharacterSelection character = null;
    public Button prev_btn = null;
    public Button next_btn = null;
    public Button selected_btn = null;
    public Button start_btn = null;
    public PlayerInfoPanel playerInfo = null;

    public Animator anim = null;

    public Action AnimChanger = null;
    public Coroutine animCo = null;

    public Material skyboxMat = null;

    public override void Create()
    {
        base.Create();
        AnimChanger = Anim;

        RenderSettings.skybox = skyboxMat;
        prev_btn.onClick.AddListener(() => { PrevBtn(AnimChanger); });
        next_btn.onClick.AddListener(() => { NextBtn(AnimChanger); });
        selected_btn.onClick.AddListener(() => { SelectBtn(); });
        start_btn.onClick.AddListener(() => { StartBtn(); });
        
        AnimChanger.Invoke();
    }

    public void NextBtn(Action onFinished)
    {
        character.Characters[character.selectedCharacter].gameObject.SetActive(false);
        character.selectedCharacter = (character.selectedCharacter + 1) % character.Characters.Length;
        
        character.Characters[character.selectedCharacter].gameObject.SetActive(true);
        onFinished.Invoke();
    }

    public void PrevBtn(Action onFinished)
    {
        character.Characters[character.selectedCharacter].gameObject.SetActive(false);
        character.selectedCharacter--;
        if(character.selectedCharacter < 0)
        {
            character.selectedCharacter += character.Characters.Length;
        }
        character.Characters[character.selectedCharacter].gameObject.SetActive(true);
        onFinished.Invoke();
    }

    public void Anim()
    {
        character.Characters[character.selectedCharacter].transform.position = new Vector3(0,-1.5f,3);
        anim = character.Characters[character.selectedCharacter].GetComponent<Animator>();
        anim.SetInteger("anim", 1);
        
        animCo = StartCoroutine(ResetAnim());
    }

    IEnumerator ResetAnim() 
    { 
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
        anim.SetInteger("anim", 0);
    }

    public void SelectBtn()
    {
        if(playerInfo.gameObject.activeSelf == true)
        playerInfo.gameObject.SetActive(false);
        else
        {
            playerInfo.gameObject.SetActive(true);
        }
        playerInfo.jobText.text = "Avility : "+character.jobName[character.selectedCharacter];
        App.inst.dataMgr.playerJob = character.jobName[character.selectedCharacter];
    }
    public void StartBtn()
    {
        PlayerPrefs.SetInt("SelectedCharacter", character.selectedCharacter);

        //App.inst.dataMgr.playerJob = playerInfo.jobText.text;
        //App.inst.dataMgr.playerName = playerInfo.input.text;
        App.inst.dataMgr.playerLevel = 1;
        App.inst.dataMgr.playerHP = 100;
        App.inst.dataMgr.playerMP = 100;
        App.inst.dataMgr.playerGold = 0;
        
        if (animCo!= null)
        {
            StopCoroutine(animCo);
            animCo = null;
        }
        //씬 이동함수 호출
        onClick?.Invoke(Click.Start);

    }

    public void OptionBtnClick()
    {
        onClick?.Invoke(Click.Option);
    }

    public void ExitBtnClick()
    {
        onClick?.Invoke(Click.Exit);
    }
}

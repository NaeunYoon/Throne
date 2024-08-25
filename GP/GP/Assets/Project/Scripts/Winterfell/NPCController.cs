using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public PlayerManager player = null;
    private NavMeshAgent nav = null;
    public bool isFollowing = false;
    private Action onFinished = null;
    private void Start()
    {
        //player = FindObjectOfType<PlayerManager>();
        nav = this.GetComponent<NavMeshAgent>();
        isFollowing = false;
        //nav.destination = player.transform.position;
    }

    public void FirstDialog()
    {
        onFinished = PrincessDialog;
        App.inst.uiMgr.monologue.Show();
        string maidTxt = "윈터펠에 오신 것을 환영합니다. 이 곳은 온통 눈으로 둘러쌓여 외롭지만 그래서 아름다운 곳이지요. ";
        App.inst.uiMgr.monologue.TextEffect(onFinished, maidTxt,App.inst.uiMgr.monologue.portraitArr[1],8f);
    }

    public void PrincessDialog()
    {
        onFinished = SecondDialog;
        string princessTxt = "환대에 감사합니다. 당신은 누구인가요? ";
        App.inst.uiMgr.monologue.TextEffect(onFinished, princessTxt,App.inst.uiMgr.monologue.portraitArr[0],5f);
    }

    public void SecondDialog()
    {
        onFinished = NullOnFinished;
        string maidTxt = "저는 황녀<플레이어> 님의 시종 웨슬리입니다. 궁금하신 점이 있다면 저에게 물어봐주세요. ";
        App.inst.uiMgr.monologue.TextEffect(onFinished, maidTxt,App.inst.uiMgr.monologue.portraitArr[1],5f);
    }

    public void NullOnFinished()
    {
        onFinished = null;
        App.inst.uiMgr.monologue.Hide();
    }
    
    
    
    private void Update()
    {
        if (isFollowing)
        {
            nav.destination = player.transform.position;
            FirstDialog();
            isFollowing = false;
        }
        
    }
}

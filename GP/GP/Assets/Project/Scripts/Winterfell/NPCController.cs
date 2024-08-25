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
        string maidTxt = "�����翡 ���� ���� ȯ���մϴ�. �� ���� ���� ������ �ѷ��׿� �ܷ����� �׷��� �Ƹ��ٿ� ��������. ";
        App.inst.uiMgr.monologue.TextEffect(onFinished, maidTxt,App.inst.uiMgr.monologue.portraitArr[1],8f);
    }

    public void PrincessDialog()
    {
        onFinished = SecondDialog;
        string princessTxt = "ȯ�뿡 �����մϴ�. ����� �����ΰ���? ";
        App.inst.uiMgr.monologue.TextEffect(onFinished, princessTxt,App.inst.uiMgr.monologue.portraitArr[0],5f);
    }

    public void SecondDialog()
    {
        onFinished = NullOnFinished;
        string maidTxt = "���� Ȳ��<�÷��̾�> ���� ���� �������Դϴ�. �ñ��Ͻ� ���� �ִٸ� ������ ������ּ���. ";
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

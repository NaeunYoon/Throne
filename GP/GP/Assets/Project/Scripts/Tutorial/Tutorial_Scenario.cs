using System;
using DG.Tweening;
using Cinemachine;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial_Scenario : UIBase
{
    [Header("Components")]
    public TextMeshProUGUI backgroundExplain = null;
    [SerializeField] private Sprite[] portraitImgArr = new Sprite[3];
    [Space(5f)] [CanBeNull] private Action onFinished = null;
    [Space(5f)][Header("DoPath")]
    public GameObject[] path;
    private Vector3[] wayPoint;
    [Space(5f)][Header("Player")]
    public Transform princess = null;
    private Animator princessAnim = null; 
    [Space(5f)][Header("Cams")]
    public CinemachineVirtualCamera dollyCam = null;
    private CinemachineTrackedDolly cam = null;
    public CinemachineVirtualCamera forwardCam = null;
    public CinemachineVirtualCamera backwardCam = null;
    public CinemachineVirtualCamera endCam = null;
    [Space(5f)][Header("Manager")] [SerializeField] private Scene_Menu_Tutorial _menu = null;


    public void Start()
    {
        wayPoint = new Vector3[path.Length];
        onFinished = FirstMonologue;
        cam = dollyCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        cam.m_PathPosition = 0;
        princessAnim = princess.GetComponent<Animator>();
        var pos = cam.m_PathPosition;
        BackgroundExplanation(onFinished);
    }
    
    // public override void Create()
    // {
    //     var pos = cam.m_PathPosition = 0;
    //     while (pos == 4)
    //     {
    //         FirstMonologue();
    //         break;
    //     }
    // }

    public void BackgroundExplanation(Action onFinished)
    {
        backgroundExplain.text = 
        "아무개 왕국 힘의 원천인 파멸의 씨앗이 사라지고, 황녀 <플레이어> 는 유력한 용의자로 몰리게 된다...";
        TextEffect(onFinished,backgroundExplain, 10f);
    }
    
    public void FirstMonologue()
    {
        onFinished = Dopath;
        backgroundExplain.text = string.Empty;
        //backgroundExplain.gameObject.SetActive(false);
        //onFinished = null;
        dollyCam.gameObject.SetActive(false);
        backwardCam.gameObject.SetActive(false);
        forwardCam.gameObject.SetActive(true);
        backgroundExplain.alignment = TextAlignmentOptions.Center;
        backgroundExplain.text = "어쩌다가 이렇게 된 걸까...난 결백한데.....";
        TextEffect(onFinished,backgroundExplain,10f);
    }
    private Tweener sequence = null;
    public void TextEffect(Action onfinished,TextMeshProUGUI txt, float duration)
    {
        txt.maxVisibleCharacters = 0;
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] = path[i].transform.position;
        }

        sequence = DOTween.To(x => txt.maxVisibleCharacters = (int)x,
                0f,
                txt.text.Length, duration)
            .SetEase(Ease.Linear)
            .SetDelay(1f)
            .OnComplete(() =>
            {
                //txt.gameObject.SetActive(false);
                if (onfinished != null)
                    onfinished?.Invoke();
            });;
    }

    public void Dopath()
    {
        if (backgroundExplain.gameObject.activeSelf==true)
        {
            backgroundExplain.gameObject.SetActive(false);
        }
        onFinished = null;
        forwardCam.gameObject.SetActive(false);
        backwardCam.gameObject.SetActive(true);
        //path를 사용해서 성당 안까지 들어간다
        princessAnim.SetInteger("anim", 1);
        princess.gameObject.transform.DOPath(wayPoint, 25f, PathType.CatmullRom)
            .SetLookAt(-1f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                princess.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
                princessAnim.SetInteger("anim", 0);
                PriestSequence();
            });
    }

    public void PriestSequence()
    {
        onFinished = PrincessSequence;
        App.inst.uiMgr.monologue.Show();
        string priestTxt = "죄인은 무릎을 꿇어라...";
        App.inst.uiMgr.monologue.TextEffect(onFinished, priestTxt,portraitImgArr[1],5f);
    }

    public void PrincessSequence()
    {
        princessAnim.SetTrigger("talk");
        onFinished = SisterSequence;
        string princessText = "지은 죄가 없어 꿇을 수 없습니다.. 저는 결백하다구요...";
        App.inst.uiMgr.monologue.TextEffect(onFinished,princessText,portraitImgArr[0],8f);
    }
    public void SisterSequence()
    {
        onFinished = PriestSecondSequence;
        endCam.gameObject.SetActive(true);
        string sisterText = "<플레이어> 언니가 훔치는 것을 제가 봤어요! (ㅋㅋ)..";
        App.inst.uiMgr.monologue.TextEffect(onFinished,sisterText,portraitImgArr[2],5f);
    }

    public void PriestSecondSequence()
    {
        onFinished = EndConversation;
        string priestTxt = "황녀<플레이어>는 성당에서 봉인하고 있는 파멸의 씨앗을 훔친 죄가 중해 사형에 처해지는 것이 마땅하나," +
                           "\n신분을 고려하여 황족의 신분을 박탈하고 윈터펠로 추방한다...";
        App.inst.uiMgr.monologue.TextEffect(onFinished,priestTxt,portraitImgArr[1],15f);
        princessAnim.SetTrigger("cry");
    }

    public void EndConversation()
    {
        onFinished = ShowBtn;
        App.inst.uiMgr.monologue.Hide();
        backgroundExplain.gameObject.SetActive(true);
        backgroundExplain.text = "하나밖에 없는 내 동생이 날 모함하다니..이대로 끝날 순 없어..";
        TextEffect(onFinished,backgroundExplain,10f);
    }

    public void ShowBtn()
    {
        onFinished = null;
        endCam.gameObject.SetActive(false);
        _menu.runBtn.gameObject.SetActive(true);
        _menu.SurrenderBtn.gameObject.SetActive(true);
    }
    
}

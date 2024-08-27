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
        "세르세이, 조프리, 일린 페인, 마운틴, 하운드.. " +System.Environment.NewLine+
        "살생부에 적힌 이름을 읇지 않고는 잠을 잘 수 없어..!";
        TextEffect(onFinished,backgroundExplain, 13f);
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
        backgroundExplain.text = "난 아무도 아닌 자가 되기 위해 브라보스에 있는 다면신의 신전에 왔어."
        +System.Environment.NewLine+" 이대로 물러설 순 없다고...";
        TextEffect(onFinished,backgroundExplain,13f);
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
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
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
        string priestTxt = "소녀는 누구인가?";
        App.inst.uiMgr.monologue.TextEffect(onFinished, priestTxt,portraitImgArr[1],2f);
    }

    public void PrincessSequence()
    {
        princessAnim.SetTrigger("talk");
        onFinished = SisterSequence;
        string princessText = "난 아무도 아니야.";
        App.inst.uiMgr.monologue.TextEffect(onFinished,princessText,portraitImgArr[0],2f);
    }
    public void SisterSequence()
    {
        onFinished = PriestSecondSequence;
        endCam.gameObject.SetActive(true);
        string sisterText = "넌 준비가 되지 않았어. 스타크 가의 귀족 아가씨, 아리아  ";
        App.inst.uiMgr.monologue.TextEffect(onFinished,sisterText,portraitImgArr[2],5f);
    }

    public void PriestSecondSequence()
    {
        onFinished = EndConversation;
        string priestTxt = "지금까지 다면신을 모시는 얼굴 없는 자 중에는 귀족은 없었다, "+System.Environment.NewLine+"소녀는 준비가 되었다는 것을 증명 해 내야 한다";
        App.inst.uiMgr.monologue.TextEffect(onFinished,priestTxt,portraitImgArr[1],10f);
        princessAnim.SetTrigger("cry");
    }

    public void EndConversation()
    {
        onFinished = ShowBtn;
        App.inst.uiMgr.monologue.Hide();
        backgroundExplain.gameObject.SetActive(true);
        backgroundExplain.text = "난 내 출신과 욕망을 모두 버렸어."+System.Environment.NewLine+" 나는 얼굴 없는 자가 될 거야.";
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

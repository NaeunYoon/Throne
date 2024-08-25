using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : UIBase
{
    public Image portrait = null;
    public TextMeshProUGUI userName = null;
    public int level = 0;
    public TextMeshProUGUI ability = null;

    public void InitUserInfo(Sprite sprite, string name, int lv, string jobInfo )
    {
        Debug.Log("tttttttttt"+sprite +""+name+""+lv+""+jobInfo);
        portrait.sprite = sprite;
        userName.text = name;
        level = lv;
        ability.text = jobInfo;
    }
    
}

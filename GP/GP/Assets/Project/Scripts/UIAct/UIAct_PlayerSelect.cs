using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAct_PlayerSelect : UICommonMgr
{
    public GameObject BG = null;
    public GameObject GAMEBOARD = null;
    public Scene_Menu_PlayerSelect MENU = null;

    public override void Create()
    {
        Debug.Log("Title.UIAct_PlayerSelect.Create.0");
        MENU.Create();
    }
    
    
    public override void Delete()
    {
        MENU.Delete();
    }
}

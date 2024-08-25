using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAct_Title : UICommonMgr
{
    public GameObject BG = null;
    public Scene_Menu_Title MENU = null;
    public GameObject GAMEBOARD = null;

    public override void Create()
    {
        Debug.Log("Title.UIAct_Title.Create.0");
        MENU.Create();
    }
    public override void Delete()
    {
        MENU.Delete();
    }
}

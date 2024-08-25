using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAct_Winterfell : UICommonMgr
{
    public GameObject BG = null;
    public Scene_Menu_Winterfell MENU = null;
    public GameObject GAMEBOARD = null;

    public override void Create()
    {
        Debug.Log("Tutorial.UIAct_Winterfell.Create.0");
        MENU.Create();
    }
    public override void Delete()
    {
        MENU.Delete();
    }
}

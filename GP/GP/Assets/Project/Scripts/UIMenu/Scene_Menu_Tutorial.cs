using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UICommonMenu;

public class Scene_Menu_Tutorial : UICommonMenu
{
    public Button runBtn = null;
    public Button SurrenderBtn = null;
    
    public override void Create()
    {
        base.Create();
        
        runBtn.onClick.AddListener(()=>RunBtnClick());
        SurrenderBtn.onClick.AddListener(()=>SurrenderBtnClick());
        
        AudioController.Stop("Title");
        AudioController.Play("Tutorial");
    }
    public void RunBtnClick()
    {
        onClick?.Invoke(Click.Start);
    }

    public void SurrenderBtnClick()
    {
        onClick?.Invoke(Click.Next);
    }

    public void OptionBtnClick()
    {
        onClick?.Invoke(Click.Exit);
    }
}

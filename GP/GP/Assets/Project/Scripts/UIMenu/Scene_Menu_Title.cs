using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Menu_Title : UICommonMenu
{
    public GameObject OptionPanel;

    public override void Create()
    {
        base.Create();
        AudioController.Play("Title");
    }

    public void StartBtnClick()
    {
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

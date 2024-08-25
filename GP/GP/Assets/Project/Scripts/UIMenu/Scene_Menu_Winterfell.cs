using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UICommonMenu;

public class Scene_Menu_Winterfell : UICommonMenu
{
    public Material skyboxMat = null;
    public override void Create()
    {
        base.Create();
        RenderSettings.skybox = skyboxMat;
        AudioController.Stop("Tutorial");
        AudioController.Play("Winterfell");
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

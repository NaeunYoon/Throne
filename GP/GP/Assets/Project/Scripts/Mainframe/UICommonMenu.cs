using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICommonMenu : UIBase
{
    public Action<Click> onClick { get; set; } = null;
    public enum Click
    {
        Start,
        Option,
        Inventory,
        Exit,

        GO,
        Back,
        Done,
        Next,
        Finish,
    }

    public virtual void Create()
    {

    }

    public virtual void Delete()
    {
        Stop();
    }


    public void OnOptionBtnClick()
    {
        onClick?.Invoke(Click.Option);
    }


}

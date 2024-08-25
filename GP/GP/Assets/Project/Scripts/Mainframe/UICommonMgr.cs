using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UICommonMgr : MonoBehaviour
{
    public UIBase uiBase = null;
    public Controller controller = null;
    public UserInfo userInfo = null;
    public MonologueMgr monologue = null;
    public Fader fader = null;
    public VirtualJoystick joystick = null;
    public InventoryMgr inventory = null;
    public StoreMgr store = null;
    public virtual void Create() 
    {
        uiBase.Create();
        //uiBase.Hide();
        controller.Hide();
        userInfo.Hide();
        monologue.Hide();
        joystick.Hide();
        inventory.Create();
        inventory.Hide();
        store.Create();
        //store.Show();
        //store.Hide();
    }

    public virtual void Delete() 
    {
        store.Delete();
        inventory.Delete();
        joystick.Delete();
        monologue.Delete();
        userInfo.Delete();
        controller.Delete();
        uiBase.Delete();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    private InventoryItems items = new InventoryItems();

    public List<string> AddInventory(string name)
    {
        items.itemName.Add(name);
        return items.itemName;
    }
    
    public string playerName = null;
    public int playerLevel = 0;
    public float playerHP = 100;
    public float playerMP = 100;
    public int playerGold = 0;
    public string playerJob = null;
}

[Serializable]
public class InventoryItems
{
    public List<string> itemName = new List<string>();
}

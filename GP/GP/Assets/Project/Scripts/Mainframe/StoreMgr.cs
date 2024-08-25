using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;

public enum ItemType
{
  None,
  Edible,
  Usable,
  Magician,
  Bow,
  Warrior
}
public class StoreMgr : UIBase
{
  [Header("store")]
  public Transform contents = null;
  public StoreSlot itemPrefab = null;
  public int storeSlotCnt = 0;
  public Dictionary<string, Enum> itemType = new Dictionary<string, Enum>();
  public ItemType itemTypeForStore = ItemType.None;
  [Header("itemTypes")]
  [Space(5f)]
  public List<Sprite> edible = new List<Sprite>();
  public List<Sprite> usable = new List<Sprite>();
  public List<Sprite> magician = new List<Sprite>();
  public List<Sprite> bow = new List<Sprite>();
  public List<Sprite> warrior = new List<Sprite>();
  [Header("itemData")]
  [Space(5f)]
  public List<ItemInfo> itemData = new List<ItemInfo>();
  
  [Header("etc")]
  [Space(5f)]
  private int itemAmount = 3;
  private int ran = 0;
  private List<Sprite> temp;
  private Action<List<Sprite>,ItemType> InitStoreItem;
  public InventoryMgr inventoryMgr = null;
  public Button exitBtn = null;
  /// <summary>
  /// �ʿ��� edible usable�� �����ϰ� �������� �б�ó���ؾ���. �ΰ��� �ϳ��� ����
  /// action�̳� delegate �Ἥ ������Ƽ �ȿ��� ����
  /// </summary>
  public override void Create()
  {
    GetItemData();
    //setBasicStore += SetEdible(edible);
    //SetEdible(edible);
    exitBtn.onClick.AddListener(()=>OnClickExitBtn());
    // ������ �� invoke�� ���� �־��ֱ�
    InitStoreItem += SetBasicItem;
    InitStoreItem.Invoke(edible,ItemType.Edible);
    InitStoreItem.Invoke(usable,ItemType.Usable);

    //SetBasicItem(edible, ItemType.Edible);

  }

  public void SetBasicItem(List<Sprite> t_itemList, ItemType t_type)
  {
    temp = new List<Sprite>(t_itemList);
    itemTypeForStore = t_type;
    
    for (int i = 0; i < itemAmount; i++)
    {
      ran = UnityEngine.Random.Range(0, temp.Count);
      var index = t_itemList.FindIndex(x => x.name.Equals(temp[ran].name));
      temp.RemoveAt(ran);
      itemType.Add(t_itemList[index].ToString(),itemTypeForStore);
      StoreSlot go = Instantiate(itemPrefab, contents);
      go.Init(inventoryMgr);
      go.itemImg.sprite = t_itemList[index];

      for (int j = 0; j < itemData.Count; j++)
      {
        if (itemData[j].itemName == t_itemList[index].name)
        {
          go.itemType = itemData[j].itemType.ToString();
          go.itemName.text = itemData[j].itemName;
          go.description = itemData[j].itemExplain;
          go.itemExplain.text = itemData[j].itemExplain;
          go.ability = itemData[j].itemAbility;
          break;
        }
      }
    }
    temp.Clear();
  }

 
  
  
  public virtual void Delete()
  {
    base.Delete();
    itemType.Clear();
      
  }

  public void OnClickExitBtn()
  {
    if (IsShow())
    {
      Hide();
    }
    else
    {
      Show();
    }
  }

  public void SetItemData()
  {
    ItemInfo info = new ItemInfo("", 0, 0, "", ItemType.None);
    itemData.Add(info);

    // JSON ���� ��� ����
    string jsonFilePath = Path.Combine(Application.dataPath+"/Resources/", "item_info.json");

    // ItemInfo ��ü�� JSON���� ����ȭ
    string json = JsonConvert.SerializeObject(itemData);

    // JSON �����͸� ���Ͽ� ����
    File.WriteAllText(jsonFilePath, json);

    Debug.Log("restored JSON file in Resources folder");
  }
  
  public void GetItemData()
  {
    string jsonFilePath = Application.dataPath+"/Resources/item_info.json";

    string json;
    using (StreamReader sr = new StreamReader(jsonFilePath, Encoding.UTF8))
    {
      json = sr.ReadToEnd();
    }
    
    // JSON ������ �о�ͼ� ���ڿ��� ����
    //json = File.ReadAllText(jsonFilePath,Encoding.UTF8);

    // JSON ���ڿ��� List<ItemInfo> ��ü�� ��ȯ
    itemData = JsonConvert.DeserializeObject<List<ItemInfo>>(json);
    
  }
}

public class ItemInfo
{
  public string itemName = "";
  public int itemAbility = 0;
  public int itemPrice = 0;
  public string itemExplain = "";
  public ItemType itemType = ItemType.None;

  public ItemInfo(string name, int av, int price, string explain, ItemType type)
  {
    this.itemName = name;
    this.itemAbility = av;
    this.itemPrice = price;
    this.itemExplain = explain;
    this.itemType = type;
  }
  
}

public class ItemforChar
{
  public string itemName = "";
  public int itemPrice = 0;
  public string itemExplain = "";
  public string job = "";
}
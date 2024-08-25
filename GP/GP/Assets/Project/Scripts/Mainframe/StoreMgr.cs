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
  /// 필요한 edible usable을 제외하고 나머지는 분기처리해야함. 두개는 하나로 묶기
  /// action이나 delegate 써서 프로퍼티 안에서 접근
  /// </summary>
  public override void Create()
  {
    GetItemData();
    //setBasicStore += SetEdible(edible);
    //SetEdible(edible);
    exitBtn.onClick.AddListener(()=>OnClickExitBtn());
    // 셋팅할 때 invoke에 인자 넣어주기
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

    // JSON 파일 경로 설정
    string jsonFilePath = Path.Combine(Application.dataPath+"/Resources/", "item_info.json");

    // ItemInfo 객체를 JSON으로 직렬화
    string json = JsonConvert.SerializeObject(itemData);

    // JSON 데이터를 파일에 저장
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
    
    // JSON 파일을 읽어와서 문자열로 저장
    //json = File.ReadAllText(jsonFilePath,Encoding.UTF8);

    // JSON 문자열을 List<ItemInfo> 객체로 변환
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
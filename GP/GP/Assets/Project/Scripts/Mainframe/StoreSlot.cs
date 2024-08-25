using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
   public Image itemImg = null;
   public TextMeshProUGUI itemName = null;
   public TextMeshProUGUI itemExplain = null;
   public Button buyBtn = null;
   public string itemType = null;
   
   private InventoryMgr inventoryMgr = null;

   [Header("item_data")]
   public string description = null;
   public int ability = 0;
   
   public void Init(InventoryMgr inven)
   {
      inventoryMgr = inven;
      buyBtn.onClick.AddListener(() => { OnclickBuyBtn(); });
   }
   
   public void OnclickBuyBtn()
   {
      for (int i = 0; i < inventoryMgr.slots.Count; i++)
      {
         if (inventoryMgr.slots[i].isEmpty)
         {
            BuyItem(inventoryMgr.slots[i]);
            inventoryMgr.slots[i].isEmpty = false;
            break;
         }
         else
         {
            if (inventoryMgr.slots[i].itemName == this.itemName.text)
            {
               Debug.Log("test"+inventoryMgr.slots[i].itemName+""+this.itemName.text);
               inventoryMgr.slots[i].cnt++;
               inventoryMgr.slots[i].cntTxt.text = inventoryMgr.slots[i].cnt.ToString();
               break;
            }
         }
      }
   }

   public void BuyItem(SlotMgr slot)
   {
      slot.img.sprite = this.itemImg.sprite;
      slot.itemName = this.itemName.text;
      slot.cnt++;
      slot.cntTxt.text = slot.cnt.ToString();
      slot.description = this.description;
      slot.ability = this.ability;
   }
   
   
}

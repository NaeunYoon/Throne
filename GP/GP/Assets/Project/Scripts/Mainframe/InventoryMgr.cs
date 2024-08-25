using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UIElements.Image;

public class InventoryMgr : UIBase
{
   [Header("Inven")]
   [SerializeField] private Transform _contents = null;
   public GameObject slotprefab = null;
   public int slotCnt = 0;
   public List<SlotMgr> slots = new List<SlotMgr>();
   public Button exitBtn = null;
   public Button inventoryIcon = null;
   [Space(5f)] 
   [Header("SubInven")] 
   public GameObject subInven = null;

   //public Image subImg = null;

   public UnityEngine.UI.Image subImg = null;
   //public Image subImg = null;
   public TextMeshProUGUI subInfo = null;
   public Button useBtn = null;
   public Button throwBtn = null;
   public override void Create()
   {
      //OnClickIcon();
      slotCnt = _contents.transform.childCount;
      inventoryIcon.onClick.AddListener(()=>OnClickIcon());
      exitBtn.onClick.AddListener(()=>OnClickExitBtn());
      useBtn.onClick.AddListener(()=>OnclickUseBtn());
      throwBtn.onClick.AddListener(()=>OnClickThrowBtn());
      for (int i = 0; i < slotCnt; i++)
      {
         var slot = _contents.transform.GetChild(i).GetComponent<SlotMgr>();
         slot.Init(this);
         slot.isEmpty = true;
         slots.Add(slot);
      }
   }

   public void OnClickExitBtn()
   {
      Hide();
   }
   public void OnClickIcon()
   {
      if (IsShow())
         Hide();
      else
         Show();
   }


   public override void Hide()
   {
      base.Hide();
      subInven.gameObject.SetActive(false);
   }

   public void OnclickUseBtn()
   {
      Debug.Log("OnclickUseBtn");
   }

   public void OnClickThrowBtn()
   {
      Debug.Log("OnClickThrowBtn");
   }

   public override void Show()
   {
      base.Show();
   }
}

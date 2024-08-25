using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotMgr : MonoBehaviour
{
    public string itemName = null;
    public Image img = null;
    public TextMeshProUGUI cntTxt = null;
    public bool isEmpty = true;
    public int cnt = 0;
    private InventoryMgr _invenMgr;
    private Button btn = null;
    
    [Header("item_data")]
    public string description = null;
    public int ability = 0;
    
    
    public void Init(InventoryMgr inven)
    {
        _invenMgr = inven;
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(()=>OnClickItem());

    }
    
    public void OnClickItem()
    {
        if(_invenMgr.subInven.activeSelf == false)
            _invenMgr.subInven.SetActive(true);
        
        _invenMgr.subImg.sprite = this.img.sprite;
        Debug.Log(description);
        _invenMgr.subInfo.text = description+ " ability : "+ ability;
        
    }
    
    
    
    
}

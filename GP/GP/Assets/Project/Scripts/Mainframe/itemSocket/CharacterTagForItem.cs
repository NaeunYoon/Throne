using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTagForItem : MonoBehaviour
{
    [Header("ItemTagSocket")] 
    public ItemTagSocket itemTagObject = null;

    public void Reset()
    {
        if (itemTagObject == null)
        {
            itemTagObject = new ItemTagSocket();
            //itemTagObject.tag = App.inst.DefaultItemTag;
        }
    }
}


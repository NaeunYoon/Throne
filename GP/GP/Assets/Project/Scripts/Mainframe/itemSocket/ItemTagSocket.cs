using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/ItemTag",fileName = "ItemTag",order = int.MinValue)]
public class ItemTagSocket : ScriptableObject
{
    [SerializeField] private string[] CharacterType = new string[10];
    
    public string this[int index]
    {
        get { return CharacterType[index]; }
        set { CharacterType[index] = value; }
    }
    
}

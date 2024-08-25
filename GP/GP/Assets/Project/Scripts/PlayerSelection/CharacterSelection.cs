using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] Characters = new GameObject[3];
    public string[] jobName = new string[3] { "Magician","Warrior","Archer"};
    public int selectedCharacter = 0;
}

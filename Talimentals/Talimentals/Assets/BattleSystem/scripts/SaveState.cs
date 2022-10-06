using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
   // public string currentRoom;
    //saving the inventory
    public string team = string.Join(" ", GameManager.instance.team);
}
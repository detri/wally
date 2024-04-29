using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public Sprite icon;
    public string characterName;
    public GameObject characterPrefab;
    public GameObject startingWeapon;
}

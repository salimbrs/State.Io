using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : EnemyManager
{
    public TextMeshPro armyNoTxt;
    //public SpriteRenderer territorySprite;

    private void Start()
    {
        armyNoTxt = transform.GetChild(0).GetComponent<TextMeshPro>();
        armyNoTxt.text = ArmyNo.ToString();
        TerritorySprite = transform.parent.GetComponent<SpriteRenderer>();
    }
}
   


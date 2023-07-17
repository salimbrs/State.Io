using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Attack
{
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("enemy") && other.gameObject.name == PlayerManager.playerManagerInstance.enemy.name)
        {
            
            other.GetComponent<Enemy>().UnderTheAtatch(other.GetComponent<Enemy>().armyNoTxt);
            gameObject.SetActive(false);
        }
        
    }
}

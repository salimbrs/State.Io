using TMPro;
using UnityEngine;
public abstract class EnemyManager : MonoBehaviour
{
    public int ArmyNo, PlayerArmyNo;
    public SpriteRenderer TerritorySprite;
    private void Awake()
    {
        ArmyNo = Random.Range(10, 25);
    }

    public void UnderTheAtatch(TextMeshPro ArmyNoTxt)
    {
        if (ArmyNo > 0)
        {
            ArmyNo--;
            ArmyNoTxt.text = ArmyNo.ToString();
        }
        else
        {
            PlayerArmyNo++;
            ArmyNoTxt.text = PlayerArmyNo++.ToString();
        }
        if (ArmyNo==0)
        {
            TerritorySprite.color = new Color(32f, 173f, 253f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(32f, 173f, 253f);
        }
    }



}

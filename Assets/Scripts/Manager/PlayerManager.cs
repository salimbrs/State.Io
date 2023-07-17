using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header(" Elements")]
    public static PlayerManager playerManagerInstance;
    [SerializeField] TextMeshPro ArmyNoTxt;
    public List<GameObject> group = new List<GameObject>();
    [SerializeField] float[] angles;
    [SerializeField] Attack soldierPref;
    [SerializeField] float SpearTime;


    [Header(" Settings ")]
    int counter;
    int armyNo;
    IEnumerator IeGatherArmy,IeGenerateTheSoldier;
    Vector3 offSet, intialPos;
    [SerializeField] float nearClib;
    public  bool drag;
    Camera cam;
   public Transform enemy;
    private void Start()
    {
        playerManagerInstance = this;
        cam = Camera.main;
        intialPos = transform.localPosition;
       
        StartCoroutine(GatherTheArmy());
        
    }

    
    IEnumerator GatherTheArmy()
    {
        while (counter<10)
        {
            counter++;
            ArmyNoTxt.text = counter.ToString();
            armyNo = counter;
          
            yield return new WaitForSeconds(1f);
        }
       
    }

    private void Update()
    {
        MouseInput();
        //FormatTime();
    }
    void MouseInput()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, nearClib));
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider!=null && hit.collider.CompareTag("pointer"))
                {
                    
                    offSet = transform.position - mousePos;
                    drag = true;
                  
                }
            }
        }

        if (drag)
            transform.position = offSet + mousePos;

        if (Input.GetMouseButtonUp(0))
        {
            drag = false;
            transform.localPosition = intialPos;
            
            if (enemy != null)
            {
                IeGenerateTheSoldier = GenerateTheSoldier();
                StartCoroutine(IeGenerateTheSoldier);

               
                StopCoroutine(GatherTheArmy());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            
        }
       
    }


    IEnumerator GenerateTheSoldier()
    {
        var soldierNo = 0;
        var playerFakeSoldierNo = armyNo;
        var maxSoldierPerBatch = 3;
        

        while (soldierNo < playerFakeSoldierNo)
        {
            var soldierToGenerate = Mathf.Min(maxSoldierPerBatch, playerFakeSoldierNo - soldierNo);

            for (int i = 0; i < soldierToGenerate; i++)
            {
                var newSoldier = Instantiate(soldierPref, transform.position, Quaternion.identity);
                group.Add(newSoldier.gameObject);
                newSoldier.ExecuteOrder(enemy.transform.position, angles[i]);
            }

            soldierNo += soldierToGenerate;
            armyNo = 0;
            ArmyNoTxt.text = armyNo.ToString();
         
            yield return new WaitForSeconds(SpearTime);
            
        }

        Invoke("RunGatherArmy", 1f);
        
        
    }

    void RunGatherArmy()
    {
       
        StartCoroutine(GatherTheArmy());
    }

 
}

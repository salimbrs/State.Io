using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Attack : MonoBehaviour
{
    public float soldierDis, directionScale;
    Vector3 _targetPos, _finalPos, _distance;
    bool _spreadCompleted;


    private void Update()
    {
        OffSetSettings();
    }
    public void ExecuteOrder(Vector3 target, float angle)
    {
        var direction = Quaternion.Euler(0, 0, angle) * (target - transform.position);

        _finalPos = target;
        var sqrMg = target - transform.position;

        directionScale = sqrMg.sqrMagnitude >= 30f ? 0.15f : 0.3f;
        _targetPos = transform.position + direction * directionScale; ;
    }

    void OffSetSettings()
    {
        var offSetFinalTarget = Vector3.Distance(_finalPos, transform.position);
        var offSetTarget = Vector3.Distance(_targetPos, transform.position);

        if (offSetTarget > 0.3f && !_spreadCompleted)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * 2f);
        }
        else
        {
            _spreadCompleted = true;

            if (offSetFinalTarget > 1.5f)
            {
                for (int i = 0; i < PlayerManager.playerManagerInstance.group.Count; i++)
                {
                    _distance = transform.position - PlayerManager.playerManagerInstance.group.ElementAt(i).transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, _finalPos + _distance * soldierDis, Time.deltaTime * 3f);
               
                
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _finalPos, Time.deltaTime * 2f);
            }
        }
    }
}

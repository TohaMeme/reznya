using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveToComplitedReason
{
    Success,
    Failure,
    Aborted
}

[RequireComponent(typeof(AISense))]
public class AIController : BaseCharacterController
{
    bool isMoveToCompleted = true;
    int pathPointIndex;

    NavMeshPath path;
    AISense sense;

    public AISense Sense => sense;

    Action<MoveToComplitedReason> moveToComplited;


    protected override void Awake()
    {
        base.Awake();

        sense = GetComponent<AISense>();
        path = new NavMeshPath();
    }
    public bool MoveTo(Vector3 targetPos, Action<MoveToComplitedReason> complited = null)
    {

        if (!isMoveToCompleted)
            AbortMoveTo();
        

        moveToComplited = complited;

        bool hasPath =  NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);
        if (hasPath)
        {
            if (path.corners.Length == 1) 
            { 
                InvokeMoveToComplited(MoveToComplitedReason.Success); 
                return true; 
            }
            pathPointIndex = 1;
        }
            

        isMoveToCompleted = !hasPath;

        if (!hasPath)
            InvokeMoveToComplited(MoveToComplitedReason.Failure);


        return hasPath;
    }

    protected virtual void Update()
    {
        if(path.status != NavMeshPathStatus.PathInvalid)
        {
            for (int i = 0; i < path.corners.Length - 1; i++) 
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1]);
            }
        }
        
        if (isMoveToCompleted)
        {
            return;
        }

        Vector3 targetPos = path.corners[pathPointIndex];
        Vector3 sourcePos = transform.position;
        targetPos.y = 0;
        sourcePos.y = 0;

        if (Vector3.Distance(sourcePos, targetPos) < 1)
        {
            if(pathPointIndex + 1 >= path.corners.Length)
            {
                InvokeMoveToComplited(MoveToComplitedReason.Success);
                
                return;
            }
            pathPointIndex++;
            targetPos = path.corners[pathPointIndex];
            targetPos.y = 0;
        }

        Vector3 direction = (targetPos - sourcePos).normalized;

        SetRoation(Quaternion.LookRotation(direction, transform.up).eulerAngles.y);

        MoveWorld(direction.x, direction.z);
    }

    private void InvokeMoveToComplited(MoveToComplitedReason reason)
    {

        isMoveToCompleted = true;

        Action<MoveToComplitedReason> action = moveToComplited;
        moveToComplited = null;
        action?.Invoke(MoveToComplitedReason.Success);
    }

    public void AbortMoveTo()
    {
        InvokeMoveToComplited(MoveToComplitedReason.Aborted);
    }
}

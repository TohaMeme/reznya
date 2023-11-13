using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class RoamingAIState : AIState
{
    public AIController AIController { get; }

    public RoamingAIState(AIController aIController, AIStateMachine stateMachine) : base(stateMachine)
    {
        AIController = aIController;
    }
    public override void Enable()
    {
        AIController.MoveTo(GetRandomPositionInRadius(10), HandleMoveToComplited);
    }

    public override void Disable()
    {

    }

    void HandleMoveToComplited(MoveToComplitedReason reason)
    {
        Debug.Log(reason);

        if (reason == MoveToComplitedReason.Failure)
            return;

        ChangeState("Roaming");
    }

    Vector3 GetRandomPositionInRadius(float radius)
    {
        Vector3 randomDir = Random.insideUnitSphere * radius;
        Vector3 targetPos = AIController.transform.position + randomDir;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, radius, NavMesh.AllAreas))
            return hit.position;
        else
            return AIController.transform.position;
    }
}

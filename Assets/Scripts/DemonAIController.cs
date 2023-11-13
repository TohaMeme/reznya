using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class DemonAIController : AIController
{
    AIStateMachine stateMachine;
     void Start()
    {
        stateMachine = new AIStateMachine();

        stateMachine.AddState("Roaming", new RoamingAIState(this, stateMachine));
        stateMachine.SetActiveState("Roaming");
    }
}

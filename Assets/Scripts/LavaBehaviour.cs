using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : FloorType
{
    [SerializeField] int damageAmount;
    DamagableComponent _damagableComponent;

    public override void OnCharacterEnter(PlayerController controller)
    {
            _damagableComponent = controller.gameObject.GetComponent<DamagableComponent>();
            InvokeRepeating("InLava", 0.2f, 1f);
    }

    public override void OnCharacterExit(PlayerController controller)
    {
            CancelInvoke("InLava");
    }

    public override void OnCharacterStay(PlayerController controller)
    {
        base.OnCharacterStay(controller);
    }

    void InLava()
    {
        _damagableComponent.DealDamage(damageAmount);
    }

}

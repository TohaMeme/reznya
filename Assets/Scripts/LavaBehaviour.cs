using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : FloorType
{
    [SerializeField] int damageAmount;
    DamagableComponent _damagableComponent;

    public override void OnCharacterEnter(DamagableComponent damagableComponent)
    {
            _damagableComponent = damagableComponent;
            InvokeRepeating(nameof(InLava), 0.2f, 1f);
    }

    public override void OnCharacterExit(DamagableComponent damagableComponent)
    {
            CancelInvoke(nameof(InLava));
    }

    public override void OnCharacterStay(DamagableComponent damagableComponent)
    {
        base.OnCharacterStay(damagableComponent);
    }

    void InLava()
    {
        _damagableComponent.DealDamage(damageAmount);
    }

}

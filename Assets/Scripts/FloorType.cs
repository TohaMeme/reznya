using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorType : MonoBehaviour
{
    public virtual  void OnCharacterStay(DamagableComponent damagableComponent)
    {
        print($"Lava player stay: {damagableComponent.name}");
    }

    public virtual void OnCharacterExit(DamagableComponent damagableComponent)
    {
        print($"Lava player exit: {damagableComponent.name}");
    }

    public virtual void OnCharacterEnter(DamagableComponent damagableComponent)
    {
        print($"Lava player enter: {damagableComponent.name}");
    }
}

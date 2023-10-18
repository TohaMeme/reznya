using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorType : MonoBehaviour
{
    public virtual  void OnCharacterStay(PlayerController controller)
    {
        print($"Lava player stay: {controller.name}");
    }

    public virtual void OnCharacterExit(PlayerController controller)
    {
        print($"Lava player exit: {controller.name}");
    }

    public virtual void OnCharacterEnter(PlayerController controller)
    {
        print($"Lava player enter: {controller.name}");
    }
}

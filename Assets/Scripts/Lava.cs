using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public void OnCharacterStay(PlayerController controller)
    {
        print($"Lava player stay: {controller.name}");
    }

    public void OnCharacterExit(PlayerController controller)
    {
        print($"Lava player exit: {controller.name}");
    }

    public void OnCharacterEnter(PlayerController controller)
    {
        print($"Lava player enter: {controller.name}");
    }
}

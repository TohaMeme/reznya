using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Aim : MonoBehaviour
{
    [SerializeField] Image aimImage;
   public bool CanShoot { get; set; }

    private void Update()
    {
        aimImage.color = CanShoot ? Color.red : Color.white;
    }
}

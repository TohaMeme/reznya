using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] UI_Aim aim;
    void Update()
    {
        DamagableComponent damagable =  EnemyManager.GetFirstVisibleTarget(transform, 3, Affiliation.Demon, 30);

        aim.CanShoot = damagable != null;
    }
}

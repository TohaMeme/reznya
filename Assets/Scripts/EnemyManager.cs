using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static HashSet<EnemyDamage> damagableComponents = new HashSet<EnemyDamage>();

    public static IReadOnlyCollection<EnemyDamage> Enemies => damagableComponents;

    public static void RegisterEnemy(EnemyDamage damagble)
    {
        damagableComponents.Add(damagble);
    }

    public static void UnregisterEnemy(EnemyDamage damagble)
    {
        damagableComponents.Remove(damagble);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    CharacterController characterController;
    static HashSet<DamagableComponent> damagableComponents = new HashSet<DamagableComponent>();

    public static IReadOnlyCollection<DamagableComponent> Enemies => damagableComponents;

    GameObject floor;
    GameObject Floor
    {
        get => floor;
        set
        {
            if (floor != value)
            {
                if (floor != null)
                    floor.SendMessage("OnCharacterExit", this.gameObject.GetComponent<DamagableComponent>(), SendMessageOptions.DontRequireReceiver);
                if (value != null)
                    value.SendMessage("OnCharacterEnter", this.gameObject.GetComponent<DamagableComponent>(), SendMessageOptions.DontRequireReceiver);
            }

            floor = value;

        }
    }

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();
    }
    public static void RegisterEnemy(DamagableComponent damagble)
    {
        damagableComponents.Add(damagble);
    }

    public static void UnregisterEnemy(DamagableComponent damagble)
    {
        damagableComponents.Remove(damagble);
    }

    public static DamagableComponent GetFirstVisibleTarget(Transform sourceTransform, float coneAngle, Affiliation affiliation, float maxDistance)
    {
        foreach (DamagableComponent enemy in EnemyManager.Enemies.Where(damagable => (damagable.Affiliation & affiliation) > 0))
        {
            Vector3 enemyDirection = (enemy.transform.position - sourceTransform.position);

            if (enemyDirection.sqrMagnitude > maxDistance * maxDistance) continue;

            Vector3 enemyDirection2D = enemyDirection;
            enemyDirection2D.y = 0;
            enemyDirection2D = enemyDirection.normalized;

            enemyDirection = enemyDirection.normalized;

            float angle = Mathf.Acos(Vector3.Dot(sourceTransform.forward, enemyDirection)) * Mathf.Rad2Deg;

            if (angle < 3)
            {

                CharacterController enemyCollider = enemy.GetComponent<CharacterController>();
                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);

                if (AimLineAttack(sourceTransform, enemy.transform.position)
                    && AimLineAttack(sourceTransform, enemy.transform.position + unitFrac)
                    && AimLineAttack(sourceTransform, enemy.transform.position - unitFrac))
                {
                    return enemy;
                }

            }

        }
        return null;
    }
    static bool AimLineAttack(Transform sourceTransform, Vector3 targetPos)
    {

        if (Physics.Linecast(sourceTransform.position, targetPos, out RaycastHit hit) && hit.collider.GetComponent<DamagableComponent>())
        {
            Debug.DrawLine(sourceTransform.position, targetPos, Color.green);
            return true;
        }
        return false;
    }

    void GroundCheck()
    {
        if (Physics.Linecast(transform.position, transform.position + Vector3.down * (characterController.height / 2 + 0.5f), out RaycastHit hit))
        {
            Floor = hit.collider.gameObject;
            Floor.SendMessage("OnCharacterStay", this.gameObject.GetComponent<DamagableComponent>(), SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            Floor = null;
        }
    }
}

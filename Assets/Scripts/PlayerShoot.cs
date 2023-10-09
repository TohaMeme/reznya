using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] UI_Aim aim;
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green);

        foreach(EnemyDamage enemy in EnemyManager.Enemies)
        {
            //transform.forward;
            Vector3 enemyDirection = (enemy.transform.position - transform.position);

            Vector3 enemyDirection2D = enemyDirection;
            enemyDirection2D.y = 0;
            enemyDirection2D =  enemyDirection.normalized;

            enemyDirection = enemyDirection.normalized;

            float angle = Mathf.Acos(Vector3.Dot(transform.forward, enemyDirection)) * Mathf.Rad2Deg;

            if (angle < 3) 
            {

                var enemyCollider = enemy.GetComponent<CapsuleCollider>();
                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);
                RaycastHit hit;

                if (AimLineAttack(enemy.transform.position) 
                    && AimLineAttack(enemy.transform.position + unitFrac)
                    && AimLineAttack(enemy.transform.position - unitFrac))
                {
                    aim.CanShoot = true;
                    return;

                }
                

                /*if(Physics.Linecast(transform.position, enemy.transform.position, out hit) && hit.collider.GetComponent<EnemyDamage>())
                {
                    Debug.DrawLine(transform.position, enemy.transform.position, Color.green);
                    aim.CanShoot = true;
                    return;
                }

                if (Physics.Linecast(transform.position, enemy.transform.position + unitFrac, out hit) && hit.collider.GetComponent<EnemyDamage>())
                {
                    Debug.DrawLine(transform.position, enemy.transform.position + unitFrac, Color.green);
                    aim.CanShoot = true;
                    return;
                }

                if (Physics.Linecast(transform.position, enemy.transform.position - unitFrac, out hit) && hit.collider.GetComponent<EnemyDamage>())
                {
                    Debug.DrawLine(transform.position, enemy.transform.position - unitFrac, Color.green);
                    aim.CanShoot = true;
                    return;
                }*/

            }

         

        }
        aim.CanShoot = false;
 
    }
    bool AimLineAttack(Vector3 targetPos)
    {

        if (Physics.Linecast(transform.position, targetPos, out RaycastHit hit) && hit.collider.GetComponent<EnemyDamage>())
        {
            Debug.DrawLine(transform.position, targetPos, Color.green);
            return true;
        }
        return false;
    }
}

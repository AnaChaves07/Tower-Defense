using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turrets : BaseTurret //Torreta normal com heran�a 
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetinRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float timeUntilFire;
    private float timeUntilNextShot;

    private void Update()//Verifica se existe algum alvo e se tiver chama os outros m�todos 
    {
        if (target == null)
        {
            FindTarget();
             return;
        }
       
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
            return;
        }    
                timeUntilFire += Time.deltaTime;
                if (timeUntilFire >= 1f/bps)
                {
                    Shoot();
                timeUntilFire = 0f;
                }
    }
    public override void Shoot()//Usa o prefab da bala para atirar 
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    public override void FindTarget()//Procura o primeiro inimigo que aparece e define como alvo
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()//Verifica se o alvo est� dentro do alcance da torre.
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, turretRotationPoint.position) <= targetinRange;
    }

    private void RotateTowardsTarget()//Rotaciona a torre em dire��o ao alvo.
    {

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime );
     
    }
    private void OnDrawGizmosSelected()//Desenha um c�rculo no editor para mostrar a �rea de ataque da torre.
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetinRange);
    }


}


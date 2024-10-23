using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turrets : MonoBehaviour
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

    private void Update()
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
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, turretRotationPoint.position) <= targetinRange;
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - turretRotationPoint.position;

      
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

 
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Transform cannon = turretRotationPoint.GetChild(0); 

    
        cannon.localRotation = Quaternion.RotateTowards(cannon.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetinRange);
    }


}


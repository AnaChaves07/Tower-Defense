using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Turrets2 : BaseTurret
{ 
[Header("References")]
    [SerializeField] private UnityEngine.Transform turretRotationPoint;
[SerializeField] private LayerMask enemyMask;
[SerializeField] private GameObject bulletPrefab;
[SerializeField] private UnityEngine.Transform firingPoint;

[Header("Attribute")]
[SerializeField] private float targetinRange = 5f;
[SerializeField] private float rotationSpeed = 5f;
[SerializeField] private float bps = 2f;

private UnityEngine.Transform target;
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
    if (timeUntilFire >= 1f / bps)
    {
        Shoot();
        timeUntilFire = 0f;
    }
}
public override void Shoot()
{
    GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
    Bullet bulletScript = bulletObj.GetComponent<Bullet>();
    bulletScript.SetTarget(target);
}
public override void FindTarget()
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
    float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);

}
private void OnDrawGizmosSelected()
{
    Handles.color = Color.cyan;
    Handles.DrawWireDisc(transform.position, transform.forward, targetinRange);
}


}

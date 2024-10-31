using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Turrets2 : BaseTurret//Segunda torreta que atira duas vezes nos inimigos, com herança 
{ 
[Header("References")]
    [SerializeField] private UnityEngine.Transform turretRotationPoint;// Ponto que rotaciona a torre para mirar.
    [SerializeField] private LayerMask enemyMask;// Máscara que identifica os inimigos.
    [SerializeField] private GameObject bulletPrefab;// O prefab da bala que a torre dispara.
    [SerializeField] private UnityEngine.Transform firingPoint;// O local onde a bala é disparada.

    [Header("Attribute")]
[SerializeField] private float targetinRange = 5f;// Distância em que a torre pode atacar inimigos.
    [SerializeField] private float rotationSpeed = 5f;// Velocidade de rotação da torre.
    [SerializeField] private float bps = 2f;// Número de disparos por segundo.

    private UnityEngine.Transform target;// O inimigo que a torre está mirando.
    private float timeUntilFire;// Controle do tempo até o próximo disparo.
   // private float timeUntilNextShot;

private void Update()//Verifica se existe algum alvo e se tiver chama os outros métodos
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
public override void Shoot()//Usa o prefab da bala e define seu alvo e atira
    {
    GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
    Bullet bulletScript = bulletObj.GetComponent<Bullet>();
    bulletScript.SetTarget(target);
}
public override void FindTarget()//Procura inimigos ao redor e define o primeiro encontrado como alvo.
    {
    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2)transform.position, 0f, enemyMask);
    if (hits.Length > 0)
    {
        target = hits[0].transform;
    }
}

private bool CheckTargetIsInRange()//Verifica se o alvo está dentro do alcance da torre.
    {
    if (target == null) return false;
    return Vector2.Distance(target.position, turretRotationPoint.position) <= targetinRange;
}
private void RotateTowardsTarget()//Rotaciona a torre em direção ao alvo.
    {
    float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);

}

}

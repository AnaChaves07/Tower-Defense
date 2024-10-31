using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public class TurretSlomo : BaseTurret //Classe para a torreta de gelo que desacelera os inimigos, com herança 
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetinRange = 5f;
    [SerializeField] private float aps = 4f;
    [SerializeField] private float freezeTime = 1f;

    private Transform target;
    private float timeUntilFire;
    private void Update()//Checa se é hora de desacelerar inimigos. 
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }
     private void FreezeEnemies()//Congela os inimigos por um tempo e devolve sua velocidade depois
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Enemy em = hit.transform.GetComponent < Enemy> ();
                em.UpdateSpeed(0.5f);
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(Enemy em)//Espera um tempo e então restaura a velocidade do inimigo.
    {
        yield return new WaitForSeconds(freezeTime);
        em.ResetSpeed();
    }
    public override void Shoot()//Usa o prefab da bala para atirar 
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        FreezeEnemies ();
    }
    public override void FindTarget()//Procura por inimigos e define o primeiro encontrado como alvo.
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetinRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }


}

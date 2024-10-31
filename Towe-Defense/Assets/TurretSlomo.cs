using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlomo : BaseTurret //Classe para a torreta de gelo que desacelera os inimigos, com herança 
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;// Máscara que determina quais objetos são considerados inimigos.
    [SerializeField] private GameObject bulletPrefab;// Prefab da bala que será disparada pela torre.
    [SerializeField] private Transform firingPoint;// Ponto de onde a bala será disparada.

    [Header("Attribute")]
    [SerializeField] private float targetinRange = 5f;// Alcance em que a torre pode afetar inimigos.
    [SerializeField] private float aps = 4f;// Taxa de disparo (ataques por segundo).
    [SerializeField] private float freezeTime = 1f;// Tempo que os inimigos ficarão desacelerados.

    private Transform target;// O alvo atual que a torre está visando.
    private float timeUntilFire; // Temporizador para controlar a taxa de disparo.
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

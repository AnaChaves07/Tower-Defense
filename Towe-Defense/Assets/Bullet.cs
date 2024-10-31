using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour//Move e direciona a bala para atirar no inimigo
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;// Referência ao componente Rigidbody2D da bala.

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;// Velocidade da bala.
    [SerializeField] private int bulletDamage = 1;// Quantidade de dano que a bala causa.
    private Transform target;// O alvo que a bala está perseguindo.

    public void SetTarget(Transform _target)//Permite que a bala saiba qual alvo ela deve seguir.
    {
        target = _target;
    }

    private void FixedUpdate()//Atualiza e calcula a direção do alvo para depois atirar
    {
        if(!target) return; 
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other)//Checa se existe uma colisão da bala com o inimigo, se existe ele destroy a bala 
    {
      
      other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
      Destroy(gameObject);
    }
}

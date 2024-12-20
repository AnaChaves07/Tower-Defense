using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour//Define as informa��es gerais do inimigo
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;// Refer�ncia ao componente Rigidbody2D do inimigo.

    [Header("Attributes")]
    [SerializeField] private float speed = 2f;// Velocidade com que o inimigo se move.

    private Transform target;// O pr�ximo ponto do caminho que o inimigo deve seguir.
    private int pathIndex = 0;// �ndice que indica a posi��o atual do inimigo no caminho.
    private float baseSpeed;// Velocidade base do inimigo, usada para reset.

    private void Start()//Salva a velocidade do inimigo
    {
        baseSpeed = speed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()// Atualiza o indice da movimenta��o do inimigo de acordo com os pontos indicados na cena 
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f) 
        {
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length)
            {
                Spawn.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()//Calcula a dire��o dos pontos e move o inimigo de acordo 
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity= direction * speed;
    }

    public void UpdateSpeed(float newSpeed)//Permite que a velocidade seja alterada
    {
     speed = newSpeed;
    }

    public void ResetSpeed()//Restaura a velocidade para a normal
    {
        speed = baseSpeed;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour //Controla a vida do inimigo 
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;// Pontos de vida do inimigo.
    [SerializeField] private int currencyWorth = 50; // Quantidade de moeda que o inimigo gera ao ser destruído.


    public void TakeDamage(int dmg)//Controla o dano tomado pelo inimigo e depois destroi ele
    {
        if (dmg < 0) return; 
        hitPoints -= dmg;

        if(hitPoints <= 0)
        {
            Spawn.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
      
            Destroy(gameObject);
        }
    }
    
}

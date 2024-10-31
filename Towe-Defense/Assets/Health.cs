using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
   

    public void TakeDamage(int dmg)
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

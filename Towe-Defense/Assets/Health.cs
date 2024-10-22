using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; 

    public void TakeDamage(int dmg)
    {
        if (dmg < 0) return; 
        hitPoints -= dmg;

        if(hitPoints <= 0)
        {
            Spawn.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

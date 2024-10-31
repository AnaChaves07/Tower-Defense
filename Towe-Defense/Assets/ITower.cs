using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower //Interface que define os métodos que as torres devem ter.
{
    void Shoot();       // Método para disparar
    void FindTarget();  // Método para encontrar alvos
}

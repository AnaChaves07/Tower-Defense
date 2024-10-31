using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower //Interface que define os m�todos que as torres devem ter.
{
    void Shoot();       // M�todo para disparar
    void FindTarget();  // M�todo para encontrar alvos
}

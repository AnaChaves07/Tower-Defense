using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour, ITower //Classe pra heran�a 
{
    public abstract void Shoot();// M�todos a serem implementados pelas subclasses
    public abstract void FindTarget();// M�todos a serem implementados pelas subclasses
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour, ITower //Classe pra herança 
{
    public abstract void Shoot();// Métodos a serem implementados pelas subclasses
    public abstract void FindTarget();// Métodos a serem implementados pelas subclasses
}
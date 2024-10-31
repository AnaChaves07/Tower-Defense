//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Tower //Representa a torreta que pode ser contruidas no jogo 
{
    public string name;// Nome da torre.
    public int cost;// Custo para construir a torre.
    public GameObject prefab;// Prefab que representa a torre no jogo.

    public Tower(string _name, int _cost, GameObject _prefab)//cria uma novo instancia da classe e recebe três parâmetros 
    {
        name = _name;
        cost = _cost;   
        prefab = _prefab;
    }
    
}

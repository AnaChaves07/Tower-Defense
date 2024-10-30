using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour //Gerencia a quantidade de moedas de acordo com os inimigos acertados 
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        main = this;    
    }

    private void Start()//Define a quantidade inicial de moeda.
    {
        currency = 300;
    }

    public void IncreaseCurrency(int amount)//Adiciona uma quantidade de moeda.
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)// Controla o gasto da moeda
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;

        }
        else
        {
            return false; 
        }
    }
}

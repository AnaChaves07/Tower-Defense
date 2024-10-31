using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour// Controla a seleção das torres que podem ser construídas no jogo.
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers; // Lista de torres disponíveis para construção.

    private int SelectedTower = 0;// Índice da torre atualmente selecionada.

    private void Awake()//Define a instancia da classe
    {
        main = this;
    }

    public Tower GetSelectedTower()//Retorna quando torre que está  selecionada
    {
        return towers[SelectedTower];
    }

    public void SetSelectTower(int _SelectedTower)//Permite que o espaço selecionado fique ativo para colocar a torre 
    {
        SelectedTower = _SelectedTower;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour// Controla a sele��o das torres que podem ser constru�das no jogo.
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers; // Lista de torres dispon�veis para constru��o.

    private int SelectedTower = 0;// �ndice da torre atualmente selecionada.

    private void Awake()//Define a instancia da classe
    {
        main = this;
    }

    public Tower GetSelectedTower()//Retorna quando torre que est�  selecionada
    {
        return towers[SelectedTower];
    }

    public void SetSelectTower(int _SelectedTower)//Permite que o espa�o selecionado fique ativo para colocar a torre 
    {
        SelectedTower = _SelectedTower;
    }

}

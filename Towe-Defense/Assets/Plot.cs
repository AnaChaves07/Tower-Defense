using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour//Controla os quadrados onde pode ser construido as torretas
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    void Start()//Armazena a cor original do quadrado 
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()//Quando o mouse estiver em cima do quadrado, ele muda de cor
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()//Ao tirar o mouse, o quadrado volta com a cor original 
    {
        sr.color = startColor;
    }
    private void OnMouseDown()//Ao clicar com o mouse a torre é construida no espaço do quadrado
    {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower(); 
        
        if(towerToBuild.cost > LevelManager.main.currency)
        {
            return; 
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost  );  
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
   
}

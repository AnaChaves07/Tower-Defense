using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers; 

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    public void SetSelectTower(int _SelectedTower)
    {
        SelectedTower = _SelectedTower;
    }

}

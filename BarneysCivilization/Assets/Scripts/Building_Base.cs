﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Base : MonoBehaviour
{
    public HexCell Cell;
    public CardManager Owner;
    public bool IsPermenant;
    public int Turns;

    public virtual void OnCreated(HexCell cell,CardManager owner)
    {
        Cell = cell;
        Owner = owner;
        cell.OwnerChangeEvent += OnCellChangeOwner;
        cell.PlacedBuilding = this;
        InGameManager.instance.GameStateChangeEvent += OnGameStateChange;
    }

    protected virtual void OnGameStateChange()
    {
        if (InGameManager.isGameState(GameStateType.Decision))
        {
            if (!IsPermenant)
            {
                Turns--;
                if (Turns <= 0)
                {
                    OnBuildingDestroy();
                    Destroy(gameObject);
                }
            }     
        }
    }

    public virtual void OnBuildingDestroy()
    {
        Cell.OwnerChangeEvent -= OnCellChangeOwner; 
        InGameManager.instance.GameStateChangeEvent -= OnGameStateChange;
        Cell.PlacedBuilding = null;
    }
    protected virtual void OnCellChangeOwner(CardManager newOwner)
    {
        if (Cell.OwnerManager != Owner)
        {
            OnBuildingDestroy();                                
            Owner.BuildingDestroy(this);
            Destroy(gameObject);
        }
    }
}

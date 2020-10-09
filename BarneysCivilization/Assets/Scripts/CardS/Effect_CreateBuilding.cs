﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_CreateBuilding : CardEffect
{
    public bool SupportAllCellTypes = true;
    public List<HexCellType> SupportCellTypes = new List<HexCellType>();
    public int RequireHealth;
    public int DecreaseHealth;
    public GameObject BuildingPrefab;

    public override bool CanUseCard(CardManager user, HexCell cell)
    {
        bool isSupportCellType = SupportAllCellTypes;
        if (!SupportAllCellTypes)
        {
            if (cell)
            {
                isSupportCellType = SupportCellTypes.Contains(cell.CellType);
            }
            else
            {
                isSupportCellType = false;
            }
        }
        return base.CanUseCard(user, cell) && isSupportCellType;
    }
    public override UseCardFailReason GetFailReason(CardManager user, HexCell cell)
    {
        bool isSupportCellType = SupportAllCellTypes;
        if (!SupportAllCellTypes)
        {
            if (cell)
            {
                isSupportCellType = SupportCellTypes.Contains(cell.CellType);
            }
            else
            {
                isSupportCellType = false;
            }
        }
        if (!isSupportCellType)
        {
            return UseCardFailReason.NotValidCellType;
        }
        return base.GetFailReason(user, cell);
    }
    public override List<HexCell> GetCanUseCells(CardManager user)
    {
        List<HexCell> cells = new List<HexCell>();
        foreach (HexCell cell in user.OccupiedCells)
        {
            bool isSupportCellType = SupportAllCellTypes ? true : SupportCellTypes.Contains(cell.CellType);
            if (cell.GetUnitOnCell() && cell.GetUnitOnCell().Health >= RequireHealth && (cell.PlacedBuilding == null || cell.PlacedBuilding.CanUpgrade(user)) && isSupportCellType)
            {
                cells.Add(cell);
            }
        }
        return cells;
    }

    public override void Effect(CardManager user, HexCell cell)
    {
        base.Effect(user, cell);
        if (DecreaseHealth != 0 && cell.GetUnitOnCell())
        {
            cell.GetUnitOnCell().ChangeHealth(-DecreaseHealth,Vector3.zero);
        }
        if (cell.PlacedBuilding == null)
        {
            user.CreateBuilding(BuildingPrefab, cell);
        }
        else
        {
            cell.PlacedBuilding.UpgradeBuilding(cell, user);
        }
    }
}

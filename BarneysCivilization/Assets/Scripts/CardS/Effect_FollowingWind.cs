﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_FollowingWind : CardEffect
{
    public int Turns = 1;
    public int ActionPointAddAmount = 1;
    public GameObject BuffPrefab;

    public override bool CanUseCard(CardManager user, HexCell cell)
    {
        return base.CanUseCard(user, cell);
    }

    public override List<HexCell> GetCanUseCells(CardManager user)
    {
        return base.GetCanUseCells(user);
    }

    public override void Effect(CardManager user, HexCell cell)
    {
        GameObject g = Instantiate(BuffPrefab, cell.transform.position, Quaternion.identity);
        CellBuff_Base buff = g.GetComponent<CellBuff_Base>();
        buff.Turns = Turns;
        buff.OnCreated(cell, user);

        user.ActionPoint += ActionPointAddAmount;
    }
}
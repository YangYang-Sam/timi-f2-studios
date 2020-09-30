﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_HotWater : CardEffect
{
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
        user.ActionPoint += 1;
        user.UnitMoveSpeed += 1;
    }
}

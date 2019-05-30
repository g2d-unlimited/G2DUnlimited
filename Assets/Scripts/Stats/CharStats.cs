using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharStats : MonoBehaviour
{
    public BarScript bar;

    public float baseHP = 200;
    public float currHP = 100;
    public float baseARM = 100;
    public float currARM = 100;

    public string theName;
    public float baseATK;
    public float currATK;
    public float baseDEF;
    public float currDEF;

    public List<BaseAttack> attackList = new List<BaseAttack>();

    public float CurrentValueHP
    {
        get
        {
            return currHP;
        }

        set
        {
            this.currHP = Mathf.Clamp(value, 0, MaxValueHP);
            bar.Value = currHP;
        }
    }

    public float MaxValueHP
    {
        get
        {
            return baseHP;
        }

        set
        {
            this.baseHP = value;
            bar.MaxValue = baseHP;
        }
    }
    public float CurrentValueArmor
    {
        get
        {
            return currARM;
        }

        set
        {
            this.currARM = Mathf.Clamp(value, 0, MaxValueArmor);
            bar.Value = currARM;
        }
    }

    public float MaxValueArmor
    {
        get
        {
            return baseARM;
        }

        set
        {
            this.baseARM = value;
            bar.MaxValue = baseARM;
        }
    }

    public void Initialize()
    {
        this.MaxValueHP = baseHP;
        this.CurrentValueHP = currHP;
        this.MaxValueArmor = baseARM;
        this.CurrentValueArmor = currARM;
    }
}

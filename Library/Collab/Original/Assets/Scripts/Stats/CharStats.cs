using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharStats : MonoBehaviour
{
    //[SerializeField]
    public BarScript bar;

    //[SerializeField]
    public float maxValueHP = 100;

    [SerializeField]
    public float currentValueHP;

    public float maxValueArmor = 100;

    public float currentValueArmor = 100;

    public float CurrentValueHP
    {
        get
        {
            return currentValueHP;
        }

        set
        {
            this.currentValueHP = Mathf.Clamp(value, 0, MaxValueHP);
            bar.Value = currentValueHP;
        }
    }

    public float MaxValueHP
    {
        get
        {
            return maxValueHP;
        }

        set
        {
            this.maxValueHP = value;
            bar.MaxValue = maxValueHP;
        }
    }
    public float CurrentValueArmor
    {
        get
        {
            return currentValueArmor;
        }

        set
        {
            this.currentValueArmor = Mathf.Clamp(value, 0, MaxValueArmor);
            bar.Value = currentValueArmor;
        }
    }

    public float MaxValueArmor
    {
        get
        {
            return maxValueArmor;
        }

        set
        {
            this.maxValueArmor = value;
            bar.MaxValue = maxValueArmor;
        }
    }

    public void Initialize()
    {
        this.MaxValueHP = maxValueHP;
        this.CurrentValueHP = currentValueHP;
        this.MaxValueArmor = maxValueArmor;
        this.CurrentValueArmor = currentValueArmor;
    }
}

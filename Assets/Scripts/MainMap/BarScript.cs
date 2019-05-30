﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    public float fillAmount;

    [SerializeField]
    public Image content;

    [SerializeField]
    public Text valueText;

    [SerializeField]
    public float lerpSpeed;

    public CharStats m;
    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, m.baseHP, 0, 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    /// <summary>
    /// Value - kiek reikia atvaizduoti.
    /// inMin - mažiausia hp vertė --> 0
    /// inMax - didžiausia hp vertė --> priklausys nuo pickintų item'ų
    /// outMin, outMax - fill amounts.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="inMin"></param>
    /// <param name="inMax"></param>
    /// <returns></returns>
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        float bb = (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        Debug.Log(bb);
        return bb;
    }
}

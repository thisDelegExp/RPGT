using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Stat : MonoBehaviour
{

    private Image content;
    [SerializeField]
    private Text statValue;
    [SerializeField]
    private float lerpSpeed;
    public float MaxValue { get; set; }
    private float currentFill;
    private float currentValue;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MaxValue)
                currentValue = MaxValue;
            else
            {
                if (value < 0)
                    currentValue = 0;
                else
                    currentValue = value;
            }
            
            currentFill = CurrentValue / MaxValue;
            if(statValue!=null)
                statValue.text =string.Format("{0}/{1}", CurrentValue, MaxValue);
        }
    }

    void Start()
    {
        content=GetComponent<Image>();
        
    }


    void Update()
    {
        //Debug.Log(CurrentValue);
        if (currentFill != content.fillAmount)
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
    }

    public void Initialize(float currentValue, float maxValue)
    {
        if (content == null)
            content = GetComponent<Image>();
        MaxValue = maxValue;
        CurrentValue = currentValue; ;
        content.fillAmount = CurrentValue / MaxValue;
    }
}

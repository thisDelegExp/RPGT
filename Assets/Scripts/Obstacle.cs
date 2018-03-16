using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour, IComparable<Obstacle>
{
    public SpriteRenderer GetSpriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    private Color defaultColor;
    private Color fadedColor;

    // Use this for initialization
    void Start ()
    {
        
        defaultColor = GetSpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CompareTo(Obstacle other)
    {
        if (GetSpriteRenderer.sortingOrder > other.GetSpriteRenderer.sortingOrder)
            return 1;
        else if (GetSpriteRenderer.sortingOrder < other.GetSpriteRenderer.sortingOrder)
            return -1;
            else
                return 0;
    }

    public void FadeOut()
    {
        GetSpriteRenderer.color = fadedColor;
    }

    public void FadeIn()
    {
        GetSpriteRenderer.color = defaultColor;
    }
}

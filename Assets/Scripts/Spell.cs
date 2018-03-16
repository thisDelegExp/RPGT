using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spell
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int damage;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float castTime;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Color barColor;
    

    public string GetName{get{return name; }}
    public int GetDamage{get{return damage;}}
    public Sprite GetIcon{get {return icon;}}
    public float GetSpeed{get { return speed; }}       
    public float GetCastTime{get{return castTime;}}
    public GameObject GetPrefab{get{ return prefab; }}
    public Color GetBarColor{get{ return barColor;}}
}   
   
        
      
        
            
       

       

   
        
            
       

        
   

    

        
            
       
       
       
        
            
        

   

   

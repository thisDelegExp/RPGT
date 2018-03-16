﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private NPC currentTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ClickTarget();
	}

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,512);
            if (hit.collider != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.Deselect();
                    
                }
                currentTarget = hit.collider.GetComponent<NPC>();

                player.Target = currentTarget.Select();
                UIManager.Instance.ShowTargetFrame(currentTarget);
               
                //if(hit.collider.tag=="Enemy")
                //    player.Target = hit.transform.GetChild(0);
            }
            else//deselect target
                {
                UIManager.Instance.HideTargetFrame();
                    if (currentTarget != null)
                        currentTarget.Deselect();
                    currentTarget = null;
                    player.Target = null;
                }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    [SerializeField]
    private Stat stamina;
   
    [SerializeField]
    private float initialStaminaValue;
    [SerializeField]
    private float maxStaminaValue;
    
    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex = 2;
    public Transform Target { get; set; }
    [SerializeField]
    private Block[] blocks;
    private SpellBook spellBook;
    private Vector3 min, max;
    protected override void Start()
    {
        base.Start();
        
        stamina.Initialize(initialStaminaValue, maxStaminaValue);
        spellBook = GetComponent<SpellBook>();
        //target = GameObject.Find("Target").transform; //DEBUG ONLY
    }
    protected override void Update ()
    {
        GetInput(); //updating player direction 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
        base.Update();
        //Debug.Log(LayerMask.GetMask("Block"));
        
        
    }
    private void GetInput()
    {
        //DEBUG ONLY
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.CurrentValue -= 10;
            stamina.CurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            health.CurrentValue += 10;
            stamina.CurrentValue += 10;
        }
        //DEBUG ONLY


        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }

        
    }

    public void SetLimits(Vector3 min,Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private IEnumerator Attack(int spellIndex)
    {
            Transform currentTarget = Target;
            Spell s = spellBook.PrepareSpell(spellIndex);
            IsAttacking = true;
            animator.SetBool("cast", IsAttacking);
        
            yield return new WaitForSeconds(s.GetCastTime); //attack delay

        if (Target != null && InLineOfSight())
        {
            SpellController spell = Instantiate(s.GetPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellController>();
            spell.Initialize(Target, s.GetDamage);
        }
            StopAttack();
        
        
    }

    public void CastSpell(int spellIndex)
    {
        
        Block();
        if (Target != null && !IsAttacking && !IsMoving && InLineOfSight())
        {
            castCoroutine = StartCoroutine(Attack(spellIndex));
            Debug.Log("attacked");
        }
    }
	
	private bool InLineOfSight()
    {
        if (Target != null)
        {
            Vector3 targetDirection = (Target.transform.position - transform.position).normalized;
            //Debug.DrawRay(transform.position, targetDirection,Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, Target.transform.position), 256);
            if (hit.collider == null)
            {
                return true;
            }
        }
        return false;
    }

    private void Block()
    {
        foreach (Block item in blocks)
        {
            item.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

    public override void StopAttack()
    {
        spellBook.StopCasting();
        base.StopAttack();

    }





}

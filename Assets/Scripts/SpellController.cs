using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpellController : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    [SerializeField]
    private float speed;

    private float damage;
    
    public Transform MyTarget { get; private set; }
                                       // Use this for initialization
    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //target = GameObject.Find("Target").transform; //DEBUG ONLY
	}

    public void Initialize(Transform target, int damage)
    {
        this.MyTarget = target;
        this.damage = damage;
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        if (MyTarget!=null)
        {
            Vector2 direction = MyTarget.position - transform.position;
            myRigidbody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
	}

    public void Fire(Transform target)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox"&&collision.transform==MyTarget)
        {
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(damage);
            GetComponent<Animator>().SetTrigger("Impact");
            myRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}

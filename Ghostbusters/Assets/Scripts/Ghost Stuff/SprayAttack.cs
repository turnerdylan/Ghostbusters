using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayAttack : MonoBehaviour
{
    public GameObject ghostBall;
    private Animator anim;
    [SerializeField] private float attackTimer;
    [SerializeField] private float force;
    [SerializeField] private int numProjectiles;
    [SerializeField] private float sprayAngle;
    [SerializeField] private float projectileLifetime;
    private float _attackTimer;
    private bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _attackTimer = attackTimer;
    }

    void Update()
    {
       _attackTimer -= Time.deltaTime;
        if(_attackTimer < 0 && !attacking)
        {
            Debug.Log(transform.forward);
            attacking = true;
            StartCoroutine(GetComponent<LegGhostMovement>().State_Attack());
            StartCoroutine(EndAttack());
        }
    }    
    
    public IEnumerator EndAttack()
    {
        //yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        Spray();
        yield return new WaitForSeconds(1.0f);
        _attackTimer = attackTimer;
        attacking = false;
        StartCoroutine(GetComponent<LegGhostMovement>().State_Follow());
    }

    void Spray()
    {
        for(int i = 0; i<numProjectiles; i++)
        {
            float angle = (i*(sprayAngle/(numProjectiles-1))) - (sprayAngle/2);
            GameObject go = Instantiate(ghostBall, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            if(transform.forward.x < 0)
                angle -= Vector3.Angle(transform.forward, new Vector3(0,0,1));
            else
                angle += Vector3.Angle(transform.forward, new Vector3(0,0,1));
            AddForceAtAngle(go.GetComponent<Rigidbody>(), force, angle);
            Destroy(go, projectileLifetime);
        }
    }
    public void AddForceAtAngle(Rigidbody rb, float force, float angle)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180);
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180);
        Vector3 dir = new Vector3(ycomponent, 0, xcomponent);
        rb.AddForce(dir*force);
    }
}

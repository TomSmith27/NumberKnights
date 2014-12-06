using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    public Transform targetTransform;
    public int MoveSpeed;
    private bool _onAttackCoolDown;
    private Animator anim;
    public int Health
    {
        get
        {
            return healthBar.Health;
        }
        set
        {
            healthBar.Health = value;
        }
    }
    private CastleController enemyCastle;
    HealthBarRenderer healthBar;
	// Use this for initialization
	void Start () {
        healthBar = gameObject.GetComponent<HealthBarRenderer>();
        anim = gameObject.GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("No Animator Attached to " + this.gameObject.name);

        CastleController[] castles = GameObject.FindObjectsOfType<CastleController>();
        foreach (CastleController castle in castles)
        {
            if (castle.tag != this.tag)
            {
                targetTransform = castle.transform;
                enemyCastle = castle;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Health < 1)
            Destroy(this.gameObject);
        if (Vector3.Distance(this.transform.position, targetTransform.position) > 3)
        {
            float step = MoveSpeed * Time.deltaTime;
            Vector3 moveLocation = Vector3.MoveTowards(this.transform.position, targetTransform.position, step);
            moveLocation.y = this.transform.position.y;
            this.transform.position = moveLocation;
        }
        else if(!_onAttackCoolDown)
        {
            _onAttackCoolDown = true;
            StartCoroutine(AttackCastle());
        }
	}
    public IEnumerator AttackCastle()
    {
            enemyCastle.Health--;
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(5f);
            _onAttackCoolDown = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != this.gameObject.tag)
        {
            anim.SetTrigger("Attack");
            coll.gameObject.GetComponent<MinionController>().Health--;
        }
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MinionController : Controller {

    public Transform targetTransform;
    public int MoveSpeed;
    public float AttackSpeed;
    private bool _onAttackCoolDown;
    private Animator anim;
    public List<Controller> currentEnemies = new List<Controller>();
    private CastleController enemyCastle;

	// Use this for initialization
	void Start () {
        healthBar = gameObject.GetComponentInChildren<HealthBarRenderer>();
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
        while (currentEnemies.Count > 0 && currentEnemies[0] == null )
        {
                currentEnemies.RemoveAt(0);
        }
        Fighting = currentEnemies.Count > 0;
        if (Fighting)
        {
            if (!_onAttackCoolDown)
            {
                _onAttackCoolDown = true;
                StartCoroutine(Attack(currentEnemies[0]));
            }
        }
        else if (Vector3.Distance(this.transform.position, targetTransform.position) > 3)
        {
            float step = MoveSpeed * Time.deltaTime;
            Vector3 moveLocation = Vector3.MoveTowards(this.transform.position, targetTransform.position, step);
            moveLocation.y = this.transform.position.y;
            this.transform.position = moveLocation;
        }
	}
    public IEnumerator Attack(Controller controller)
    {
            controller.Health--;
            Debug.Log("Attack");
            if (currentEnemies[0].Health == 0)
            {
                currentEnemies.RemoveAt(0);
            }
            if (currentEnemies.Count == 0)
                Fighting = false;
            anim.SetTrigger("Attack");
            
            yield return new WaitForSeconds(AttackSpeed);
            _onAttackCoolDown = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != this.gameObject.tag)
        {
            Controller c = coll.GetComponent<Controller>();
            currentEnemies.Add(c);
            if (!_onAttackCoolDown)
            {
                Fighting = true;
                _onAttackCoolDown = true;
                StartCoroutine(Attack(c));
            }
        }        
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        Debug.Log("Colliding");
    }
    


    public bool Fighting { get; set; }
}

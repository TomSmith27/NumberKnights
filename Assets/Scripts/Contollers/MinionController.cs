using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    public Transform targetTransform;
    public int MoveSpeed;
    public int Health = 1;
    private CastleController enemyCastle;
	// Use this for initialization
	void Start () {
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
        else
        {
            enemyCastle.Health--;
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != this.gameObject.tag)
        {
            coll.gameObject.GetComponent<MinionController>().Health--;
        }
    }

}

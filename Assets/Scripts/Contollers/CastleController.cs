using UnityEngine;
using System.Collections;

public class CastleController : MonoBehaviour {

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
    HealthBarRenderer healthBar;
	// Use this for initialization
	void Start () {
        healthBar = gameObject.GetComponent<HealthBarRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Health < 1)
            Debug.Log(this.gameObject.tag.ToString() + " Lost the game");
	}
}

using UnityEngine;
using System.Collections;


public class CastleController : Controller {

	// Use this for initialization
	void Start () {
       healthBar = gameObject.GetComponentInChildren<HealthBarRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Health < 1)
            Debug.Log(this.gameObject.tag.ToString() + " Lost the game");
	}
}

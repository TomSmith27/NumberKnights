using UnityEngine;
using System.Collections;

public class CastleController : MonoBehaviour {


    public int Health = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Health < 1)
            Debug.Log(this.gameObject.tag.ToString() + " Lost the game");
	}
}

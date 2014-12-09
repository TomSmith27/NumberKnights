using UnityEngine;
using System.Collections;

public class HealthBarAttacher : MonoBehaviour {

    public GameObject healthBar;
    private Sprite _sprite;
    public float XScale, YScale, Offset, MaxHealth;
	// Use this for initialization
	void Start () {
        _sprite = this.GetComponent<SpriteRenderer>().sprite;
        float pixel2units = (_sprite.rect.width) / _sprite.bounds.size.x;
        Vector3 p = this.transform.position;
        p.y = p.y + (_sprite.rect.height / 2 + Offset) / pixel2units;
        GameObject g = Instantiate(healthBar , p, Quaternion.identity) as GameObject;
        HealthBarRenderer r = g.GetComponent<HealthBarRenderer>();
        r.XScale = XScale;
        r.YScale = YScale;
        r.MaxHealth = (int)MaxHealth;
        g.transform.parent = this.transform;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

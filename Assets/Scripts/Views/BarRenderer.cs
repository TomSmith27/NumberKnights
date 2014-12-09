using UnityEngine;
using System.Collections;

public class BarRenderer : MonoBehaviour
{
    protected Sprite _sprite;
    protected Transform ParentPos;
    public float XScale { get; set; }
    public float YScale { get; set; }
    
    protected void Start()
    {
        this.transform.localScale = new Vector3(XScale, YScale, 1);
        _sprite = this.GetComponent<SpriteRenderer>().sprite;
        ParentPos = GetComponentInParent<Transform>();
        Vector3 p = this.transform.position;

        float pixel2units = (_sprite.rect.width / XScale) / _sprite.bounds.size.x;
        p.x = p.x - (_sprite.rect.width / 2) / pixel2units;
        p.y = p.y + (_sprite.rect.height / 2) / pixel2units;
        this.transform.position = p;

    }
    protected void Update()
    {
       
    }

}

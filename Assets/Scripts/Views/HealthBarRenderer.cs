using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class HealthBarRenderer : BarRenderer
{

    public int MaxHealth;
    public int health;
    protected GUIStyle HealthBarStyle;
    protected SpriteRenderer childRenderer;
    // Use this for initialization
    void Start()
    {
        base.Start();
        health = MaxHealth;
        //Crazy Linq that makes sure the sprite we get is not us
        var child = from c in this.GetComponentsInChildren<SpriteRenderer>()
                        where (c.transform != this.transform)
                        select c;
        childRenderer = (SpriteRenderer)child.First();

    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    Color HealthColour()
    {
        if (health > MaxHealth * 0.7f)
            return Color.green;
        else if (health > MaxHealth * 0.4f)
            return Color.yellow;
        else
            return Color.red;
    }
    // Update is called once per frame
    void Update()
    {
        float healthPercent = ((float)health / (float)MaxHealth);
        childRenderer.transform.localScale = new Vector3( healthPercent, childRenderer.transform.localScale.y, 1);
        childRenderer.color = HealthColour();
        base.Update();
    }
}

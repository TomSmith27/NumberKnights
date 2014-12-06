using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HealthBarRenderer : BarRenderer
{

    public int MaxHealth;
    public int health;
    protected GUIStyle HealthBarStyle;
    // Use this for initialization
    void Start()
    {
        health = MaxHealth;
        //HealthBarStyle = customSkin.customStyles [3];
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    void OnGUI()
    {
        base.OnGUI();
        GUI.backgroundColor = HealthColour();
        GUI.Button(new Rect(pos.x, pos.y, BAR_WIDTH * ((float)health/MaxHealth), BAR_HEIGHT), string.Empty);
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

    }
}

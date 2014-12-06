using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BarRenderer : MonoBehaviour
{

    public float BAR_WIDTH = 40;
    public float BAR_HEIGHT = 10;
    protected Sprite sprite;
    protected Vector2 pos;
    protected const int padding = 2;
    protected const int offset = 5;

    
    void Start()
    {
        sprite = GetAttachedSprite();
    }
    public void OnGUI()
    {
        if (sprite == null)
            sprite = GetAttachedSprite();

        pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.x = pos.x - BAR_WIDTH / 2;
        
        pos.y = (Screen.height - pos.y) - BAR_HEIGHT - offset - (sprite.rect.height/2);
        GUI.Box(new Rect(pos.x - padding/2, pos.y - padding/2, BAR_WIDTH + padding, BAR_HEIGHT + padding), string.Empty);

    }
    public Sprite GetAttachedSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}

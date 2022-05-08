using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public SpriteAtlas[] atlases;
    private SpriteAtlas atlas;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        atlas = atlases[GetComponent<PlayerDetails>().playerID - 1];
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        string spriteName = spriteRenderer.sprite.name;
        spriteRenderer.sprite = atlas.GetSprite(spriteName);
    }
}

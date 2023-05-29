using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponRenderer : MonoBehaviour
{
    // should be on our gun sprite
    /// <summary>
    /// So this is the variable to see what our player's sort order is
    /// In order to get the illusion of the gun being infront of the player when it's pointing down we'll put it on a layer higher than the player
    /// But if it's pointed upwards let it be behind them. That way it doesn't cover the player's face either.
    /// </summary>
    [SerializeField]
    protected int dino_MCSortOrder = 0;
    protected SpriteRenderer weaponRenderer;

    private void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipWeaponSprite(bool _val)
    {   // would need to account for the point where bullets are shot from.
        // the transform for that would not be at correct point. Had similar issue in the other project I'd done.
        int flipWeaponModifier = _val ? -1 : 1;
        transform.localScale = new Vector3(transform.localScale.x, flipWeaponModifier * Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }

    public void RenderBehindPlayer(bool val)
    {
        if (val)
        {
            weaponRenderer.sortingOrder = dino_MCSortOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = dino_MCSortOrder + 1;
        }
    }
}

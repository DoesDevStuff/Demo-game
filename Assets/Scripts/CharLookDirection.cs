using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class CharLookDirection : MonoBehaviour
{
    protected SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("Cross product to Right" + Vector3.Cross(Vector2.up, Vector2.right));
        Debug.Log("Cross product to Left" + Vector3.Cross(Vector2.up, -Vector2.right));

    }

    public void FaceDirection(Vector2 mouseinput)
    {
        // this is going to be on the component attached to our player. I won't need to get the reference to the player this way
        // 
        var pdirection = (Vector3)mouseinput - transform.position;
        var resultantDirection = Vector3.Cross(Vector2.up, pdirection); // calculates for z, -z = going right, +z = going left

        if (resultantDirection.z > 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (resultantDirection.z < 0)
        {
            playerSpriteRenderer.flipX = false;
        }
    }
}

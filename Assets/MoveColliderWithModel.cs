using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves n colliders with a child model if model is being moved by an animator.
/// </summary>
public class MoveColliderWithModel : MonoBehaviour
{
    public GameObject model;
    public BoxCollider2D[] boxColliders;
    private Vector3 position;
    

    // Update is called once per frame
    void Update()
    {
        if (model == null || boxColliders.Length == 0) return;
        position = model.transform.localPosition;
        foreach (BoxCollider2D col in boxColliders)
        {
            if (col == null) break;
            col.offset = new Vector2(position.x, position.y);    
        }
        
    }
}

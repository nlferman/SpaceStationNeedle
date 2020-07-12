using UnityEngine;
using System;
using System.Collections;
using MoreMountains.TopDownEngine;
using MoreMountains.Feedbacks;

public class ForceFieldAbility : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    public MMFeedbacks forceFieldHitFeedbacks;
    public bool abilityEnabled = true;
    public void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        //circleCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("Something entered" + col.transform.name);
        if (!abilityEnabled) return;
        if (col.transform.GetComponent<Projectile>() != null && col.tag != "Player")
        {
            print("projectile?");
            GameObject projectile = col.gameObject;
            projectile.gameObject.SetActive(false);
            forceFieldHitFeedbacks?.PlayFeedbacks(this.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    #region Singleton

    public static DamageController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool isDead(Stats stats)
    {
        if (stats.hitPoints <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void DamageObject(int damage, Stats stats)
    {
        stats.hitPoints -= damage;

        //add tag identifier to if statements

        ZombieAnimator zombieAnimator = stats.transform.gameObject.GetComponent<ZombieAnimator>();
        int rng = Random.Range(0, 100);
        if (stats.hitPoints <= 0)
        {
            if (rng > 50)
            {
                zombieAnimator.PlayKnockdownBack();
            }
            else
            {
                zombieAnimator.PlayKnockdownFront();
            }
        }

        else 
        {
            rng *= damage;
            if (rng > 1420)
            {
                Debug.Log("playing knockback");
                zombieAnimator.PlayKnockdownBack();
            }
            else if (rng > 1340)
            {
                Debug.Log("playing knockforwards");
                zombieAnimator.PlayKnockdownFront();
            }
            else if (rng > 1200)
            {
                Debug.Log("playing stagger");
                zombieAnimator.PlayStagger();
            }
            else
            {
                Debug.Log("playing hit");
                zombieAnimator.PlayHit();
            }
        }
    }
}

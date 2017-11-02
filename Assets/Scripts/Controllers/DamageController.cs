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

    public void DamageObject(int damage, Stats stats)
    {
        stats.hitPoints -= damage;
        if (stats.transform.gameObject.GetComponent<ZombieController>())
        {
            ZombieAnimator zombieAnimator = stats.transform.gameObject.GetComponent<ZombieAnimator>();
            int rng = Random.Range(0, 100);
            if (rng > 92)
            {
                zombieAnimator.PlayKnockdownBack();
            }
            else if (rng > 85)
            {
                zombieAnimator.PlayKnockdownFront();
            }
            else if (rng > 65)
            {
                zombieAnimator.PlayStagger();
            }
            else
            {
                zombieAnimator.PlayHit();
            }
        }
    }

}

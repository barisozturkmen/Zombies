using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    public float lookRadius = 9f;
    public float hearingRadius = 18f;
    public float disengageRadius = 27f;
    public float attackRangeRadius = 1f;
    public float attackAngle = 20f;
    public float turnSpeed = 5f;
    public bool isWalking = false;
    public bool playingAnimation = false;

    private ZombieAnimator _zombieAnimator;
    private Transform _target;
    private NavMeshAgent _agent;
    private GunController _gunController;
    private bool _followPlayer;
    private Zombie _zombie;
    private Stats _stats;


	void Start () {
        _zombieAnimator = this.GetComponent<ZombieAnimator>();
        _target = PlayerManager.instance.player.transform;
        _agent = this.GetComponent<NavMeshAgent>();
        _gunController = GunController.instance;
        _zombie = this.GetComponent<Zombie>();
        _stats = this.GetComponent<Stats>();
    }

	//void Update () {
    //    float distance = Vector3.Distance(_target.position, transform.position);
    //    //if (playingAnimation == false)
    //    //{
    //    //    Debug.Log("attacking");
    //    //    AttackTarget();
    //    //}
    //
    //
    //    //if in range and not attacking, face target
    //    //if facing, attack
    //    //if not in range, check if player detected
    //    //if detected move to player
    //    if (distance <= attackRangeRadius)
    //    {
    //        isWalking = false;
    //        _followPlayer = false;
    //        if (playingAnimation == false)
    //        {
    //            //FaceTarget();
    //            if ((Vector3.Angle(transform.forward, _target.transform.position - transform.position) < attackAngle))
    //            {
    //                {
    //                    Debug.Log("attacking");
    //                    AttackTarget();
    //                }
    //            }
    //        }
    //    }
    //
    //
    //    //if within sight/heariing - follow
    //    else if (distance <= lookRadius ||
    //        _gunController.equippedGun.isShooting && distance <= hearingRadius)
    //    {
    //        _followPlayer = true;
    //    }
    //    //if outside of disengage radius, within attack range or playing anim - stop
    //    else if (distance >= disengageRadius ||
    //        distance <= attackRangeRadius)
    //    {
    //        _followPlayer = false;
    //        _agent.SetDestination(this.transform.position);
    //    }
    //
    //
    //
    //    if (_followPlayer == true && playingAnimation == false)
    //    {
    //        _agent.SetDestination(_target.position);
    //        if (isWalking == true)
    //        {
    //            _zombieAnimator.PlayWalkRandom();
    //        }
    //        else
    //        {
    //            _zombieAnimator.PlayWalkStartRandom();
    //        }
    //    }
    //    else
    //    {
    //        _agent.SetDestination(this.transform.position);
    //    }
    //
    //
    //}

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void AttackTarget()
    {
        //check if target in range during swipe and do damage

        _zombieAnimator.PlaySwipeRandom();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}

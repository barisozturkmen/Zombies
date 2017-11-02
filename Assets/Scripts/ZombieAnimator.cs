using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimator : MonoBehaviour {

    public enum ZombieAnimations
    {
        Idle,
        Walk_Start, Walk, Walk_Leaning,
        Swipe_Lefthand, Swipe_Righthand,
        Hit, Stagger,
        Knockdown_Back, Knockdown_Front, StandUp_Back, StandUp_Front
    }

    private Animator _animator;
    private ZombieController _zombieController;
    private NavMeshAgent _navMeshAgent;
    private int _walkAnimation;

    private float _staggerAmount = 1.5f;

    private float _idleTime;
    private float _walkStartTime;
    private float _swipeAttackTime;
    private float _hitTime;
    private float _staggerTime;
    private float _knockdownBackTime;
    private float _knockdownFrontTime;
    private float _standUpBackTime;
    private float _standUpFrontTime;

    //private float _

    //remove this
    public PlayerController playercontroller;
    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _zombieController = this.GetComponent<ZombieController>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _walkAnimation = (int)Mathf.Round(Random.Range(0, 2));
        UpdateAnimClipTimes();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public float KnockdownTime()
    {
        float knockdownTime = Random.Range(2, 5);
        return knockdownTime;
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        //Debug.Log(clips.Length);
        foreach (AnimationClip clip in clips)
        {
                //Debug.Log(clip.length);
    
                switch (clip.name)
            {
                case "Idle":
                    _idleTime = clip.length;
                    break;
                case "WalkStart":
                    _walkStartTime = clip.length;
                    break;
                case "SwipeLeftHand":
                    _swipeAttackTime = clip.length;
                break;
                case "Hit":
                    _hitTime = clip.length;
                    break;
                case "Stagger":
                    _staggerTime = clip.length;
                    break;
                case "KnockdownBack":
                    _knockdownBackTime = clip.length;
                    break;
                case "KnockdownFront":
                    _knockdownFrontTime = clip.length;
                    break;
                case "StandUpBack":
                    _standUpBackTime = clip.length;
                    break;
                case "StandUpFront":
                    _standUpFrontTime = clip.length;
                    break;
            }
        }
    }

    //public void PlayIdle()
    //{
    //    StartCoroutine(Idle());
    //}

    public void PlayWalkStartRandom()
    {
        if (_walkAnimation == 0)
        {
            Debug.Log("walk anim start is: " + _walkAnimation);
            StartCoroutine(WalkStart());
        }
        else
            _zombieController.isWalking = true;
    }

    public void PlayWalkRandom()
    {
        if (_walkAnimation == 0)
        {
            StartCoroutine(Walk());
        }
        else
        {
            StartCoroutine(WalkLeaning());
        }
    }

    public void PlaySwipeRandom()
    {
        if ((int)Mathf.Round(Random.Range(0, 2)) == 0)
        {
            StartCoroutine(SwipeLeftHand());
        }
        else
        {
            StartCoroutine(SwipeRightHand());
        }
    }

    public void PlayHit()
    {
        StartCoroutine(Hit());
    }

    public void PlayStagger()
    {
        StopAllCoroutines();
        StartCoroutine(Stagger());
    }

    public void PlayKnockdownBack()
    {
        StopAllCoroutines();
        StartCoroutine(KnockdownBack());
    }

    public void PlayKnockdownFront()
    {

        StopAllCoroutines();
        StartCoroutine(KnockdownFront());

    }

    public void PlayStandUpBack()
    {

        StopAllCoroutines();
        StartCoroutine(StandUpBack());

    }

    public void PlayStandUpFront()
    {

        StopAllCoroutines();
        StartCoroutine(StandUpFront());

    }

    //private IEnumerator Idle()
    //{
    //    float completionAmount = 0f;
    //    while (completionAmount < _idleTime)
    //    {
    //        completionAmount += Time.deltaTime;
    //        _animator.Play(ZombieAnimations.Idle.ToString());
    //        yield return null;
    //    }
    //}

    private IEnumerator WalkStart()
    {
        _zombieController.playingAnimation = true;
        float completionAmount = 0f;
        while (completionAmount < _walkStartTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Walk_Start.ToString());
            yield return null;
        }
        _zombieController.isWalking = true;
        _zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator Walk()
    {
        _animator.Play(ZombieAnimations.Walk.ToString());
        yield return null;
    }

    private IEnumerator WalkLeaning()
    {
        _animator.Play(ZombieAnimations.Walk_Leaning.ToString());
        yield return null;
    }

    private IEnumerator SwipeLeftHand()
    {
        Debug.Log("left");
        _zombieController.playingAnimation = true;
        float completionAmount = 0f;
        while (completionAmount < _swipeAttackTime)
        {
            if (completionAmount < _swipeAttackTime / 2)
            {
                transform.position += transform.forward * Time.deltaTime;
            }
            //else
            //{
            //    transform.position -= transform.forward * Time.deltaTime * 0.3f;
            //}
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Swipe_Lefthand.ToString());
            yield return null;
        }
        completionAmount = 0f;
        while (completionAmount < _idleTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Idle.ToString());
            yield return null;
        }
        _zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator SwipeRightHand()
    {
        Debug.Log("right");
        _zombieController.playingAnimation = true;
        float completionAmount = 0f;

        while (completionAmount < _swipeAttackTime)
        {
            if (completionAmount < _swipeAttackTime / 2)
            {
                transform.position += transform.forward * Time.deltaTime;
            }
            //else
            //{
            //    transform.position -= transform.forward * Time.deltaTime * 0.3f;
            //}
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Swipe_Righthand.ToString());
            yield return null;
        }
        completionAmount = 0f;
        while (completionAmount < _idleTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Idle.ToString());
            yield return null;
        }
        _zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator Hit()
    {
        _animator.SetBool("Hit", true);
        yield return new WaitForSeconds(_hitTime);
        _animator.SetBool("Hit", false);
        yield return null;
    }

    private IEnumerator Stagger()
    {
        _zombieController.playingAnimation = true;
        _zombieController.isWalking = false;
        float completionAmount = 0f;

        while (completionAmount < _staggerTime)
        {
            if (completionAmount < _staggerTime / 2)
            {
                transform.position -= transform.forward * Time.deltaTime * _staggerAmount;
            }
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Stagger.ToString());
            yield return null;
        }
        _zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator KnockdownBack()
    {
        _zombieController.playingAnimation = true;
        _zombieController.isWalking = false;
        _navMeshAgent.enabled = false;
        float completionAmount = 0f;
        float rotationCompletionAmount = 0f;
        float knockdownTIme = KnockdownTime();
        //Quaternion knockDownRotation = Quaternion.Euler(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        float height = 0.7f;
        //float 
        this.transform.position = new Vector2(this.transform.position.x, height);
        while (completionAmount < _knockdownBackTime + knockdownTIme + 10)
        {
            rotationCompletionAmount = (completionAmount * 3.2f);

            this.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, -90, rotationCompletionAmount), this.transform.rotation.y, this.transform.rotation.z);
            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Knockdown_Back.ToString());
            yield return null;

        }
        //_navMeshAgent.enabled = true;
        //_zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator KnockdownFront()
    {
        _zombieController.playingAnimation = true;
        _zombieController.isWalking = false;
        _navMeshAgent.enabled = false;
        float completionAmount = 0f;
        float rotationCompletionAmount = 0f;
        float knockdownTIme = KnockdownTime();
        //Quaternion knockDownRotation = Quaternion.Euler(90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        float height = 0.7f;
        this.transform.position = new Vector2(this.transform.position.x, height);
        while (completionAmount < _knockdownFrontTime + knockdownTIme)
        {
            rotationCompletionAmount = (completionAmount * 3.2f);
            //if (this.transform.eulerAngles.x >= -90f ||
            //    completionAmount < _knockdownBackTime / 1.5)
            //{
            //height = Mathf.Lerp(0, 0.61f, rotationCompletionAmount * 2f);
            this.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, 90, rotationCompletionAmount), this.transform.rotation.y, this.transform.rotation.z);


            //}

            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.Knockdown_Front.ToString());
            yield return null;

        }
        _navMeshAgent.enabled = true;
        _zombieController.playingAnimation = false;
        yield return null;
    }


    private IEnumerator StandUpBack()
    {
        _zombieController.playingAnimation = true;
        _zombieController.isWalking = false;
        _navMeshAgent.enabled = false;
        float completionAmount = 0f;
        float rotationCompletionAmount = 0f;
        while (completionAmount < _standUpBackTime)
        {
            if ((completionAmount / _standUpBackTime) >= 0.5f)
            {
                rotationCompletionAmount = ((completionAmount / _standUpBackTime) - 0.5f) * 2f;
            }

            this.transform.rotation = Quaternion.Euler(Mathf.Lerp(-90, 0, rotationCompletionAmount), this.transform.rotation.y, this.transform.rotation.z);

            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.StandUp_Back.ToString());
            yield return null;
        }


        _navMeshAgent.enabled = true;
        _zombieController.playingAnimation = false;
        yield return null;
    }

    private IEnumerator StandUpFront()
    {
        _zombieController.playingAnimation = true;
        _zombieController.isWalking = false;
        _navMeshAgent.enabled = false;
        float completionAmount = 0f;
        float rotationCompletionAmount = 0f;
        while (completionAmount < _standUpBackTime)
        {
            if (rotationCompletionAmount < 0.25f)
            {
                rotationCompletionAmount = (completionAmount / _standUpBackTime) / 2f;
            }

            else
            {
                rotationCompletionAmount += Time.deltaTime * 1f;
            }


            this.transform.rotation = Quaternion.Euler(Mathf.Lerp(90, 0, rotationCompletionAmount), this.transform.rotation.y, this.transform.rotation.z);

            completionAmount += Time.deltaTime;
            _animator.Play(ZombieAnimations.StandUp_Front.ToString());
            yield return null;
        }

        _navMeshAgent.enabled = true;
        _zombieController.playingAnimation = false;
        yield return null;
    }
}

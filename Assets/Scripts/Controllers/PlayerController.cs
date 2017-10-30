using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GunController))]
public class PlayerController : MonoBehaviour {

    //Components
    private GunController _gunController;
    private MouseController _mouseController = new MouseController();
    private PlayerAnimator _playerAnimator;
    private PlayerInteractor _playerInteractor;

    public bool playingAnimation = false;
    public bool isAiming = false;

    //Handling
    public float rotationSpeed = 300f;
    public float walkSpeed = 0.02f;
    public float runSpeed = 0.05f;

    private Quaternion _targetRotation;
    private Vector3 _newTargetPosition;
    //private float _yOffset = 0.5f;
    private bool _moveToCursorPos = false;
    private bool _canShoot;
    private float _speedMod = 0;
    private float _acceleration = 5;

    private Gun _gun;


    void Start ()
    {
        //_newTargetPosition.y = _yOffset;
        _gunController = GunController.instance;
        _gun = _gunController.equippedGun;
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerInteractor = GetComponent<PlayerInteractor>();
        _mouseController.interactableLayerId = _playerInteractor.interactableLayerId;



    }
	
	void Update ()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        _gun = _gunController.equippedGun;
        //ACCELERATION
        if ((this.transform.position != _newTargetPosition &&
                _moveToCursorPos == true) ||
                Input.GetAxisRaw("Horizontal") != 0 ||
                Input.GetAxisRaw("Vertical") != 0)
        {
            _speedMod = Mathf.Clamp(_speedMod + (_acceleration * Time.deltaTime), 0, 1);
        }
        else
        {
            _speedMod = 0;
        }

        //WEAPONS
        if (Input.GetButton("Aim") && playingAnimation == false && _gunController.equippedGun != null)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
        //if (Input.GetButtonDown("EquipPistol"))
        //{
        //    _gunController.EquipGun(WeaponType.Pistol);
        //}
        //if (Input.GetButtonDown("EquipShotgun"))
        //{
        //    _gunController.EquipGun(WeaponType.Shotgun);
        //}
        //ANIMATION
        //AIMING
        if (isAiming == true)
        {
            _playerAnimator.SetAnimationSpeed(1f);
            _newTargetPosition = this.transform.position;
            if (Input.GetButton("Shoot") && _gun.CanShoot())
            {
                _gunController.OnTriggerHold();
                if (_gun.isShooting == true)
                {
                    _playerAnimator.PlayShoot();
                }
            }
            else if (Input.GetButton("Reload"))
            {
                _playerAnimator.PlayReload();
            }
            else if (Input.GetButtonUp("Shoot"))
            {
                _gunController.OnTriggerRelease();
            }
            else
            {
                _playerAnimator.PlayAim();
            }
        }
        //NOT AIMING
        else if (playingAnimation == false)
        {
            if (Input.GetButtonDown("Interact"))
            {
                GameObject interactableGO = _mouseController.GetInteractableGO(Input.mousePosition);
                if (interactableGO != null)
                {
                    _playerInteractor.InteractWithItem(
                        interactableGO,
                        _playerInteractor.IsInteractableInRange(this.transform.position, interactableGO.transform.position));
                }
            }
            _playerAnimator.PlayDefault();
        }



        //ROTATION
        _targetRotation = _mouseController.GetPlayerRotation(
            _mouseController.GetMouseGroundPosition(Input.mousePosition),
            this.transform.position);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            _targetRotation,
            rotationSpeed * Time.deltaTime);
        //KEYBOARD MOVEMENT
        if (isAiming == false && playingAnimation == false)
        {
            //ANIMATION SPEED
            float animationSpeedPercent = ((Input.GetButton("Run")) ? 1 : 0.5f) * _speedMod;
            _playerAnimator.SetAnimationSpeed(animationSpeedPercent);
            Vector3 keyboardInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (keyboardInput != Vector3.zero)
            {
                keyboardInput *= (Mathf.Abs(keyboardInput.x) == 1 && Mathf.Abs(keyboardInput.z) == 1) ? 0.7f : 1;
                keyboardInput *= ((Input.GetButton("Run")) ? runSpeed : walkSpeed) * _speedMod;
                transform.Translate(keyboardInput, Space.World);
                _moveToCursorPos = false;
            }
            else
            {
                //MOUSE MOVEMENT
                if (Input.GetMouseButton(1))
                {
                    MovePlayerToMouseClick(Input.mousePosition);
                }

                if (this.transform.position != _newTargetPosition &&
                    _moveToCursorPos == true)
                {
                    this.transform.position = Vector3.MoveTowards(
                        this.transform.position,
                        _newTargetPosition,
                        ((Input.GetButton("Run")) ? runSpeed : walkSpeed) * _speedMod);
                }

                if (this.transform.position == _newTargetPosition ||
                    keyboardInput != Vector3.zero)
                {
                    _moveToCursorPos = false;
                }
            }
        }
        Vector3 offsetPos = this.transform.position;
        //offsetPos.y = _yOffset;
        this.transform.position = offsetPos;
    }

    private void MovePlayerToMouseClick(Vector3 mousePos)
    {
        _newTargetPosition = _mouseController.GetMouseClickPosition(mousePos);
        _moveToCursorPos = true;
    }



}

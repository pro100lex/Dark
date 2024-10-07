using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Source _source;

    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private float _stepsCooldown;

    private Vector3 _playerVelocity;

    private void Start()
    {
        _source = GameObject.Find("Source").GetComponent<Source>();

        _rb = GetComponent<Rigidbody>();
        //_rb.freezeRotation = true;

        _source.IsPlayerSquat = false;
        _source.IsPlayerFullGrowth = true;
        //_source.PlayerCurrentMovementName = "FullGrowthWalkingForward";
        //_source.IsPlayerWalking = true;
    }

    private void Update()
    {
        PlayerMovementController();
        PlayerJumpController();
        PlayerGroundCheckController();
        PlayerFallController();
        PlayerInputController();
        MovementType();
        PlayerStepsController();
        PlayerSquatController();

        Debug.Log(_source.PlayerCurrentMovementName);
    }

    private void PlayerMovementController()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        _moveDirection = transform.forward * _verticalInput + transform.right * _horizontalInput;

        try
        {
            _source.PlayerCharacterController.Move(_moveDirection.normalized * _source.PlayerMovementSpeedByType[_source.PlayerCurrentMovementName] * Time.deltaTime);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    private void PlayerFallController()
    {
        if (_source.IsPlayerOnGround && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
    }

    private void PlayerGroundCheckController()
    {
        _source.IsPlayerOnGround = Physics.CheckSphere(_source.PlayerGroundCheck.transform.position, _source.PlayerGroundCheckRadius, _source.PlayerGroundCheckLayerMask);
    }

    private void PlayerJumpController()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _source.IsPlayerOnGround)
        {
            _playerVelocity.y = Mathf.Sqrt(_source.PlayerJumpHeight * -2f * _source.GameGravity);
        }

        _playerVelocity.y += _source.GameGravity * Time.deltaTime;
        _source.PlayerCharacterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void PlayerSquatController()
    {
        if (_source.IsPlayerSquat)
        {
            transform.localScale = new Vector3(transform.localScale.x, _source.PlayerSquatHeightScale, transform.localScale.z);
        }
        else if (!_source.IsPlayerSquat)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
    }

    private void PlayerStepsController()
    {
        string playerMovementStance = GetPlayerMovementStance();
        string playerMovementType = GetPlayerMovementType();
        string playerMovementDirection = GetPlayerMovementDirection();
        if (_stepsCooldown >= 1)
        {
            _stepsCooldown = 0f;
            Debug.Log("Шаг");
            TerrainScannerController();
        }

        if (playerMovementStance == "Air" || playerMovementType == "Staying")
        {
            _stepsCooldown = 0f;
        }
        else
        {
            _stepsCooldown += _source.PlayerStepsSpeedMultiplierByType[_source.PlayerCurrentMovementName] * Time.deltaTime;
        }
    }

    private void PlayerInputController()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_source.IsPlayerSquat)
        {
            _source.IsPlayerSquat = true;
            _source.IsPlayerFullGrowth = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && _source.IsPlayerSquat)
        {
            _source.IsPlayerSquat = false;
            _source.IsPlayerFullGrowth = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _source.IsPlayerRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _source.IsPlayerRunning = false;
        }
    }

    private void TerrainScannerController()
    {
        GameObject terrainScanner = Instantiate(_source.TerrainScannerEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerEffect = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if (terrainScannerEffect != null)
        {
            var main = terrainScannerEffect.main;
            main.startLifetime = 5f;
            main.startSize = 70f;

        }
        else
        {
            Debug.Log("Нет детей");
        }
        Destroy(terrainScanner, 5 + 1);
    }

    private void MovementType()
    {
        string playerMovementStance = GetPlayerMovementStance();
        string playerMovementType = GetPlayerMovementType();
        string playerMovementDirection = GetPlayerMovementDirection();

        Debug.Log(string.Format("{0}, {1}, {2}", playerMovementStance, playerMovementType, playerMovementDirection));

        if (playerMovementStance == "Air")
        {
            if (playerMovementType == "Staying")
            {
                _source.PlayerCurrentMovementName = "AirStaying";
            }

            else if (playerMovementType == "Walking")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "AirWalkingForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "AirWalkingBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "AirWalkingSideways";
                }
                //else if (playerMovementDirection == "None")
                //{
                //    _source.PlayerCurrentMovementName = "AirWalkingNone";
                //}
            }

            else if (playerMovementType == "Running")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "AirRunningForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "AirRunningBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "AirRunningSideways";
                }
                //else if (playerMovementDirection == "None")
                //{
                //    _source.PlayerCurrentMovementName = "AirRunningNone";
                //}
            }
        }

        else if (playerMovementStance == "Squat")
        {
            if (playerMovementType == "Staying")
            {
                _source.PlayerCurrentMovementName = "SquatStaying";
            }

            else if (playerMovementType == "Walking")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "SquatWalkingForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "SquatWalkingBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "SquatWalkingSideways";
                }
                //else if (playerMovementDirection == "None")
                //{

                //}
            }

            else if (playerMovementType == "Running")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "SquatRunningForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "SquatRunningBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "SquatRunningSideways";
                }
                //else if (playerMovementDirection == "None")
                //{

                //}
            }
        }
        else if (playerMovementStance == "FullGrowth")
        {
            if (playerMovementType == "Staying")
            {
                _source.PlayerCurrentMovementName = "FullGrowthStaying";
            }

            else if (playerMovementType == "Walking")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthWalkingForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthWalkingBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthWalkingSideways";
                }
                //else if (playerMovementDirection == "None")
                //{

                //}
            }

            else if (playerMovementType == "Running")
            {
                if (playerMovementDirection == "Forward")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthRunningForward";
                }

                else if (playerMovementDirection == "Backward")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthRunningBackward";
                }

                else if (playerMovementDirection == "Sideways")
                {
                    _source.PlayerCurrentMovementName = "FullGrowthRunningSideways";
                }
                //else if (playerMovementDirection == "None")
                //{

                //}
            }
        }
    }

    private string GetPlayerMovementStance()
    {
        if (!_source.IsPlayerOnGround)
        {
            return "Air";
        }
        else
        {
            if (_source.IsPlayerSquat)
            {
                return "Squat";
            }
            else
            {
                return "FullGrowth";
            }
        }
    }

    private string GetPlayerMovementType()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            return "Running";
        }

        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftShift))
        {
            return "Walking";
        }

        else
        {
            return "Staying";
        }
    }

    private string GetPlayerMovementDirection()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return "Forward";
        }

        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            return "Backward";
        }

        else if (!Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            return "Sideways";
        }

        else
        {
            return "None";
        }
    }
}

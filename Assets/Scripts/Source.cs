using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{

    [SerializeField] private float _playerCameraSensivityX;
    [SerializeField] private float _playerCameraSensivityY;

    private float _playerCameraRotationX;
    private float _playerCameraRotationY;

    [SerializeField] private Transform _playerCameraPosition;
    [SerializeField] private Transform _playerCameraOrientation;

    [SerializeField] private CharacterController _playerCharacterController;

    [SerializeField] private GameObject _playerObject;

    [SerializeField] private float _playerMovementSpeed;

    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _layerGround;
    private bool _isPlayerOnGround;
    [SerializeField] private float _playerGroundDrag;

    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private GameObject _playerStepRayUpper;
    [SerializeField] private GameObject _playerStepRayLower;
    [SerializeField] private float _playerStepHeight;
    [SerializeField] private float _playerStepSmooth;

    [SerializeField] private GameObject _playerModel;

    private bool _isPlayerWalking;
    private bool _isPlayerStaying;
    private bool _isPlayerRunning;
    private bool _isPlayerCrouching;

    private string _playerMovementByType;

    [SerializeField] private float _playerWalkingSpeed;
    [SerializeField] private float _playerRunningSpeed;

    [SerializeField] private float _playerSpeedMultiplierInAir;

    private Dictionary<string, float> _playerMovementSpeedByType = new Dictionary<string, float>();
    private Dictionary<string, float> _playerStepsSpeedMultiplierByType = new Dictionary<string, float>();

    [SerializeField] private float _gameGravity;

    [SerializeField] private GameObject _playerGroundCheck;
    [SerializeField] private float _playerGroundCheckRadius;
    [SerializeField] private LayerMask _playerGroundCheckLayerMask;

    [SerializeField] private float _playerJumpHeight;

    private bool _isPlayerSquat;
    private bool _isPlayerFullGrowth;

    private string _playerMovementStance;
    private string _playerMovementType;
    private string _playerMovementDirection;

    private string _playerCurrentMovementName;

    [SerializeField] private float _playerSquatHeightScale;

    [SerializeField] private float _playerAirStayingSpeed;
    [SerializeField] private float _playerAirWalkingForwardSpeed;
    [SerializeField] private float _playerAirWalkingBackwardSpeed;
    [SerializeField] private float _playerAirWalkingSidewaysSpeed;
    [SerializeField] private float _playerAirRunningForwardSpeed;
    [SerializeField] private float _playerAirRunningBackwardSpeed;
    [SerializeField] private float _playerAirRunningSidewaysSpeed;
    //[SerializeField] private float _
    [SerializeField] private float _playerFullGrowthStayingSpeed;
    [SerializeField] private float _playerFullGrowthWalkingForwardSpeed;
    [SerializeField] private float _playerFullGrowthWalkingBackwardSpeed;
    [SerializeField] private float _playerFullGrowthWalkingSidewaysSpeed;
    [SerializeField] private float _playerFullGrowthRunningForwardSpeed;
    [SerializeField] private float _playerFullGrowthRunningBackwardSpeed;
    [SerializeField] private float _playerFullGrowthRunningSidewaysSpeed;

    [SerializeField] private float _playerSquatStayingSpeed;
    [SerializeField] private float _playerSquatWalkingForwardSpeed;
    [SerializeField] private float _playerSquatWalkingBackwardSpeed;
    [SerializeField] private float _playerSquatWalkingSidewaysSpeed;
    [SerializeField] private float _playerSquatRunningForwardSpeed;
    [SerializeField] private float _playerSquatRunningBackwardSpeed;
    [SerializeField] private float _playerSquatRunningSidewaysSpeed;



    [SerializeField] private float _playerAirStayingStepsMultiplierSpeed;
    [SerializeField] private float _playerAirWalkingForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerAirWalkingBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerAirWalkingSidewaysStepsMultiplierSpeed;
    [SerializeField] private float _playerAirRunningForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerAirRunningBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerAirRunningSidewaysStepsMultiplierSpeed;
    //[SerializeField] private float _
    [SerializeField] private float _playerFullGrowthStayingStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthWalkingForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthWalkingBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthWalkingSidewaysStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthRunningForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthRunningBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerFullGrowthRunningSidewaysStepsMultiplierSpeed;

    [SerializeField] private float _playerSquatStayingStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatWalkingForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatWalkingBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatWalkingSidewaysStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatRunningForwardStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatRunningBackwardStepsMultiplierSpeed;
    [SerializeField] private float _playerSquatRunningSidewaysStepsMultiplierSpeed;

    [SerializeField] private GameObject _terrainScannerEffect;

    private void Awake() 
    {
        _playerMovementSpeedByType.Add("AirStaying", _playerAirStayingSpeed);
        _playerMovementSpeedByType.Add("AirWalkingForward", _playerAirWalkingForwardSpeed);
        _playerMovementSpeedByType.Add("AirWalkingBackward", _playerAirWalkingBackwardSpeed);
        _playerMovementSpeedByType.Add("AirWalkingSideways", _playerAirWalkingSidewaysSpeed);
        _playerMovementSpeedByType.Add("AirRunningForward", _playerAirRunningForwardSpeed);
        _playerMovementSpeedByType.Add("AirRunningBackward", _playerAirRunningBackwardSpeed);
        _playerMovementSpeedByType.Add("AirRunningSideways", _playerAirRunningSidewaysSpeed);

        _playerMovementSpeedByType.Add("FullGrowthStaying", _playerFullGrowthStayingSpeed);
        _playerMovementSpeedByType.Add("FullGrowthWalkingForward", _playerFullGrowthWalkingForwardSpeed);
        _playerMovementSpeedByType.Add("FullGrowthWalkingBackward", _playerFullGrowthWalkingBackwardSpeed);
        _playerMovementSpeedByType.Add("FullGrowthWalkingSideways", _playerFullGrowthWalkingSidewaysSpeed);
        _playerMovementSpeedByType.Add("FullGrowthRunningForward", _playerFullGrowthRunningForwardSpeed);
        _playerMovementSpeedByType.Add("FullGrowthRunningBackward", _playerFullGrowthRunningBackwardSpeed);
        _playerMovementSpeedByType.Add("FullGrowthRunningSideways", _playerFullGrowthRunningSidewaysSpeed);

        _playerMovementSpeedByType.Add("SquatStaying", _playerSquatStayingSpeed);
        _playerMovementSpeedByType.Add("SquatWalkingForward", _playerSquatWalkingForwardSpeed);
        _playerMovementSpeedByType.Add("SquatWalkingBackward", _playerSquatWalkingBackwardSpeed);
        _playerMovementSpeedByType.Add("SquatWalkingSideways", _playerSquatWalkingSidewaysSpeed);
        _playerMovementSpeedByType.Add("SquatRunningForward", _playerSquatRunningForwardSpeed);
        _playerMovementSpeedByType.Add("SquatRunningBackward", _playerSquatRunningBackwardSpeed);
        _playerMovementSpeedByType.Add("SquatRunningSideways", _playerSquatRunningSidewaysSpeed);



        _playerStepsSpeedMultiplierByType.Add("AirStaying", _playerAirStayingStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirWalkingForward", _playerAirWalkingForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirWalkingBackward", _playerAirWalkingBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirWalkingSideways", _playerAirWalkingSidewaysStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirRunningForward", _playerAirRunningForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirRunningBackward", _playerAirRunningBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("AirRunningSideways", _playerAirRunningSidewaysStepsMultiplierSpeed);

        _playerStepsSpeedMultiplierByType.Add("FullGrowthStaying", _playerFullGrowthStayingStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthWalkingForward", _playerFullGrowthWalkingForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthWalkingBackward", _playerFullGrowthWalkingBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthWalkingSideways", _playerFullGrowthWalkingSidewaysStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthRunningForward", _playerFullGrowthRunningForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthRunningBackward", _playerFullGrowthRunningBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("FullGrowthRunningSideways", _playerFullGrowthRunningSidewaysStepsMultiplierSpeed);

        _playerStepsSpeedMultiplierByType.Add("SquatStaying", _playerSquatStayingStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatWalkingForward", _playerSquatWalkingForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatWalkingBackward", _playerSquatWalkingBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatWalkingSideways", _playerSquatWalkingSidewaysStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatRunningForward", _playerSquatRunningForwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatRunningBackward", _playerSquatRunningBackwardStepsMultiplierSpeed);
        _playerStepsSpeedMultiplierByType.Add("SquatRunningSideways", _playerSquatRunningSidewaysStepsMultiplierSpeed);
    }

    public float PlayerCameraSensivityX { get { return _playerCameraSensivityX; } }
    public float PlayerCameraSensivityY { get { return _playerCameraSensivityY; } }
    public float PlayerCameraRotationX { get { return _playerCameraRotationX; } set { _playerCameraRotationX = value; } }
    public float PlayerCameraRotationY { get { return _playerCameraRotationY; } set { _playerCameraRotationY = value; } }
    public Transform PlayerCameraPosition { get { return _playerCameraPosition; } }
    public Transform PlayerCameraOrientation { get { return _playerCameraOrientation; } }
    public float PlayerMovementSpeed {  get { return _playerMovementSpeed; } }
    public float PlayerHeight { get { return _playerHeight; } }
    public LayerMask LayerGround { get { return _layerGround; } }
    public bool IsPlayerOnGround { get { return _isPlayerOnGround;} set { _isPlayerOnGround = value; } }
    public float PlayerGroundDrag { get {  return _playerGroundDrag; } }
    public Animator PlayerAnimator { get { return _playerAnimator; } set { _playerAnimator = value; } }

    public GameObject PlayerStepRayUpper { get { return _playerStepRayUpper; } }
    public GameObject PlayerStepRayLower { get {  return _playerStepRayLower; } }
    public float PlayerStepHeight {  get { return _playerStepHeight; } }
    public float PlayerStepSmooth {  get { return _playerStepSmooth; } }

    public GameObject PlayerModel {  get { return _playerModel; } }
    public bool IsPlayerWalking {  get { return _isPlayerWalking; } set { _isPlayerWalking = value; } }
    public bool IsPlayerStaying {  get { return _isPlayerStaying; } set { _isPlayerStaying = value; } }
    public bool IsPlayerRunning { get { return _isPlayerRunning; } set { _isPlayerRunning = value; } }

    public string PlayerMovementByType {  get { return _playerMovementByType; } set { _playerMovementByType = value; } }
    public Dictionary<string, float> PlayerMovementSpeedByType {  get { return _playerMovementSpeedByType; } }
    public float PlayerSpeedMultiplierInAir {  get { return _playerSpeedMultiplierInAir;} }
    public bool IsPlayerCrouching { get { return _isPlayerCrouching;} set {  _isPlayerCrouching = value;} }
    public GameObject PlayerObject { get { return _playerObject; } set { _playerObject = value; } }
    public CharacterController PlayerCharacterController { get { return _playerCharacterController; } set { _playerCharacterController = value; } }
    public float GameGravity { get { return _gameGravity; } }
    public GameObject PlayerGroundCheck { get { return _playerGroundCheck; } }
    public float PlayerGroundCheckRadius {  get { return _playerGroundCheckRadius; } }
    public LayerMask PlayerGroundCheckLayerMask {  get { return _playerGroundCheckLayerMask; } }
    public float PlayerJumpHeight { get { return _playerJumpHeight; } }
    public bool IsPlayerSquat {  get { return _isPlayerSquat; } set { _isPlayerSquat = value; } }
    public bool IsPlayerFullGrowth {  get { return _isPlayerFullGrowth; } set { _isPlayerFullGrowth = value; } }
    public string PlayerCurrentMovementName {  get { return _playerCurrentMovementName; } set { _playerCurrentMovementName = value; } }
    public Dictionary<string, float> PlayerStepsSpeedMultiplierByType {  get { return _playerStepsSpeedMultiplierByType; } }

    public GameObject TerrainScannerEffect { get { return _terrainScannerEffect; } }
    public float PlayerSquatHeightScale { get { return _playerSquatHeightScale; } }
}

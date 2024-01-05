using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabberTool : Tool
{
    [SerializeField]
    private Transform movingPart;

    [SerializeField]
    private GrabberToolSocket socket;
    
    [SerializeField]
    private GrabberToolTerrainCollider terrainCollider;

    [SerializeField]
    private LineRenderer rope;

    [SerializeField]
    private float maxDistance = 20;

    [SerializeField]
    private float shootingSpeed = 20;

    [SerializeField]
    private float reelingSpeed = 10;

    [BoxGroup("Audio")]
    [LabelText("Reel Loop Outgoing")]
    [SerializeField]
    private AudioClip audioClipReelOutgoing;

    [BoxGroup("Audio")]
    [LabelText("Reel Loop Incoming")]
    [SerializeField]
    private AudioClip audioClipReelLoopIncoming;

    [BoxGroup("Audio")]
    [LabelText("Shoot")]
    [SerializeField]
    private AudioClip audioClipShoot;

    [BoxGroup("Audio")]
    [LabelText("Hit Garbage")]
    [SerializeField]
    private AudioClip audioClipHitGarbage;

    [BoxGroup("Audio")]
    [LabelText("Return with Garbage")]
    [SerializeField]
    private AudioClip audioClipReturnWithGarbage;

    [BoxGroup("Audio")]
    [LabelText("Return without Garbage")]
    [SerializeField]
    private AudioClip audioClipReturnWithoutGarbage;
    
    [BoxGroup("Audio")]
    [LabelText("Release Garbage")]
    [SerializeField]
    private AudioClip audioClipReleaseGarbage;

    [FoldoutGroup("Audio/Sources")]
    [SerializeField]
    private AudioSource audioSourceReelLoop;

    [FoldoutGroup("Audio/Sources")]
    [SerializeField]
    private AudioSource audioSourceOneShots;
    
    [InfoBox("X: amplitude (0-1), Y: duration in seconds")]
    [BoxGroup("Haptics")]
    [LabelText("Shoot")]
    [SerializeField]
    private Vector2 hapticsShoot;

    [BoxGroup("Haptics")]
    [LabelText("Hit Garbage")]
    [SerializeField]
    private Vector2 hapticsHitGarbage;

    [BoxGroup("Haptics")]
    [LabelText("Return with Garbage")]
    [SerializeField]
    private Vector2 hapticsReturnWithGarbage;

    [BoxGroup("Haptics")]
    [LabelText("Return without Garbage")]
    [SerializeField]
    private Vector2 hapticsReturnWithoutGarbage;


    private GrabberState _currentState;
    private bool _garbageIsLocked;

    private Rigidbody _grabberRigidbody;

    private Garbage _grabbedGarbage;
    private Transform _grabbedGarbageTransform;
    private Rigidbody _grabbedGarbageRigidbody;

    public bool IsShootingOutwards => _currentState == GrabberState.Shooting;

    private void Awake()
    {
        socket.OnHitGarbage += OnHitGarbage;
        terrainCollider.OnHitTerrain += Reel;
        _grabberRigidbody = movingPart.GetComponent<Rigidbody>();
    }

    protected override void DoActivate()
    {
        switch (_currentState)
        {
            case GrabberState.Idle:
                if (_garbageIsLocked) Release();
                else Shoot();
                break;
            case GrabberState.Shooting:
                Reel();
                break;
        }
    }

    protected override void DoDeactivate()
    {
    }

    private void Shoot()
    {
        movingPart.SetParent(null);
        _currentState = GrabberState.Shooting;

        audioSourceOneShots.PlayOneShot(audioClipShoot);
        audioSourceReelLoop.clip = audioClipReelOutgoing;
        audioSourceReelLoop.Play();
        
        attachedMultiTool.SendHapticImpulse(hapticsShoot.x,hapticsShoot.y);
    }

    private void Reel()
    {
        _currentState = GrabberState.Reeling;

        audioSourceReelLoop.clip = audioClipReelLoopIncoming;
        audioSourceReelLoop.Play();
    }

    private void Idle()
    {
        movingPart.SetParent(transform);
        movingPart.SetPositionAndRotation(transform.position, transform.rotation);
        _currentState = GrabberState.Idle;

        audioSourceReelLoop.Stop();
        audioSourceOneShots.PlayOneShot(_garbageIsLocked ? audioClipReturnWithGarbage : audioClipReturnWithoutGarbage);

        Vector2 haptics = _garbageIsLocked ? hapticsReturnWithGarbage : hapticsReturnWithoutGarbage;
        attachedMultiTool.SendHapticImpulse(haptics.x,haptics.y);
    }

    private void Release()
    {
        _grabbedGarbageRigidbody.isKinematic = false;
        _grabbedGarbageTransform.SetParent(null);
        _grabbedGarbage.IsActive = true;

        _grabbedGarbageRigidbody.velocity = Velocity * 2;
        _grabbedGarbageRigidbody.angularVelocity = AngularVelocity;

        _grabbedGarbage = null;
        _grabbedGarbageTransform = null;
        _grabbedGarbageRigidbody = null;
        _garbageIsLocked = false;
        
        audioSourceOneShots.PlayOneShot(audioClipReleaseGarbage);
    }

    private void OnHitGarbage(Garbage garbage)
    {
        if (_garbageIsLocked || _currentState != GrabberState.Shooting) return;

        _currentState = GrabberState.Reeling;
        _garbageIsLocked = true;

        _grabbedGarbage = garbage;
        _grabbedGarbageTransform = garbage.GetComponent<Transform>();
        _grabbedGarbageRigidbody = garbage.GetComponent<Rigidbody>();

        _grabbedGarbageTransform.SetParent(movingPart, true);
        _grabbedGarbage.IsActive = false;

        _grabbedGarbage.GetComponent<XRGrabInteractable>().selectEntered.AddListener(args => OnGrabGarbage());
        
        audioSourceOneShots.PlayOneShot(audioClipHitGarbage);
        
        attachedMultiTool.SendHapticImpulse(hapticsHitGarbage.x,hapticsHitGarbage.y);
    }

    private void OnGrabGarbage()
    {
        _grabbedGarbage = null;
        _grabbedGarbageTransform = null;
        _grabbedGarbageRigidbody = null;
        _garbageIsLocked = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 transformPosition = transform.position;
        Vector3 grabberPosition = movingPart.position;
        float distance = Vector3.Distance(transformPosition, grabberPosition);


        switch (_currentState)
        {
            case GrabberState.Idle:
                break;
            case GrabberState.Shooting:
                _grabberRigidbody.MovePosition(grabberPosition + movingPart.forward * (shootingSpeed * Time.fixedDeltaTime));
                if (distance > maxDistance) Reel();
                break;
            case GrabberState.Reeling:
                float requiredTime = distance / reelingSpeed;
                if (!_garbageIsLocked) requiredTime /= 2;
                Vector3 newGrabberPosition =
                    Vector3.Lerp(grabberPosition, transformPosition, Time.fixedDeltaTime / requiredTime);
                _grabberRigidbody.MovePosition(newGrabberPosition);
                if (distance < .1f) Idle();
                break;
        }

        rope.SetPosition(1,transformPosition);
        rope.SetPosition(0,movingPart.position);
    }

    private enum GrabberState
    {
        Idle,
        Shooting,
        Reeling,
    }
}
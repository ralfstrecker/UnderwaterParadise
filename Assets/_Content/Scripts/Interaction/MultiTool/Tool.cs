using System.Linq;
using CircularBuffer;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    protected bool IsActivated;
    
    [HideInInspector]
    public MultiTool attachedMultiTool;

    protected Vector3 Velocity =>
        _velocityBuffer.Aggregate(Vector3.zero, (current, t) => current + t) / _velocityBuffer.Size;

    protected Vector3 AngularVelocity =>
        _angularVelocityBuffer.Aggregate(Vector3.zero, (current, t) => current + t) / _angularVelocityBuffer.Size;

    private readonly CircularBuffer<Vector3> _velocityBuffer = new CircularBuffer<Vector3>(10);
    private Vector3 _lastPosition;
    private readonly CircularBuffer<Vector3> _angularVelocityBuffer = new CircularBuffer<Vector3>(10);
    private Quaternion _lastRotation;


    public void Activate()
    {
        IsActivated = true;
        DoActivate();
    }

    public void Deactivate()
    {
        IsActivated = false;
        DoDeactivate();
    }

    protected virtual void FixedUpdate()
    {
        // Buffer Velocity
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = (currentPosition - _lastPosition) / Time.fixedDeltaTime;
        _lastPosition = currentPosition;
        _velocityBuffer.PushFront(currentVelocity);

        // Buffer Angular Velocity
        Quaternion currentRotation = transform.rotation;
        Quaternion velocityDifference = currentRotation * Quaternion.Inverse(_lastRotation);
        Vector3 currentAngularVelocity = Mathf.Deg2Rad / Time.fixedDeltaTime * new Vector3(
            Mathf.DeltaAngle(0f, velocityDifference.eulerAngles.x),
            Mathf.DeltaAngle(0f, velocityDifference.eulerAngles.y),
            Mathf.DeltaAngle(0f, velocityDifference.eulerAngles.z)
        );
        _lastRotation = currentRotation;
        _angularVelocityBuffer.PushFront(currentAngularVelocity);
    }


    protected abstract void DoActivate();
    protected abstract void DoDeactivate();
}
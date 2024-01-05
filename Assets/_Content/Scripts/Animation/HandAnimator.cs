using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private ActionBasedController _controller;
    private static readonly int TriggerHash = Animator.StringToHash("Trigger");
    private static readonly int GripHash = Animator.StringToHash("Grip");

    private void Start()
    {
        _controller = GetComponentInParent<ActionBasedController>();
    }

    private void Update()
    {
        animator.SetFloat(TriggerHash, _controller.activateActionValue.action.ReadValue<float>());
        animator.SetFloat(GripHash, _controller.selectActionValue.action.ReadValue<float>());
    }
}
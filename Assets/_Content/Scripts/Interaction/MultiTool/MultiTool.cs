using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MultiTool : MonoBehaviour
{
    [SerializeField]
    private Transform toolSlot;

    [SerializeField]
    private List<Tool> tools;

    private readonly List<Tool> _instantiatedTools = new List<Tool>();
    private Tool _currentTool;

    private ActionBasedController _selectingController;

    private void Awake()
    {
        InitializeTools();
        SwitchTool(0);
    }

    private void InitializeTools()
    {
        foreach (Tool tool in tools)
        {
            Tool toolObject = Instantiate(tool, toolSlot);
            toolObject.attachedMultiTool = this;
            toolObject.gameObject.SetActive(false);
            _instantiatedTools.Add(toolObject);
        }
    }

    private void SwitchTool(int index)
    {
        if (index < 0 || index >= tools.Count) return;

        if (_currentTool != null) _currentTool.gameObject.SetActive(false);
        _currentTool = _instantiatedTools[index];
        _currentTool.gameObject.SetActive(true);
    }

    public void Select()
    {
        _selectingController = GetComponent<XRGrabInteractable>()
            .firstInteractorSelecting.transform
            .GetComponent<ActionBasedController>();
    }

    public void Deselect()
    {
        _selectingController = null;
    }

    public void Activate() => _currentTool.Activate();

    public void Deactivate() => _currentTool.Deactivate();

    public void SendHapticImpulse(float amplitude, float duration)
    {
        if (_selectingController != null) _selectingController.SendHapticImpulse(amplitude, duration);
    }

    public void DeleteIfNotGrabbed()
    {
        if (_selectingController != null) return;
        Destroy(gameObject);
    }
}
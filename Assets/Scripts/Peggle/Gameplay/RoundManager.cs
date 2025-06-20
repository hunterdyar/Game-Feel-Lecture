using System;
using Peggle;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private PeggleManager peggleManager;
    [SerializeField] private InputActionReference _restartAction;
    private void Awake()
    {
        peggleManager.Init();
        _restartAction.action.Enable();
    }

    void Start()
    {
        peggleManager.StartNewGame(this);   
    }

    private void Update()
    {
        if (_restartAction.action.WasPerformedThisFrame())
        {
            peggleManager.StartNewGame((this));
        }
    }
}

using System;
using Peggle;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private PeggleManager peggleManager;

    private void Awake()
    {
        peggleManager.Init();
    }

    void Start()
    {
        peggleManager.StartNewGame(this);   
    }
}

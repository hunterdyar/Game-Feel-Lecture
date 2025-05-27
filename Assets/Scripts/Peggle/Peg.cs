using System;
using Peggle;
using UnityEngine;
using UnityEngine.Serialization;

public class Peg : MonoBehaviour, IBallHit
{
    public static Action<Peg> OnPegHit;
    public static Action<Peg> OnPegCleared;
    public static Action<Peg> OnPegLoadedIn;
    public Action<PegType> OnPegTypeChanged;
    public Action<int, Collision2D> OnThisPegHit;
    public Action<PegState> OnPegStateChanged;
    
    [FormerlySerializedAs("_manager")] public PeggleManager Manager;
    private Collider2D _col;
    private int _hitCount = 0;
    public PegType PegType => _pegType;
    private PegType _pegType;
    private PegState _pegState;
    
    [SerializeField] int _hitsToClear = 1;

    private float ballStuckTime = 0;
    void Awake()
    {
        _col = GetComponent<Collider2D>();
        _pegState = PegState.ActiveToBeHit;
        Manager.RegisterPeg(this);
    }
    
    private void OnEnable()
    {
        PeggleManager.OnRoundStart += OnRoundStart;
        PeggleManager.StartGame += StartGame;
    }

    private void StartGame()
    {
        _pegState = PegState.ActiveToBeHit;
        _hitCount = 0;
        _col.enabled = true;
        OnPegLoadedIn?.Invoke(this);
        OnPegStateChanged(_pegState);
    }

    public void SetPegType(PegType pegType)
    {
        _pegType = pegType;
        //update visuals
        OnPegTypeChanged?.Invoke(_pegType);
    }
    private void OnDisable()
    {
        PeggleManager.OnRoundStart -= OnRoundStart;
        PeggleManager.StartGame -= StartGame;
    }

    private void OnRoundStart()
    {
        //
    }

    public void Hit(Ball ball, Collision2D collision)
    {
        _hitCount++;
        if (_hitCount >= _hitsToClear && _pegState == PegState.ActiveToBeHit)
        {
            _pegState = PegState.LitUp;
            OnPegStateChanged?.Invoke(_pegState);
            Manager.PegLitUp(this);
        }
        OnThisPegHit?.Invoke(_hitCount, collision);
        OnPegHit?.Invoke(this);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        ballStuckTime += Time.deltaTime;
        if (ballStuckTime >= Manager.Settings.timeOfContinuousContactBeforeRemovingPeg)
        {
            //todo: Check if ball is moving not-that-slowly
            _pegState = PegState.ClearedByStuck;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        ballStuckTime = 0;
    }

    public void Clear()
    {
        _pegState = PegState.Cleared;
        OnPegStateChanged?.Invoke(_pegState);
        _col.enabled = false;
        OnPegCleared?.Invoke(this);
    }
}

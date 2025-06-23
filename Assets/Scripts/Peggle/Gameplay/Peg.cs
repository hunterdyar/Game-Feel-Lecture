using System;
using BTween;
using Peggle;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Peg : MonoBehaviour, IBallHit
{
    public static Action<Peg> OnPegHit;
    public static Action<Peg> OnPegCleared;
    public static Action<Peg> OnPegLoadedIn;
    public Action<PegType> OnPegTypeChanged;
    public Action<int, Collision2D> OnThisPegHit;
    public Action<PegState> OnPegStateChanged;
    
    [FormerlySerializedAs("_manager")] public PeggleManager Manager;
    private CircleCollider2D _col;
    private int _hitCount = 0;
    public PegType PegType => _pegType;
    private PegType _pegType;
    private PegState _pegState;
    private Tween _entranceTween;
    
    [SerializeField] int _hitsToClear = 1;

    private float ballStuckTime = 0;
    void Awake()
    {
        _col = GetComponent<CircleCollider2D>();
        _pegState = PegState.ActiveToBeHit;
        Manager.RegisterPeg(this);
        BallPrediction.RegisterCircleColliderForPrediction(gameObject,_col);
    }

    private void OnDestroy()
    {
        BallPrediction.UnregisterColliderForPrediction(gameObject);
        if (_entranceTween != null)
        {
            _entranceTween.Stop();
            _entranceTween = null;
        }
    }

    private void OnEnable()
    {
        PeggleManager.OnRoundStart += OnRoundStart;
        PeggleManager.StartGame += StartGame;
        BallPrediction.SetEnabled(gameObject,true);
    }

    private void StartGame()
    {
        _pegState = PegState.ActiveToBeHit;
        _hitCount = 0;
        _col.enabled = true;
        OnPegLoadedIn?.Invoke(this);
        OnPegStateChanged(_pegState);
        if (PeggleManager.Settings.PegEntranceAnimation)
        {
            float delay = Random.Range(0,PeggleManager.Settings.RandomVariationEntranceAnimationStartTime);
            if (delay <= Mathf.Epsilon)
            {
                _entranceTween = transform.BScaleFromTo(Vector3.zero, Vector3.one, 1f, Ease.EaseOutBounce, true);
            }
            else
            {
                transform.localScale = Vector3.zero;
                _entranceTween = new Tween(delay);//a tween with no property tweens is still a valid tween.
                _entranceTween.Add(new NothingTween());
                _entranceTween.Then(transform.BScaleTo(Vector3.one, 1f, Ease.EaseOutBounce, false));
                _entranceTween.Start();
            }
        }
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
        BallPrediction.SetEnabled(gameObject, false);
        
        if (_entranceTween != null)
        {
            _entranceTween.Stop();
            _entranceTween = null;
        }
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

    public void Exit(Ball ball, Collision2D collision)
    {
        ballStuckTime = 0;
    }

    public void Stay(Ball ball, Collision2D collision)
    { 
        ballStuckTime += Time.deltaTime;
        if (ballStuckTime >= PeggleManager.Settings.timeOfContinuousContactBeforeRemovingPeg)
        {
            //todo: Check if ball is moving not-that-slowly
            _pegState = PegState.ClearedByStuck;
            gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        _pegState = PegState.Cleared;
        OnPegStateChanged?.Invoke(_pegState);
        _col.enabled = false;
        BallPrediction.SetEnabled(gameObject, false);
        OnPegCleared?.Invoke(this);
    }
}

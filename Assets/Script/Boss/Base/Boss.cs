using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    [field: SerializeField]public bool CanShoot { get; set; }
    [field: SerializeField]public GameObject MainObject { get; set; }
    [field: SerializeField] public float DeadDuration { get; set; }

    public StandingEnemyStateMachine StateMachine { get; set; }
    public BossATK1State ATK1State { get; set; }
    public BossATK2State ATK2State { get; set; }
    public BossATK3State ATK3State { get; set; }

    public float ATK1Delay;
    public int ATK1RepeatTime;
    public float ATK2Delay;
    public int ATK2RepeatTime;
    public float ATK3Delay;
    public int ATK3RepeatTime;

    public Animator Animator { get; set; }

    private void Awake()
    {
        StateMachine = new StandingEnemyStateMachine();
        ATK1State = new BossATK1State(this, StateMachine);
        ATK2State = new BossATK2State(this, StateMachine);
        ATK3State = new BossATK3State(this, StateMachine);
    }
    private void Start()
    {
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(ATK1State);   
    }
    private void Update()
    {
        if(!CanShoot)return;
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        if(!CanShoot)return;
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    public virtual void BossATK1()
    {
    }
    public virtual void BossATK2()
    {
    }
    public virtual void BossATK3()
    {
    }
    public enum AnimationTriggerType
    {
        IdleAnimFinish,
        DeadAnimFinish,
        AttackAnimFinish
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
    public void SetCanShoot(bool value)
    {
        CanShoot = value;
    }
    public void BossDead()
    {
        SoundControl.Instance.BossDeathSoundPlay();
        Debug.Log("Boss Dead Animation");
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        Sequence seq = DOTween.Sequence();

        seq.Join(
            transform.DORotate(
                new Vector3(0, 0, -360),
                DeadDuration,
                RotateMode.FastBeyond360
            )
        );

        seq.Join(
            transform.DOScale(Vector3.zero, DeadDuration)
        );

        seq.SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            InGamePauseManager.Instance.WinGameMenuOn();
            gameObject.SetActive(false);
        });
    }
}

using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set;}

    public StateMachine()
    {
        CurrentState = null;
    }

    public void Initialize(State initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        if (CurrentState == newState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

public abstract class State
{
    protected readonly Player _player;
    protected readonly Rigidbody2D _rigidbody;

    public State(Player player)
    {
        _player = player;
        _rigidbody = player.GetComponent<Rigidbody2D>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void HandleInput() { }
}

public class WaitState : State
{
    public WaitState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _player.gameObject.SetActive(true);
        Debug.Log("Waiting for game start...");
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.StateMachine.ChangeState(new FlyState(_player));
        }
    }
}

public class FlyState : State
{
    public FlyState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Player is flying!");
    }

    public override void Update()
    {
        HandleInput();
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Jump();
        }
    }
}




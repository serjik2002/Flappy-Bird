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




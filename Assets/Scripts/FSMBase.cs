using UnityEngine;
using System.Collections;

public class FSMBase : MonoBehaviour {

    public enum State
    {
        Run, Attack, Jump, Dead, Skill
    }

    public State state = State.Run;

    [HideInInspector]
    public new Transform transform;

    //protected CharacterController _cc;
    protected Animator _animator;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        //_cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Invoke(state.ToString(), 0f);
    }

    public void SetState(State newState)
    {
        state = newState;
        _animator.SetInteger("state", (int)state);
    }
}

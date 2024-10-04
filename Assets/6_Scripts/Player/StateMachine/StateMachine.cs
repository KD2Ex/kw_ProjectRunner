using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Type CurrentState => m_Current.State.GetType();
    
    private StateNode m_Current;
    private Dictionary<Type, StateNode> m_Nodes = new();
    private HashSet<ITransition> m_AnyTransitions = new();

    public void Update()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            ChangeState(transition.To);
        }
        
        m_Current?.State.Update();
    }
    
    public void FixedUpdate()
    {
        m_Current.State?.FixedUpdate();
    }

    public void SetState(IState state)
    {
        m_Current = m_Nodes[state.GetType()];
        m_Current.State?.Enter();
    }
    
    private void ChangeState(IState state)
    {
        /*Debug.Log("Current: " + m_Current.State);
        Debug.Log("New : " + state);*/
        if (m_Current.State == state) return;

        var prevState = m_Current.State;
        var nextState = m_Nodes[state.GetType()].State;
        
        prevState?.Exit();
        nextState?.Enter();
        
        m_Current = m_Nodes[state.GetType()];
    }
    
    private ITransition GetTransition()
    {
        foreach (var transition in m_AnyTransitions)
        {
            if (transition.Condition.Evaluate()) return transition;
        }

        foreach (var transition in m_Current.Transitions)
        {
            if (transition.Condition.Evaluate()) return transition;
        }

        return null;
    }
    
    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    public void AddAnyTransition(IState to, IPredicate condition)
    {
        m_AnyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }
    
    StateNode GetOrAddNode(IState state)
    {
        var node = m_Nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            m_Nodes.Add(state.GetType(), node);
        }

        return node;
    }

}
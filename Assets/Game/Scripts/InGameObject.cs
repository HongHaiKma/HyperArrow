using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObject : MonoBehaviour, ITakenHit
{
    public virtual void OnEnable()
    {
        AddListener();
    }

    public virtual void OnDisable()
    {
        RemoveListener();
    }

    public virtual void OnDestroy()
    {
        RemoveListener();
    }

    public virtual void AddListener()
    {

    }

    public virtual void RemoveListener()
    {

    }
}

interface ITakenHit
{

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// data drive int
/// </summary>

public enum BindType
{
    UpdateWhenChanged,
    UpdateOnActive,
    UpdateAnyWay,
}

public class DataDriver
{
    public static int InvalidIndex = -1;

    public static implicit operator bool(DataDriver dd)
    {
        return dd != null;
    }
    public DataDriver()
    {
        mActions = new List<Action>();
    }
    public void Bind(Action act, bool immediate = true)
    {
#if UNITY_EDITOR
        if (mActions.IndexOf(act) != -1)
        {
            UnityEngine.Debug.LogError("Repeated Action：" + act.Target.ToString());
            return;
        }
#endif
        mActions.Add(act);
        if (immediate)
            act();
    }
    /// <summary>
    /// force update
    /// </summary>
    public virtual void Update()
    {
        UpdateInner(false);
    }

    public void Set<T>(T data, bool trigger = true)
    {
        mLastData = mData;
        mData = data;
        bool equal = object.Equals(mLastData, mData);
        if (!trigger)
            return;
        UpdateInner(equal);
    }
    void UpdateInner(bool equal)
    {
        UpdateActions();
    }
    protected void UpdateActions()
    {
        mGarbage.Clear();

        foreach (var act in mActions)
        {
            try
            {
                act();
            }
            catch(MissingReferenceException)
            {
                //Debug.LogWarning(ex.Message);
                mGarbage.Add(act);
            }
        }

        foreach (var gar in mGarbage)
        {
            mActions.Remove(gar);
        }
    }
    protected List<Action> mActions;
    private List<Action> mGarbage = new List<Action>();
    protected object mData;
    protected object mLastData;
}
public class DataDriverObject<T> : DataDriver
{
    public T value
    {
        get
        {
            return (T)mData;
        }
        set
        {
            Set(value);
        }
    }
    public T oldValue
    {
        get
        {
            return (T)mLastData;
        }
    }
}

public class DInt : DataDriverObject<int>
{
    public bool valid
    {
        get
        {
            return value != InvalidIndex;
        }
    }
    public void Invalidate()
    {
        value = InvalidIndex;
    }
    public DInt(int initValue = -1)
    {
        mLastData = mData = initValue;
    }

    public static implicit operator int(DInt change)
    {
        return (int)change.value;
    }
    public static implicit operator uint(DInt change)
    {
        return (uint)change.value;
    }
    public override string ToString()
    {
        return value.ToString();
    }
}
public enum DListAction
{
    None,
    Add,
    Remove,
}
/// <summary>
/// data drive list
/// </summary>
public class DList<T> : DataDriverObject<List<T>>
{
    public DList()
    {
        mData = new List<T>();
        mLastData = mData;
    }
    public T curItem
    {
        get
        {
            return mCurItem;
        }
    }

    public DListAction curAction
    {
        get
        {
            return mCurAction;
        }
    }

    T mCurItem;
    DListAction mCurAction = DListAction.None;

    public void Add(T item, bool trigger = false)
    {
        mCurItem = item;
        mCurAction = DListAction.Add;
        value.Add(item);
        if (trigger)
            UpdateActions();
    }
    public void Remove(T item, bool trigger = false)
    {
        mCurItem = item;
        mCurAction = DListAction.Remove;
        value.Remove(item);
        if (trigger)
            UpdateActions();
    }

    public void RemoveAt(int index, bool trigger = false)
    {
        T item = value[index];
        mCurItem = item;
        mCurAction = DListAction.Remove;
        value.RemoveAt(index);
        if (trigger)
            UpdateActions();
    }

    public int Count
    {
        get
        {
            return value.Count;
        }
    }
    public T this[int index] 
    {
        get
        {
            return value[index];
        }
        set
        {
            this.value[index] = value;
        }
    }
    public class DString : DataDriverObject<string>
    {
    }
}
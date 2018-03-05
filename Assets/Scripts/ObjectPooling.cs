using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling<T> where T : class, new()
{
	private Stack<T> m_objectStack;
	private Action<T> m_resetAction;
	private Action<T> m_onetimeInitAction;

	public ObjectPooling (int initialBufferSize, Action<T> ResetAction = null, Action<T> OnetimeInitAction = null)
	{
		m_objectStack = new Stack<T> (initialBufferSize);
		m_resetAction = ResetAction;
		m_onetimeInitAction = OnetimeInitAction;
	}

    public void InitBuffer(int initialBufferSize)
    {
        for (int i = 0; i < initialBufferSize; i++)
        {
			//T t = null;
			m_onetimeInitAction(null);
			//Store(t);
        }
    }

    public T New ()
	{
		if (m_objectStack.Count > 0) {
			T t = m_objectStack.Pop ();
			
			if (m_resetAction != null)
				m_resetAction (t);
			
			return t;
		} else {
			T t = null;
			
			if (m_onetimeInitAction != null) {
				m_onetimeInitAction (t);
                t = m_objectStack.Pop();
            }
			
			return t;
		}
	}

	public void Store (T obj)
	{
		m_objectStack.Push (obj);
	}

	public int Count {
		get { return m_objectStack.Count; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using System.Reflection;

[assembly: AssemblyVersion("1.0.*")]
public static class MyExtension
{
	public static void SafeInvoke<T1, T2>(this Action<T1,T2> action, T1 param1, T2 param2)
	{
		if (action != null)
		{
			action.Invoke(param1, param2);
		}
	}

	public static void SafeInvoke<T>(this Action<T> action, T param)
    {
        if (action != null)
        {
            action.Invoke(param);
        }
    }

    public static void SafeInvoke(this Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke(this UnityAction action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static DateTime GetBuildDate()
    {
        System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
        System.DateTime startDate = new System.DateTime(2000, 1, 1, 0, 0, 0);
        System.TimeSpan span = new System.TimeSpan(version.Build, 0, 0, version.Revision * 2);
        return startDate.Add(span);
    }
}


using UnityEngine;
using System.Collections;
using System;
using System.Text;

public static class DateTimeUtils
{
	public static DateTime UnixEpoch = new DateTime(2017, 1, 1, 0, 0, 0);
	public static StringBuilder strBuilder = new StringBuilder();
	public static ulong ToSeconds(this DateTime date)
	{
		TimeSpan unixTimeSpan = date - UnixEpoch;
		return (ulong)unixTimeSpan.TotalSeconds;
	}

	public static DateTime ToDateTime(ulong _seconds)
	{
		return (UnixEpoch.AddSeconds(_seconds));
	}

	public static DateTime EndOfTheDay(this DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
	}

	public static DateTime BeginningOfTheDay(this DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}

	public static bool IsLeapYear(this DateTime value)
	{
		return (System.DateTime.DaysInMonth(value.Year, 2) == 29);
	}

	public static bool IsWeekend(this DateTime value)
	{
		return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
	}

	public static DateTime GetLastDayOfMonth(this DateTime dateTime)
	{
		return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
	}

	public static long TimeElapsed(this DateTime date, DateTime now)
	{
		return (now - date).Seconds;
	}

	public static string SecondsToDateTimeString(int seconds)
	{
		strBuilder.Length = 0;
		TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
		
		if(timeSpan.Days > 0)
		{
			strBuilder.AppendFormat("{0}d", timeSpan.Days);
		}
		if(timeSpan.Hours > 0)
		{
			if(strBuilder.Length > 0)
			{
				strBuilder.Append(":");
			}
			strBuilder.AppendFormat("{0}h", timeSpan.Hours);
		}
		if (timeSpan.Minutes> 0)
		{
			if (strBuilder.Length > 0)
			{
				strBuilder.Append(":");
			}
			strBuilder.AppendFormat("{0}m", timeSpan.Minutes);
		}
		if (timeSpan.Seconds > 0)
		{
			if (strBuilder.Length > 0)
			{
				strBuilder.Append(":");
			}
			strBuilder.AppendFormat("{0}s", timeSpan.Seconds);
		}

		return strBuilder.ToString();
	}
}
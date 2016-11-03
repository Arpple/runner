using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class ListExtension
{
	public static void ZipDo<T1, T2>(
		this IEnumerable<T1> list1,
		IEnumerable<T2> list2,
		Action<T1, T2> action
	)
	{
		using(IEnumerator<T1> e1 = list1.GetEnumerator())
			using(IEnumerator<T2> e2 = list2.GetEnumerator())
			{
				while(e1.MoveNext() && e2.MoveNext())
				{
					action(e1.Current, e2.Current);
				}
			}
	}
}

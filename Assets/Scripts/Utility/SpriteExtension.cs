using UnityEngine;
using System.Collections;

public static class SpriteExtension
{
	/// <summary>
	/// Moves to right using SpriteRenderer.
	/// </summary>
	/// <param name="rightTransform">Right transform.</param>
	/// <param name="leftTransform">Left transform.</param>
	public static void MoveToRight(this Transform rightTransform, Transform leftTransform, float space = 0)
	{
		SpriteRenderer leftSprite = leftTransform.GetComponent<SpriteRenderer>();
		Vector3 leftPosition = leftTransform.position;
		Vector3 leftSize = leftSprite.bounds.max - leftSprite.bounds.min;

		rightTransform.position = new Vector3(leftPosition.x + leftSize.x + space, rightTransform.position.y, rightTransform.position.z);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLadder : MonoBehaviour
{
	public Transform otherPoint;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy" && other.GetComponent<SkeletonPatroling>() != null)
        {
			other.GetComponent<SkeletonPatroling>().moveInLadder(otherPoint);
		}
	}
}

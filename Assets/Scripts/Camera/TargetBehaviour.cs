using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour
{
	private Vector3 velocity = Vector3.zero;
	private Vector3 newPos = new Vector3(0,0,0);
	public OrbitCamera2 orb;
	

	public void GoToPosSmooth(Vector3 tPos)
	{
		newPos = tPos;	
		orb.ResetDistance(newPos);
	}

	
	void Update ()
	{
		transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0.2f);
	}
}

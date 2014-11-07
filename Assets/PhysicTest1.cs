using UnityEngine;
using System.Collections;

public class PhysicTest1 : MonoBehaviour
{
		void FixedUpdate ()
		{
				if (Time.timeSinceLevelLoad >= 10) {
						rigidbody.Sleep ();
				}
		}
}

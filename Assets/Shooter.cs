using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
		public GameObject bulletPref;
		public GameObject choice;
		
		float gravity = 9.81f;
		float speed = 10;
		float rotSpeed = 0.01f;
		float theta = Mathf.Deg2Rad * 45.0f;
		LineRenderer lineRend;
		Transform _transform, markTransform;
		Vector3 markInitPos;

		void Awake ()
		{
				_transform = transform;
				lineRend = GetComponent<LineRenderer> ();
				markTransform = (Instantiate (choice, _transform.position, Quaternion.identity) as GameObject).transform;
				markInitPos = markTransform.position;
		}

		void Update ()
		{
				theta -= Input.GetAxis ("Horizontal") * rotSpeed;
				theta = Mathf.Clamp (theta, Mathf.Deg2Rad * 10f, Mathf.Deg2Rad * 80f);

				float cos = Mathf.Cos (theta);
				float sin = Mathf.Sin (theta);
				lineRend.SetPosition (1, new Vector3 (cos * 2.0f, sin * 2.0f, 0.0f));

				//鉛直方向の計算
				//ForceMode.Impulsとして力を加えたので鉛直成分は下記の計算になる
				// vf = vi + a * t の公式を利用する
				// 0 = viy + -9.81 * t は高度が最高点までの時間なので地面に落ちるまで2倍の時間がかかる事を考慮して式を変形
				float viy = sin * speed;					//VelocityInit-Y 鉛直成分の初期速度
				float lifeTime = (viy / gravity) * 2.0f;	//地面に衝突する予測時間を計算して寿命時間とする
				//水平方向計算
				float markX = cos * speed * lifeTime;		//マーカー位置を計算
				markTransform.position = markInitPos + new Vector3 (markX, 0, 0);	//マーカー移動

				if (Input.GetButtonDown ("Fire1")) {
						GameObject go = Instantiate (bulletPref, _transform.position, Quaternion.identity) as GameObject;
						//砲弾発射の初速度（ベクトル）で指定しImpulseで力を加える
						go.rigidbody.AddForce (cos * speed, sin * speed, 0.0f, ForceMode.Impulse);
						DestroyObject (go, lifeTime);
				}
		}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
	public class CharacterCrashStone : MonoBehaviour
	{
		private GameObject startingPointObject;
        public float pushForce = 10.0f; // Lực đẩy khi va chạm với hòn đá

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (startingPointObject == null)
			{
                startingPointObject = GameObject.Find("Position restart p2");

            }
			// Kiểm tra nếu va chạm với đá bay lơ lửng
			if (collision.gameObject.CompareTag("FragementedStone"))
			{
				// Lấy vị trí của game object được xác định làm điểm khởi đầu
				Vector2 startingPoint = startingPointObject.transform.position;
				transform.position = startingPoint; // Đưa nhân vật trở lại điểm khởi đầu

				// Áp dụng lực đẩy lên đá bay lơ lửng khi va chạm
				Rigidbody2D stoneRB = collision.gameObject.GetComponent<Rigidbody2D>();
                if (stoneRB != null)
				{
					Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
					stoneRB.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
				}
			}
		}


	}
}

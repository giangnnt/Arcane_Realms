using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class CharacterMoveBox: Singleton<CharacterMoveBox>
    {
        public float pushDistance = 1.0f; // Khoảng cách di chuyển khi đẩy
        public LayerMask boxLayer; // Layer của các hộp
        public LayerMask obstacleLayer; // Layer của các rào cản
        public Tilemap tilemap; // Reference đến Tilemap

        private Vector2 pushDirection; // Hướng đẩy

        private void Update()
        {
             // Lấy input di chuyển từ Player Controller
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            pushDirection = new Vector2(horizontalInput, verticalInput).normalized;

            //Xử lý đẩy hộp khi nhấn Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("KeySpace Start");
                TryPushBox();
            }
        }

        public void TryPushBox()
        {
            Debug.Log("Try PushBox Start");
            Debug.Log("PUSDIRECTION: " + pushDirection);
            // Xác định vị trí của hộp tiếp theo dựa trên hướng đẩy
            Vector3Int currentCellOfCharacter = tilemap.WorldToCell(transform.position);
            Vector3Int targetCellOfCharacter = currentCellOfCharacter + new Vector3Int(Mathf.RoundToInt(pushDirection.x), Mathf.RoundToInt(pushDirection.y), 0);
            Vector3 targetPositionOfCharacter = tilemap.GetCellCenterWorld(targetCellOfCharacter);

            Debug.Log("CurrentCellOfCharacter: " + currentCellOfCharacter);
            Debug.Log("TargetCellOfCharacter: " + targetCellOfCharacter); 
            Debug.Log("TargetPositionOfCharacter: " + targetPositionOfCharacter);

            // Kiểm tra xem có hộp ở vị trí nhân vật muốn chiếm đứng không
            Collider2D boxCollider = Physics2D.OverlapPoint(targetPositionOfCharacter, boxLayer);
            
            if (boxCollider != null)
            {
                Debug.Log("Box: " + boxCollider.transform.position);
                Vector3Int currentCellOfBox = tilemap.WorldToCell(boxCollider.transform.position);
                Vector3Int targetCellOfBox = currentCellOfBox + new Vector3Int(Mathf.RoundToInt(pushDirection.x), Mathf.RoundToInt(pushDirection.y), 0);
                Vector3 targetPositionOfBox = tilemap.GetCellCenterWorld(targetCellOfBox);
                
                Debug.Log("CurrentCellOfBox: " + currentCellOfBox);
                Debug.Log("TargetCellOfBox: " + targetCellOfBox);
                Debug.Log("TargetPositionOfBox: " + targetPositionOfBox);

                Collider2D obstacleCollider = Physics2D.OverlapPoint(targetPositionOfBox, obstacleLayer);

                if (obstacleCollider == null)
                {
                    Debug.Log("ObstacleCollider is null");

                    // Di chuyển hộp đến vị trí mới nếu không có rào cản phía sau
                    boxCollider.transform.position = targetPositionOfBox;
                }
            }
        }


    }
}

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
    public class CharacterMoveBox : Singleton<CharacterMoveBox>
    {
        public LayerMask boxLayer; // Layer của các hộp
        public LayerMask obstacleLayer; // Layer của các rào cản
        private Tilemap tilemap; // Reference đến Tilemap
       
        public void TryPushBox(Vector2 pushDirection)
        {
            if(tilemap == null)
            {
                   tilemap = GameObject.Find("SokobanGround").GetComponent<Tilemap>();
            }
            Debug.Log("PUSDIRECTION: " + pushDirection);
            Debug.Log("Current character position: " + PlayerController.Instance.transform.position);
            Vector3Int currentCellOfCharacter = tilemap.WorldToCell(PlayerController.Instance.transform.position);
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
                }else
                {
                    Debug.Log("ObstacleCollider is not null so can not move");
                }
            }
            else
            {
                Debug.Log("Box collider is null");
            }

        }


    }
}

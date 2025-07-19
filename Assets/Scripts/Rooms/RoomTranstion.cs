using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private GameObject roomExitBlockPrefab;
    [SerializeField] private Transform roomExitBlockPositon;
    private GameObject roomExitBlock;
    private RoomManager currentRoom;
    private bool open;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        currentRoom = GetComponentInParent<RoomManager>();
        currentRoom.ActivateRoom();
        roomExitBlock = Instantiate(roomExitBlockPrefab, roomExitBlockPositon.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
    }

    public void SetOpen(bool open)
    {
        if (open)
        {
            Destroy(roomExitBlock);
            Destroy(this.gameObject);
        }
    }
}

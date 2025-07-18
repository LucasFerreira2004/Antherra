using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private RoomManager currentRoom;
    private bool open;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        currentRoom = GetComponentInParent<RoomManager>();
        currentRoom.ActivateRoom();
    }

    public void SetOpen(bool open)
    {
        if (!open)
        {
            return;
        }
    }
}

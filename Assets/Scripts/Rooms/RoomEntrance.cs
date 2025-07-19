using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
public enum EntranceDirection
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}
public class RoomEntrance : MonoBehaviour, IRoomEntrance
{
    [SerializeField] private GameObject roomExitBlockPrefab;
    [SerializeField] private Transform editedBlockPosition;
    [SerializeField] private EntranceDirection entranceDirection;

    private GameObject roomExitBlock;
    private RoomManager currentRoom;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        currentRoom = GetComponentInParent<RoomManager>();
        currentRoom.ActivateRoom();
        GetComponent<Collider2D>().enabled = false;
    }

    public void CreateBlock()
    {
        if (editedBlockPosition != null)
        {
            roomExitBlock = Instantiate(roomExitBlockPrefab, editedBlockPosition.position, Quaternion.identity);
            return;
        }
        Vector3 blockPosition = GenerateAutomaticBlockPosition();
        roomExitBlock = Instantiate(roomExitBlockPrefab, blockPosition, Quaternion.identity);
    }

    private Vector3 GenerateAutomaticBlockPosition()
    {
        switch (entranceDirection)
        {
            case EntranceDirection.TOP:
                return transform.position + new Vector3(0, 2, 0);
            case EntranceDirection.BOTTOM:
                return transform.position + new Vector3(0, -2, 0);
            case EntranceDirection.LEFT:
                return transform.position + new Vector3(-2, 0, 0);
            case EntranceDirection.RIGHT:
                return transform.position + new Vector3(2, 0, 0);
        }
        Debug.LogError("Incompatible EntranceDirection type");
        return transform.position;
    }
    public void SetOpen(bool open)
    {
        if (open)
        {
            Destroy(roomExitBlock);
            Destroy(this.gameObject);
        }
        if (roomExitBlock == null)
        {
            GetComponent<Collider2D>().enabled = false;
            CreateBlock();
        }
    }
}

using UnityEngine;

public interface IRoomEntrance
{
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void CreateBlock();
    public abstract void SetOpen(bool open);
}
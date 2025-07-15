using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Factories/BaseStatusFactory")]
public class BaseStatusFactory : ScriptableObject
{
    [System.Serializable] private struct ModeToStatus
    {
        public PlayerMode mode;
        public BaseStatusStrategy statusStrategy;
    }

    [SerializeField] private ModeToStatus[] ModesToStatus;

    private Dictionary<PlayerMode, BaseStatusStrategy> ModesToStatusMap;

    private void OnEnable()
    {
        Debug.Log("factory foi criada ---------------------------");
        ModesToStatusMap = new Dictionary<PlayerMode, BaseStatusStrategy>();
        foreach (var map in ModesToStatus)
            ModesToStatusMap[map.mode] = map.statusStrategy;
    }

    public BaseStatusStrategy GetBaseStatus(PlayerMode mode)
    {
        if (ModesToStatusMap.TryGetValue(mode, out var status))
            return status;
        throw new System.Exception("PlayerMode não encontrado: " + mode);
    }
}

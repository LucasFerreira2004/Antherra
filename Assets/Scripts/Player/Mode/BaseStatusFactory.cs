using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Factories/BaseStatusFactory")]
public class BaseStatusFactory : ScriptableObject
{
    [System.Serializable] private struct ModeToStatus
    {
        public PlayerMode mode;
        public IBaseStatusStrategy statusStrategy;
    }

    [SerializeField] private ModeToStatus[] ModesToStatus;

    private Dictionary<PlayerMode, IBaseStatusStrategy> ModesToStatusMap;

    private void OnEnable()
    {
        ModesToStatusMap = new Dictionary<PlayerMode, IBaseStatusStrategy>();
        foreach (var map in ModesToStatus)
            ModesToStatusMap[map.mode] = map.statusStrategy;
    }

    public IBaseStatusStrategy GetBaseStatus(PlayerMode mode)
    {
        if (ModesToStatusMap.TryGetValue(mode, out var status))
            return status;
        throw new System.Exception("PlayerMode não encontrado: " + mode);
    }
}

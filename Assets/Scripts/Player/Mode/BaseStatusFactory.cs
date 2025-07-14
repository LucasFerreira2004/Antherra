using UnityEngine;

public class BaseStatusFactory : ScriptableObject
{
    public static BaseStatusStrategy GetBaseStatus(PlayerMode playerMode) {
        switch (playerMode)
        {
            case PlayerMode.standart:
                return new StandardBaseStatus();
            case PlayerMode.sniper:
                return new StandardBaseStatus();
            case PlayerMode.closeRange:
                return new StandardBaseStatus();
            default:
                throw new System.Exception("playerModeNaoPermitido");
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
public class HealtHeart : MonoBehaviour
{
    [SerializeField] Sprite fullHeart, halfHeart, emptyHeart;
    Image heartImage;

    public void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void setHeartImage(HeartStatus status) {
        switch (status)
        {
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
        }
    }

    //getter e setter
    public Sprite FullHeart
    {
        get => fullHeart;
        set => fullHeart = value;
    }
    public Sprite HalfHeart
    {
        get => halfHeart;
        set => halfHeart = value;
    }
    public Sprite EmptyHeart
    {
        get => emptyHeart;
        set => EmptyHeart = value;
    }
}

public enum HeartStatus
{
    Empty,
    Half,
    Full
}

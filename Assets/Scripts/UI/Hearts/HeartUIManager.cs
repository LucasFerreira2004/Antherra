using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// se refere ao containner de hears
public class HeartUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus healthStatus;
    [SerializeField] private GameObject heartPrefab;
    List<HealtHeart> hearts = new List<HealtHeart>();


    void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDamaged += DrawHearts;
        PlayerStatus.OnPlayerHealthIncreased += DrawHearts;
        PlayerStatus.OnPlayerHealed += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerDamaged -= DrawHearts;
        PlayerStatus.OnPlayerHealthIncreased -= DrawHearts;
        PlayerStatus.OnPlayerHealed -= DrawHearts;

    }


    public void DrawHearts()
    {
        ClearHearts();
        CreateInitialEmptyHearts();
        CreateHealthHearts();
    }

    private void CreateInitialEmptyHearts()
    {
        if (healthStatus == null) return;
        int maxHealth = healthStatus.MaxHealth;
        float maxHealthRemainder = maxHealth % 2;
        int heartsToMake = (int)(maxHealth / 2 + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
            CreateEmptyHeart();
    }

    private void CreateHealthHearts()
    {
        if (healthStatus == null) return;
        int currentHealth = healthStatus.CurrentHealth;
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = Mathf.Clamp(currentHealth - (i * 2), 0, 2);
            hearts[i].setHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(this.gameObject.transform);
        HealtHeart heartScript = newHeart.GetComponent<HealtHeart>();
        heartScript.setHeartImage(HeartStatus.Empty);
        hearts.Add(heartScript);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealtHeart>();
    }
}

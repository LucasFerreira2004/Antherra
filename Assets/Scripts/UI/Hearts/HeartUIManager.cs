using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// se refere ao containner de hears
public class HeartUIManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthStatus healthStatus; //pq na unity eu tive que colocar o prefab de player e não o arquivo de script?
    [SerializeField] private GameObject heartPrefab;
    List<HealtHeart> hearts = new List<HealtHeart>(); //pq e uma lista de scripts e nao de game object?


    void Start()
    {
        DrawHearts();
    }

    void Update()
    {
        DrawHearts();
    }
    private void OnEnable()
    {
        PlayerHealthStatus.OnPlayerDamaged += DrawHearts;
    }

    private void OnDesable()
    {
        PlayerHealthStatus.OnPlayerDamaged -= DrawHearts;
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
        foreach (Transform t in transform) //ver como isso funciona
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealtHeart>();
    }
}

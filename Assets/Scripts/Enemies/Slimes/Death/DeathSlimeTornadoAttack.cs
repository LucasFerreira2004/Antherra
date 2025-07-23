using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Attack/DeathSlime/Tornado")]
public class DeathSlimeTornadoAttack : SlimeAttackStrategy
{
    public GameObject tornadoPrefab;
    public float tornadoSpacing = 1f;
    public int tornadoCount = 3;
    public float tornadoLifetime = 2f;

    public override void Attack(SlimeScript context)
    {
        if (context.Player == null) return;
        if (context.State != SlimeState.Attacking) return;

        Vector2 dirToPlayer = (context.Player.position - context.FirePoint.position).normalized;

        // Calcula a direção perpendicular (rotaciona 90 graus no sentido horário)
        Vector2 perpendicular = new Vector2(-dirToPlayer.y, dirToPlayer.x).normalized;

        // Ponto central onde os tornados devem aparecer (alguns passos à frente do slime)
        Vector2 center = (Vector2)context.FirePoint.position + dirToPlayer * 1.5f;

        int half = tornadoCount / 2;

        for (int i = 0; i < tornadoCount; i++)
        {
            int offsetIndex = i - half;
            Vector2 spawnOffset = perpendicular * tornadoSpacing * offsetIndex;
            Vector2 spawnPos = center + spawnOffset;

            GameObject tornado = GameObject.Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);

            GameObject.Destroy(tornado, tornadoLifetime);
        }

        context.SetState(SlimeState.Moving);
    }
}

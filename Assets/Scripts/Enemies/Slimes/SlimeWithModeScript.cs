using System.Collections.Generic;
using UnityEngine;

public class SlimeWithModeScript : SlimeScript
{
    [Header("Modo de Ataques")]
    [SerializeField] private List<SlimeAttackStrategy> attackModes;
    private int currentAttackIndex = 0;

    protected override void Start()
    {
        base.Start();
        if (attackModes != null && attackModes.Count > 0)
        {
            attackStrategy = attackModes[currentAttackIndex];
        }
    }

    protected override void Update()
    {
        movementStrategy?.Move(this);

        if (State == SlimeState.Attacking)
        {
            attackStrategy?.Attack(this);
            NextAttackMode();
        }

        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            timer = 0f;

            // Ativa o trigger de animação
            Animator?.SetTrigger("Attack");
        }
    }

    public void NextAttackMode()
    {
        if (attackModes == null || attackModes.Count == 0) return;

        currentAttackIndex = (currentAttackIndex + 1) % attackModes.Count;
        attackStrategy = attackModes[currentAttackIndex];
    }
}

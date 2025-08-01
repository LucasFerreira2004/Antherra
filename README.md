#  Antherra

**Antherra** é um jogo rogue-like top-down desenvolvido na Unity, inspirado por títulos como *The Binding of Isaac*, *Enter the Gungeon* e *Soul Knight*. O jogo coloca o jogador no controle de uma formiga guerreira enfrentando outras formigas rebeldes em arenas geradas proceduralmente.

## 🎮 Gameplay

- Combate em tempo real com disparos em todas as direções.
- Power-ups aleatórios com efeitos positivos ou negativos.
- Atributos do jogador influenciados dinamicamente:
  - Saúde
  - Velocidade
  - Cadência de tiro
  - Dano
  - Velocidade do tiro
  - Sorte

## ⚙️ Características técnicas

- Desenvolvido com Unity.
- Sistema de salas interconectadas com transições suaves.
- Utilização de `ScriptableObjects` para gerenciamento de atributos, power-ups e inimigos.
- UI com `TextMeshPro` para exibição de status e descrições de itens.
- Tilemaps com colisão personalizada via `Custom Physics Shape`.

## 🧠 Estrutura do projeto

- `Scripts/Player`: movimentação, disparo e controle de status.
- `Scripts/Enemies`: IA dos inimigos e interação com o jogador.
- `Scripts/Items`: lógica dos power-ups e efeitos.
- `Scripts/Rooms`: geração de salas, conexão e spawn de itens/inimigos.
- `ScriptableObjects`: definições de power-ups, atributos e inimigos.
- `Prefabs`: player, inimigos, balas, itens e salas.

## Capturas da gameplay
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/cc6ac732-c083-4ad0-a78f-ff81e3837fba" />
<img width="1920" height="1080" alt="Captura de tela de 2025-07-31 21-18-46" src="https://github.com/user-attachments/assets/bcbfcdf1-6c1d-449a-9083-88878e64dd17" />
<img width="1920" height="1080" alt="Captura de tela de 2025-07-31 21-18-13" src="https://github.com/user-attachments/assets/162cb853-ab11-42b2-8de4-b910386e2ee7" />

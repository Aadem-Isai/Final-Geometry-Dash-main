Geometry Dash Project

Unity Play Link: https://play.unity.com/en/games/b8496733-ba97-4bda-b93e-78754314937e/geometry-dash


Game Description

Geometry Dash is a game where players control a cube that automatically moves forward through obstacle-filled levels. The goal is to guide the player by timing jumps perfectly with the spacebar to avoid spikes and reach the finish line. Players must complete each level without hitting obstacles, with a progress bar showing their current position through the level. The game uses a one-hit death system: any collision with an obstacle triggers a particle explosion and respawns the player at the start position. Reaching the end of the level triggers a completion particle effect. The progress bar adds tension as players advance, and visually satisfying particle effects provide feedback for both deaths and level completion.

Technical Implementation

The game uses multiple sprites for the player cube, obstacles such as spikes, and portals. Frequently spawned objects are created as prefabs, including the player with movement and particle references, as well as obstacles. The movement system allows the player to move automatically using transform.position updates for horizontal motion and Rigidbody2D impulses for jumping. Ground checks prevent mid-air jumps, and portals can modify the player’s speed, gravity, or game mode.

Physics and collision are implemented using Rigidbody2D for realistic falling and impulses. Physical colliders handle ground and platform interactions, while trigger colliders detect hazards and portals. Ground detection uses Physics2D.OverlapCircle with configurable radius and layer mask to ensure proper jump mechanics.

The particle system provides feedback for key events. Death particles trigger an explosion effect when the player collides with an obstacle, and completion particles occur once when the player reaches the end of the level. All particle effects are properly cleaned up after use to maintain performance.

The UI consists of a progress bar that updates dynamically to show the player’s position through the level. GameObjects are dynamically spawned and destroyed, including the player at the start or upon respawn and any particle effects. Level completion is automatically detected when the player reaches 100% progress, triggering the completion particle effect.

Future Development Plan

Future levels will introduce moving obstacles and additional hazards to increase difficulty. Additional mechanics may include vehicle transformations (cube turns into a ship or wave), and gravity zones. Visual themes may also expand to include neon or original colored worlds each with unique obstacle designs and particle effects.

Development Reflection

Implementing the particle system for both deaths and level completion was the most challenging aspect of this project. It required me to spend nonstop time on getting it fixed. Restarting after player death, particle spawning, and losing my inspector midway were all major pains for me. Unity’s prefab system helped with obstacle placement and level design (in progress). Particle systems make the game feel realer and exciting to play, making deaths and level completions visually satisfying. Additionally, keeping track of GameObjects played a big role because I would jamble through the inspector and realized I gave the wrong objects the wrong script.


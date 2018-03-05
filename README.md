# kixeye_unity_challenge
KIXEYE Unity/C# Coding Challenge: 2D side-scrolling infinite "roller" game
# Gameplay
- A 2D side-scrolling game.
- Player can run and jump to avoid the obstacles.
- Player gets 10 points after jumping over an obstacle.
- Player dies when colliding with an obstacle.
- Player score could be updated to a REST API Service.
- Player can replay many times.
# Leaderboard service
For now, it is assume that the leaderboard service doesn't exist yet. Therefore, I send a update score POST request to a sample url : http://sample.com/leaderboard.
Besides, I using GameSparks as a back-end service to update and manage leaderboard.
For more information, take a look at LeaderboardService.cs

# Techniques used in project
- Object pooling for spawning new obstacles.
- UV moving for background moving effect.
- Finite State Machine for controlling game state.

# Additional Plugins
- Lean Touch for input detection.
- GameSparks for leaderboard service.

# Art sources:
- https://opengameart.org/
- https://www.deviantart.com/

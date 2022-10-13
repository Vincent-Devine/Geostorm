using System.Collections.Generic;

namespace Geostorm.Core
{
    public class GameData : IGameEventListener
    {
        // Accessors
        public IEnumerable<Entity> Entities { get { return entities; } }
        public IEnumerable<Enemy> Enemies { get { return enemies; } }
        public IEnumerable<Bullet> Bullets { get { return bullets; } }
        public IEnumerable<BlackHole> BlackHoles { get { return blackHoles; } }
        public Player Player { get { return player; } }


        private List<Entity> entities = new List<Entity>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Bullet> bullets = new List<Bullet>();
        private List<BlackHole> blackHoles = new List<BlackHole>();
        private Player player = new Player(new System.Numerics.Vector2());

        private List<Enemy> enemiesAdded = new List<Enemy>();
        private List<Bullet> bulletsAdded = new List<Bullet>();
        private List<BlackHole> blackHolesAdded = new List<BlackHole>();

        public int level;
        public int newLevelTimer;
        public int timer;

        public GameData(GameInputs inputs, int newLevelTimer = 300)
        {
            player = new Player(inputs.screenSize/2);
            entities.Add(player);
            
            this.level = 0;
            this.newLevelTimer = newLevelTimer;
            this.timer = 0;
        }

        public void Update()
        {
            timer++;
            Synchnonize(); 
        }

        public void AddEnemyDelayed(Enemy enemy)
        {
            enemiesAdded.Add(enemy);
        }

        public void AddBulletDelayed(Bullet bullet)
        {
            bulletsAdded.Add(bullet);
        }

        public void AddBlackHoleDelayed(BlackHole blackHole)
        {
            blackHolesAdded.Add(blackHole);
        }

        private void Synchnonize()
        {
            // Enemy
            enemies.AddRange(enemiesAdded);
            entities.AddRange(enemiesAdded);
            enemiesAdded.Clear();


            // Bullet
            bullets.AddRange(bulletsAdded);
            entities.AddRange(bulletsAdded);
            bulletsAdded.Clear();

            // BlackHole
            blackHoles.AddRange(blackHolesAdded);
            entities.AddRange(blackHolesAdded);
            blackHolesAdded.Clear();
            
            // Delete dead entity
            entities.RemoveAll(entity => entity.isDead);
            enemies.RemoveAll(entity => entity.isDead);
            bullets.RemoveAll(entity => entity.isDead);
            blackHoles.RemoveAll(entity => entity.isDead);
        }

        public void HandleEvents(IEnumerable<GameEvent> gameEvents, GameData gameData)
        {
            foreach (GameEvent gameEvent in gameEvents)
            {
                switch (gameEvent)
                {
                    case EnemyKilledEvent:
                        EnemyKilledEvent enemyKilledEvent = gameEvent as EnemyKilledEvent;
                        Player.EntityKilled(enemyKilledEvent.enemy.scoreWin);
                        break;

                    case PlayerTakeDamageEvent:
                        PlayerTakeDamageEvent playerDamageEvent = gameEvent as PlayerTakeDamageEvent;
                        Player.TakeDamage(playerDamageEvent.damage);
                        break;
                }
            }
        }
    }
}

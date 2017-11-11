using System;
using System.Collections.Generic;
using System.Windows;

public class GameController
{
    public List<Entity> Enemies;
    public List<Entity> PlayerBullets;
    public List<Entity> EnemyBullets;
    public List<Entity> PowerUps;

    public int Level;
    public int Speed;
    public int Score;

    public Player Player { get; internal set; }
    public List<Entity> QueEnemies { get; internal set; }

    internal void Save(string v)
    {
        throw new NotImplementedException();
    }

    internal void Load(string v)
    {
        throw new NotImplementedException();
    }
}

public class Entity
{

}

public class Asteroid : Entity
{
    public Asteroid(Point x)
    {

    }
}

public class Player : Entity
{
    public Player(string name, Point x)
    {

    }

    public string Name { get; internal set; }
    public string X { get; internal set; }
    public string Y { get; internal set; }
}

public class Bullet : Entity
{
    public Bullet(Point x)
    {

    }
}

public class PowerUp : Entity
{
    public PowerUp(Point x, string type) // make enum
    {

    }
}
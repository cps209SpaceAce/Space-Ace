using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Windows;

[TestClass]
public class LoadSaveTests
{
    [TestMethod]
    public void SaveListofEnemies_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> enemies = new List<Entity>();
        enemies.Add(new Asteroid(new Point(30, 20)));
        enemies.Add(new Asteroid(new Point(10, 10)));

        ctrl.Enemies = enemies;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");             //default, gamecontroller always has a player
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[enemies]");
        Assert.IsTrue(reader.ReadLine() == "asteroid,100,30,20");
        Assert.IsTrue(reader.ReadLine() == "asteroid,100,10,10");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }

    [TestMethod]
    public void SaveListofQueEnemies_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> queEnemies = new List<Entity>();
        queEnemies.Add(new Asteroid(new Point(30, 20)));
        queEnemies.Add(new Asteroid(new Point(10, 10)));

        ctrl.QueEnemies = queEnemies;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[queuedEnemies]");
        Assert.IsTrue(reader.ReadLine() == "asteroid,100,30,20");
        Assert.IsTrue(reader.ReadLine() == "asteroid,100,10,10");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }

    [TestMethod]
    public void PlayerBullets_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> playerBullets = new List<Entity>();
        playerBullets.Add(new Bullet(new Point(30, 20)));

        ctrl.PlayerBullets = playerBullets;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[playerBullets]");
        Assert.IsTrue(reader.ReadLine() == "30,20");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }

    [TestMethod]
    public void PowerUp_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> powerUp = new List<Entity>();
        powerUp.Add(new PowerUp(new Point(30, 20), "invisiblast"));

        ctrl.PowerUps = powerUp;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[powerUps]");
        Assert.IsTrue(reader.ReadLine() == "invisiblast,30,20");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }

    [TestMethod]
    public void EnemyBullets_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> enemyBullets = new List<Entity>();
        enemyBullets.Add(new Bullet(new Point(30, 20)));

        ctrl.EnemyBullets = enemyBullets;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[enemyBullets]");
        Assert.IsTrue(reader.ReadLine() == "30,20");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }


    [TestMethod]
    public void PlayerData_Success() 
    {
        GameController ctrl = new GameController();

        ctrl.Player = new Player("Jojo", new Point(50,50));

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "Jojo,100,50,50,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }


    [TestMethod]
    public void GameControllerData_Success()
    {
        GameController ctrl = new GameController();
        ctrl.Level = 5;
        ctrl.Speed = 100;
        ctrl.Score = 9001;

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[defaults]");
        Assert.IsTrue(reader.ReadLine() == "5,100,9001");
        Assert.IsTrue(reader.ReadLine() == "[end]");

    }

    [TestMethod]
    public void Default_GameController_Success() 
    {
        GameController ctrl = new GameController();

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[defaults]");
        Assert.IsTrue(reader.ReadLine() == "0,10,0"); //default speed here.
        Assert.IsTrue(reader.ReadLine() == "[end]");

    }

    [TestMethod]
    public void Load_Player_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[player]");
        writer.WriteLine("Jojo,100,40,50,none");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.Player.Name == "Jojo");
        Assert.IsTrue(ctrl.Player.X == "40");
        Assert.IsTrue(ctrl.Player.Y == "50");
    }

    [TestMethod]
    public void Load_Enemies_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[enemies]");
        writer.WriteLine("asteroid,100,40,50");
        writer.WriteLine("asteroid,100,30,20");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.Enemies.Count == 2);
    }

    [TestMethod]
    public void Load_QuedEnemies_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[queuedEnemies]");
        writer.WriteLine("asteroid,100,40,50");
        writer.WriteLine("asteroid,100,30,20");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.QueEnemies.Count == 2);
    }

    [TestMethod]
    public void Load_EnemyBullets_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[enemyBullets]");
        writer.WriteLine("40,50");
        writer.WriteLine("30,20");
        writer.WriteLine("10,5");
        writer.WriteLine("3,2");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.EnemyBullets.Count == 4);
    }

    [TestMethod]
    public void Load_PlayerBullets_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[playerBullets]");
        writer.WriteLine("40,50");
        writer.WriteLine("30,20");
        writer.WriteLine("10,5");
        writer.WriteLine("3,2");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.PlayerBullets.Count == 4);
    }

    [TestMethod]
    public void Load_Defaults_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[defaults]");
        writer.WriteLine("2,10,1337");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.Speed == 10);
        Assert.IsTrue(ctrl.Score == 1337);
        Assert.IsTrue(ctrl.Level == 2);
    }

    [TestMethod]
    public void Load_PowerUp_Success() 
    {
        GameController ctrl = new GameController();

        StreamWriter writer = new StreamWriter("SaveFile.txt");

        writer.WriteLine("[powerUp]");
        writer.WriteLine("invisiblast,30,20");
        writer.WriteLine("porcupine,4,3");
        writer.WriteLine("dragon,0,0");
        writer.WriteLine("[end]");

        ctrl.Load("SaveFile.txt");

        Assert.IsTrue(ctrl.PowerUps.Count == 3);
    }
}



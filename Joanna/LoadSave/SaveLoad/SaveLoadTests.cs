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

        SaveManager save = new SaveManager();
        save.Save();

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

        ctrl.Enemies = queEnemies;

        SaveManager save = new SaveManager();
        save.Save();

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

        SaveManager save = new SaveManager();
        save.Save();

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

        SaveManager save = new SaveManager();
        save.Save();

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

        SaveManager save = new SaveManager();
        save.Save();

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[enemyBullets]");
        Assert.IsTrue(reader.ReadLine() == "30,20");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }


    [TestMethod]
    public void PlayerData_Success() //todo
    {
        GameController ctrl = new GameController();

        ctrl.Player = new Player("Jojo", new Point(50,50));

        SaveManager save = new SaveManager();
        save.Save();

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

        SaveManager save = new SaveManager();
        save.Save();

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[defaults]");
        Assert.IsTrue(reader.ReadLine() == "5,100,9001");
        Assert.IsTrue(reader.ReadLine() == "[end]");

    }

    [TestMethod]
    public void Default_GameController_Success() //todo
    {
        GameController ctrl = new GameController();

        SaveManager save = new SaveManager();
        save.Save();

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "unnamed,100,0,0,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");

        Assert.IsTrue(reader.ReadLine() == "[defaults]");
        Assert.IsTrue(reader.ReadLine() == "0,10,0"); //default speed here.
        Assert.IsTrue(reader.ReadLine() == "[end]");

    }
}



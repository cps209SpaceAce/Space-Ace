using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
using Model;

[TestClass]
public class LoadSaveTests
{
    [TestMethod]
    public void Save_CurrentEnemies_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> enemies = new List<Entity>();
        enemies.Add(new Asteroid(new Point(30, 20)));
        enemies.Add(new Asteroid(new Point(10, 10)));

        ctrl.current_Enemies = enemies;

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
    public void Save_QueEnemies_Success()
    {
        GameController ctrl = new GameController();
        Entity[,] queEnemies = new Entity[2, 2];
        queEnemies[0,0] = new Asteroid(new Point(30, 20));
        queEnemies[1, 0] = new Asteroid(new Point(10, 10));


        ctrl.enemie_Que = queEnemies;

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
    public void Save_PlayerBullets_Success()
    {
        GameController ctrl = new GameController();
        List<Bullet> playerBullets = new List<Bullet>();
        playerBullets.Add(new Bullet(new Point(30, 20)));

        ctrl.player_fire = playerBullets;

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
    public void Save_PowerUp_Success()
    {
        GameController ctrl = new GameController();
        List<Entity> powerUp = new List<Entity>();
        powerUp.Add(new Powerup(new Point(30, 20), "invisiblast"));

        ctrl.current_Enemies = powerUp;

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
    public void Save_PlayerData_Success() 
    {
        GameController ctrl = new GameController();

        //ctrl.player = new Player(new Point(50,50));

        ctrl.Save("SaveFile.txt");

        StreamReader reader = new StreamReader("SaveFile.txt");

        Assert.IsTrue(reader.ReadLine() == "[player]");
        Assert.IsTrue(reader.ReadLine() == "Jojo,100,50,50,none");
        Assert.IsTrue(reader.ReadLine() == "[end]");
    }


    [TestMethod]
    public void Save_GameControllerData_Success()
    {
        GameController ctrl = new GameController();
        ctrl.level = levels.Level_1;
        ctrl.base_Speed = 100;
        ctrl.current_score = 9001;

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
    public void Save_Default_GameController_Success() 
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

        //Assert.IsTrue(ctrl.Player.X == "40");
        //Assert.IsTrue(ctrl.Player.Y == "50");
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

        Assert.IsTrue(ctrl.current_Enemies.Count == 2);
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

        Assert.IsTrue(ctrl.enemie_Que[0,0] == new Asteroid(new Point(40,50)));
        Assert.IsTrue(ctrl.enemie_Que[1, 0] == new Asteroid(new Point(30, 20)));

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

        Assert.IsTrue(ctrl.player_fire.Count == 4);
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

        Assert.IsTrue(ctrl.base_Speed == 10);
        Assert.IsTrue(ctrl.current_score == 1337);
        Assert.IsTrue(ctrl.level == levels.Level_2);
    }
}



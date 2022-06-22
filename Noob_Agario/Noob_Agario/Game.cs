using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    internal class Game
    {
        public RenderWindow window;

        public static Game instance;
        public Game()
        {
            instance = this;
        }

        private static int maxPlayers = 10;
        private static int maxFood = 25;

        private int currentPlayerCount = maxPlayers;

        public List<Player> players = new List<Player>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Food> foods = new List<Food>();
        public List<Shape> shapesToDraw = new List<Shape>();

        public List<Text> playersNamesTexts = new List<Text>();

        private Random rnd = new Random();

        private int defaultWindowX = 1600;
        private int defaultWindowY = 900;
        public void Play()
        {
            (uint windowX, uint windowY) = GetSavedResolution();
            window = new RenderWindow(new VideoMode(windowX, windowY), "Game window");

            window.Closed += WindowClosed;
            window.SetFramerateLimit(60);

            ObjectCreator.GetWindow(window);
            RandomGenerator.GetWindow(window);

            CreatePlayers();
            CreateTexts();
            CreateFood();
            
            while (window.IsOpen || currentPlayerCount != 1)
            {
                window.DispatchEvents();
                window.Clear();

                PlayersLogic();
                BulletsLogic();
                DrawObjects();

                window.Display();
            }
        }

        private (uint, uint) GetSavedResolution()
        {
            var myIni = new SavingSystem("config.ini");
            if (!myIni.KeyExists("WindowResolutionX", "Window"))
            {
                myIni.Write("WindowResolutionX", defaultWindowX.ToString(), "Window");
            }
            if (!myIni.KeyExists("WindowResolutionY", "Window"))
            {
                myIni.Write("WindowResolutionY", defaultWindowY.ToString(), "Window");
            }
            uint windowX = uint.Parse(myIni.Read("WindowResolutionX", "Window"));
            uint windowY = uint.Parse(myIni.Read("WindowResolutionY", "Window"));
            return (windowX, windowY);
        }
        private void CreatePlayers()
        {
            players.Add(ObjectCreator.CreatePlayer("you", false));
            shapesToDraw.Add(players[0].playerModel);
            for (int i = 1; i < maxPlayers; i++)
            {
                players.Add(ObjectCreator.CreatePlayer("bot", true));
                shapesToDraw.Add(players[i].playerModel);
            }
        }

        private void CreateTexts()
        {
            foreach(Player player in players)
            {
                playersNamesTexts.Add(player.name);
            }
        }

        private void CreateFood()
        {
            for (int i = 0; i < maxFood; i++)
            {
                foods.Add(ObjectCreator.CreateFood());
                shapesToDraw.Add(foods[i].foodModel);
            }
        }

        private void DrawObjects()
        {
            foreach (Shape shape in shapesToDraw)
            {
                window.Draw(shape);
            }

            foreach (Text text in playersNamesTexts)
            {
                window.Draw(text);
            }

        }

        private void PlayersLogic()
        {
            foreach(Player player in players)
            {
                player.Update(players, foods);
            }
        }

        private void BulletsLogic()
        {
            foreach (Bullet bullet in bullets.ToArray())
            {
                bullet.Update(players);
            }
        }

        public void OnPlayerEaten()
        {
            currentPlayerCount--;
        }

        public void OnPlayerShoot(Bullet bullet)
        {
            bullets.Add(bullet);
            shapesToDraw.Add(bullet.bulletModel);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
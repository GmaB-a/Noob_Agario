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

        public Player[] players = new Player[maxPlayers];
        private Food[] foods = new Food[maxFood];
        private Shape[] toDraw = new Shape[maxPlayers + maxFood];

        private Text[] playersNamesTexts = new Text[maxPlayers];

        private Random rnd = new Random();

        int defaultWindowX = 1600;
        int defaultWindowY = 900;
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
            players[0] = ObjectCreator.CreatePlayer("you", false);
            toDraw[0] = players[0].playerModel;
            for (int i = 1; i < players.Length; i++)
            {
                players[i] = ObjectCreator.CreatePlayer("bot", true);
                toDraw[i] = players[i].playerModel;
            }
        }

        private void CreateTexts()
        {
            for (int i = 0; i < playersNamesTexts.Length; i++) 
            {
                playersNamesTexts[i] = players[i].name;
            }
        }

        private void CreateFood()
        {
            for (int i = 0; i < maxFood; i++)
            {
                foods[i] = ObjectCreator.CreateFood();
                toDraw[i + players.Length] = foods[i].foodModel;
            }
        }

        private void DrawObjects()
        {
            for (int i = 0; i < maxPlayers; i++)
            {
                window.Draw(toDraw[i]);
                window.Draw(playersNamesTexts[i]);
            }

            for (int i = maxPlayers; i < toDraw.Length; i++)
            {
                window.Draw(toDraw[i]);
            }
        }

        private void PlayersLogic()
        {
            foreach(Player player in players)
            {
                player.Update(players, foods);
            }
        }

        public void OnPlayerEaten()
        {
            currentPlayerCount--;
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
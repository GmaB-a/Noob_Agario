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
        private static int maxPlayers = 10;
        private static int maxFood = 20;
        Shape[] players = new Shape[maxPlayers];
        Shape[] foods = new Shape[maxFood];
        Shape[] toDraw = new Shape[maxPlayers + maxFood];
        public void Play()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");
            window.Closed += WindowClosed;

            for (int i = 0; i < maxPlayers; i++)
            {
                Player newPlayer = ObjectCreator.getInstance().CreatePlayer(window);
                players[i] = newPlayer;
            }
            players.CopyTo(toDraw, 0);

            for (int i = 0; i < maxFood; i++)
            {
                Food newFood = ObjectCreator.getInstance().CreateFood(window);
                foods[i] = newFood;
            }
            foods.CopyTo(toDraw, players.Length);
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                for(int i = 0; i < toDraw.Length; i++)
                {
                    window.Draw(toDraw[i]);
                }
                window.Display();
            }
        }
        void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
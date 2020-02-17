﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class MultiplayerShowData : GameObject
    {
        public List<Text> texts;

        public MultiplayerShowData(string name) : base(name)
        {
            this.texts = new List<Text>();
            for (int i = 0; i < 6; i++)
            {
                Text text = new Text(this, new Vector2(0, i * 16), FontManager.Get("MainFont"), Color.Black);
                texts.Add(text);
                this.AddBehaviour(text);
            }

        }

        public override void Actualize()
        {
            base.Actualize();

            texts[0].value = "Is connected: " + MultiplayerManager.Client?.Connected;
            texts[1].value = "Is server: " + MultiplayerManager.isServer;
            texts[2].value = "Client amount: " + MultiplayerManager.ClientAmount;
            texts[3].value = "Addres: "+ MultiplayerManager.addres?.ToString();
            texts[4].value = "Port: " + MultiplayerManager.port.ToString();
            texts[5].value = "Client: " + MultiplayerManager.Client?.ToString();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class TextField : Behaviour, IDrawable
    {
        private static TextField Focus;

        public delegate void Event();

        public string exampleText;
        public string text;
        private Vector2 box;
        private SpriteFont font;
        private int size;

        private float time;

        List<Keys> lastkeys = new List<Keys>();

        public TextField(GameObject gameObject, Vector2 box,SpriteFont font, int size, string text = "", string example = "...") : base(gameObject)
        {
            this.box = box;
            this.font = font;
            this.exampleText = example;
            this.text = text;
            this.size = size;
        }

        public override void Update()
        {
            base.Update();
            Rectangle rect = new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, (int)box.X, (int)box.Y);
            if (rect.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            {
                TextField.Focus = this;
            }

            if (Focus != this)
                return;

            List<Keys> keys = new List<Keys>();
            foreach (var k in Enum.GetValues(typeof(Keys)))
            {
                if (Keyboard.GetState().IsKeyDown((Keys)k))
                { 
                    keys.Add((Keys)k);
                }
            }

            foreach (var k in keys)
            {
                if (!lastkeys.Contains(k))
                {
                    if (k.Equals(Keys.Back))
                    {
                        if(Focus.text.Length > 0)
                            Focus.text = Focus.text.Remove(Focus.text.Length - 1);
                        continue;
                    }

                    if(Focus.text.Length < size)
                        Focus.text += GetStingKey(k);
                }
            }

            lastkeys = keys;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)(gameObject.Transform.Position.X );
            int y = (int)(gameObject.Transform.Position.Y );

            Vector2 center = new Vector2( gameObject.Transform.Position.X, gameObject.Transform.Position.Y);
            Rectangle rect = new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, (int)box.X, (int)box.Y);
            Vector2 vec = new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Y);

            if (text.Equals(""))
            {
                if(Focus == this)
                {

                    if (time < .5f)
                    {
                        sb.DrawString(font, "|", vec.ToXna(), Color.Black);

                    }
                    else
                    {
                        sb.DrawString(font, "", vec.ToXna(), Color.Black);

                    }
                    if (time > 1f)
                    {
                        time = 0;
                    }

                    time += Time.DeltaTime();
                }
                else
                {
                    sb.DrawString(font, exampleText, vec.ToXna(), Color.Gray);
                }
                
            }
            else
            {
                if (Focus == this)
                {
                    if (time < .5f)
                    {
                        sb.DrawString(font, text + "|", vec.ToXna(), Color.Black);
                    }
                    else
                    {
                        sb.DrawString(font, text, vec.ToXna(), Color.Black);
                    }
                    if(time > 1f)
                    {
                        time = 0;
                    }

                    time += Time.DeltaTime();
                }
                else
                {
                    sb.DrawString(font, text, vec.ToXna(), Color.Black);
                }
               
            }
        }

        public string GetStingKey(Keys keys)
        {
            switch(keys)
            {
                case Keys.D1:
                    return "1";
                case Keys.D2:
                    return "2";
                case Keys.D3:
                    return "3";
                case Keys.D4:
                    return "4";
                case Keys.D5:
                    return "5";
                case Keys.D6:
                    return "6";
                case Keys.D7:
                    return "7";
                case Keys.D8:
                    return "8";
                case Keys.D9:
                    return "9";
                case Keys.D0:
                    return "0";
                case Keys.OemPeriod:
                    return ".";
                default:
                    return "";

            }
        }

    }
}

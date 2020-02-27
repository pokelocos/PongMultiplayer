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
    class TextField : Behaviour, IDrawable , ISelectable
    {
        public Vector2 offset = Vector2.Zero;
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

            InputManager.selectables.Add(this);
        }

        public override void Update()
        {
            base.Update();
            //Rectangle rect = new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, (int)box.X, (int)box.Y);
            //if (rect.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            //{
            //    TextField.Focus = this;
            //}

            if (InputManager.focus != this)
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
                        if(this.text.Length > 0)
                            this.text = this.text.Remove(this.text.Length - 1);
                        continue;
                    }

                    if(this.text.Length < size)
                        this.text += GetStingKey(k);
                }
            }

            lastkeys = keys;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)(gameObject.Transform.Position.X );
            int y = (int)(gameObject.Transform.Position.Y );

            Vector2 vec = new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Y) + offset;

            if (text.Equals(""))
            {
                if(InputManager.focus == this)
                {

                    if (time < .5f)
                    {
                        sb.DrawString(font, "|", vec.ToXna(), Color.White);

                    }
                    else
                    {
                        sb.DrawString(font, "", vec.ToXna(), Color.White);

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
                if (InputManager.focus == this)
                {
                    if (time < .5f)
                    {
                        sb.DrawString(font, text + "|", vec.ToXna(), Color.White);
                    }
                    else
                    {
                        sb.DrawString(font, text, vec.ToXna(), Color.White);
                    }
                    if(time > 1f)
                    {
                        time = 0;
                    }

                    time += Time.DeltaTime();
                }
                else
                {
                    sb.DrawString(font, text, vec.ToXna(), Color.White);
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

        public Rectangle GetRect()
        {
            return new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, (int)box.X, (int)box.Y);
        }

        public void OnClickDown() { }
        public void OnClickUp() { }
        public void OnMouseEnter() { }
        public void OnMouseOut() { }
    }
}

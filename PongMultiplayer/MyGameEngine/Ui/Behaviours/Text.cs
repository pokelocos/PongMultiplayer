using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class Text : Behaviour, IDrawable
    {
        public string value;
        public Color color;
        private Vector2 delta;
        private SpriteFont font;

        public Text(GameObject gameObject,Vector2 delta,SpriteFont font, Color color, string text = "Example Text") : base(gameObject)
        {
            this.value = text;
            this.color = color;
            this.delta = delta;
            this.font = font;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)(gameObject.Transform.Position.X);
            int y = (int)(gameObject.Transform.Position.Y);

            Vector2 vec = new Vector2(x + delta.X, y + delta.Y);

            sb.DrawString(font, value, vec.ToXna(), color);
        }
    }
}

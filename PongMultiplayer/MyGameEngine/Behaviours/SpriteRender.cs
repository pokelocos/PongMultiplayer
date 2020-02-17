using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class SpriteRender : Behaviour , IDrawable
    {
        private Texture2D texture;
        private Color color;
        private Vector2 size;
        private Vector2 offset;
        public Vector2 Offset { get { return offset; } set { offset = value; } }

        public SpriteRender(GameObject gameObject,Texture2D texture,Vector2 size) : base(gameObject)
        {
            this.name = "[SpriteRender]: " + gameObject.GetName();
            this.texture = texture;
            this.color = Color.White;
            this.size = size;
            this.offset = Vector2.Zero;
        }

        public void SetImage(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)(gameObject.Transform.Position.X + offset.X);
            int y = (int)(gameObject.Transform.Position.Y + offset.Y);

            Vector2 center = new Vector2((texture.Width/2),(texture.Height/2));
            sb.Draw(texture, new Rectangle(x + (int)(size.X / 2), y + (int)(size.Y / 2), (int)size.X, (int)size.Y), null, color, MathHelper.ToRadians(gameObject.Transform.Rotation2D), center.ToXna(), SpriteEffects.None, 0);
        }
    }
}

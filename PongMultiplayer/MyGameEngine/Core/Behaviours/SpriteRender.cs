﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    [System.Serializable]
    class SpriteRender : Behaviour , IDrawable
    {
        public string spriteName;
        [NonSerialized] private Texture2D sprite;
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        [NonSerialized] private Color color;
        private Vector2 size;
        private Vector2 offset;
        public Vector2 Offset { get { return offset; } set { offset = value; } }

        public SpriteRender(GameObject gameObject, string texture, Vector2 size) : base(gameObject)
        {
            this.name = "[SpriteRender]: " + gameObject.GetName();
            this.spriteName = texture;
            this.sprite = ImageManager.Get(texture);
            this.color = Color.White;
            this.size = size;
            this.offset = Vector2.Zero;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)(gameObject.Transform.Position.X + offset.X);
            int y = (int)(gameObject.Transform.Position.Y + offset.Y);

            Vector2 center = new Vector2((sprite.Width/2),(sprite.Height/2));
            sb.Draw(sprite, new Rectangle(x + (int)(size.X / 2), y + (int)(size.Y / 2), (int)size.X, (int)size.Y), null, color, MathHelper.ToRadians(gameObject.Transform.Rotation2D), center.ToXna(), SpriteEffects.None, 0);
        }
    }
}

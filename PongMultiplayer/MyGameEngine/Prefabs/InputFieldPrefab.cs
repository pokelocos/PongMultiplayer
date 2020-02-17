using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class InputFieldPrefab : GameObject
    {
        Vector2 size;
        public TextField textField;

        public InputFieldPrefab(string name, Texture2D field,SpriteFont font,Vector2 size, Vector3 position) : base(name)
        {
            this.size = size;

            this.AddBehaviour(new SpriteRender(this, field, size));
            this.textField = new TextField(this, size, font, 18, "", "...");
            this.AddBehaviour(this.textField);

            this.Transform.Position = position;
        }
    }
}

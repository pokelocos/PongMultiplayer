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

        public InputFieldPrefab(string name, string texture,SpriteFont font,Vector2 size, Vector3 position) : base(name)
        {
            this.size = size;

            this.AddBehaviour(new SpriteRender(this, texture, size));
            this.textField = new TextField(this, size, font, 18, "", "...");
            this.textField.offset = new Vector2(15,10);
            this.AddBehaviour(this.textField);

            this.Transform.Position = position;
        }
    }
}

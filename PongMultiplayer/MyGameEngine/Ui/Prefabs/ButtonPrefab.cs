using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class ButtonPrefab : GameObject
    {
        public Vector2 size;
        public Button button;

        public ButtonPrefab(string name,Texture2D texture2D,Vector2 size,Vector3 position) : base(name)
        {
            this.size = size;

            this.AddBehaviour(new SpriteRender(this,texture2D, size));
            button = new Button(this, size);
            this.AddBehaviour(button);
            this.Transform.Position = position;
        }
    }
}

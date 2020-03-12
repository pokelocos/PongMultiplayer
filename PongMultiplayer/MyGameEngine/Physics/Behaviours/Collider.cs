using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MyEngine
{
    public abstract class Collider : Behaviour , IDrawable
    {
        private Vector2 offset;
        private Vector2 position;

        private List<Collider> colliders = new List<Collider>();

        public Collider(GameObject gameObject, Vector2 offset, Vector2 position) : base(gameObject)
        {
            this.offset = offset;
            this.position = position;
        }

        public override void Start()
        {
            Physics.colliders.Add(this);
        }

        protected void Col(bool exist,Collider c)
        {
            if (exist && !colliders.Contains(c))
            {
                colliders.Add(c);
                this.gameObject.EnterCollision(c);
                foreach (Behaviour b in this.gameObject.Behaviours)
                {
                    b.EnterCollision(c);
                }
            }
            if(exist && colliders.Contains(c))
            {
                this.gameObject.StayCollision(c);
                foreach (Behaviour b in this.gameObject.Behaviours)
                {
                    b.StayCollision(c);
                }
            }
            if(!exist && colliders.Contains(c))
            {
                colliders.Remove(c);
                this.gameObject.ExitCollision(c);
                foreach (Behaviour b in this.gameObject.Behaviours)
                {
                    b.ExitCollision(c);
                }
            }
            
        }

        public abstract void IsCollider(Collider collider);

        public virtual void Draw(SpriteBatch sb) { }

        public class Rect : Collider 
        {
            private Vector2 size;
            
            public Rect(GameObject gameObject, Vector2 offset,Vector2 size) :
                base(gameObject, offset, new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Y))
            {
                this.size = size;
                this.name = "[Rect Collider]: " + gameObject.GetName();
            }

            public override void Draw(SpriteBatch sb)
            {
                if(Globals.DebugMode)
                {
                    int x = (int)(position.X);
                    int y = (int)(position.Y);
                    sb.Draw(ImageManager.textures["square"], new Rectangle(x, y, (int)size.X, (int)size.Y), new Color(255,255,255,20));
                }
            }

            public override void IsCollider(Collider other)
            {                
                if(other is Rect)
                {
                    Rect collider = (Rect)other;
                    Rectangle r1 = new Rectangle((int)(position.X), (int)(position.Y), (int)size.X, (int)size.Y);
                    //Console.WriteLine((int)(position.X + offset.X)+","+ (int)(position.Y + offset.Y) + "," + (int)size.X + "," + (int)size.Y);
                    Rectangle r2 = new Rectangle(
                        (int)(collider.position.X),
                        (int)(collider.position.Y),
                        (int)collider.size.X,(int)collider.size.Y);
                    
                    if(r1.Intersects(r2))
                    {
                        Col(true, other);
                        return;
                    }

                }

                if(other is Circle)
                {
                    Circle collider = (Circle)other;

                }

                Col(false,other);
            }

            public override void Update()
            {
                position.X = gameObject.Transform.Position.X + offset.X;
                position.Y = gameObject.Transform.Position.Y + offset.Y;
            }

        }

        public class Circle : Collider
        {
            private double radius;           

            public Circle(GameObject gameObject, Vector2 offset, double radius) :
                base(gameObject,offset, new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Y))
            {
                this.radius = radius;
                this.name = "[Circle Collider]: " + gameObject.GetName();
            }

            public override void Draw(SpriteBatch sb)
            {
                if(Globals.DebugMode)
                {
                    int x = (int)(gameObject.Transform.Position.X + offset.X);
                    int y = (int)(gameObject.Transform.Position.Y + offset.Y);
                    sb.Draw(ImageManager.textures["circle"], new Rectangle(x, y, (int)radius*2, (int)radius*2), new Color(255, 255, 255, 0));
                }
            }


            public override void IsCollider(Collider other)
            {
                if (other is Rect)
                {
                    Rect collider = (Rect)other;

                }

                if (other is Circle)
                {
                    Circle collider = (Circle)other;
                    
                    if(this.radius + collider.radius >= Vector3.Distance(gameObject.Transform.Position,collider.GameObject.Transform.Position))
                    {
                        Col(true, other);
                        return;
                    }
                }
                Col(false, other);
            }

            public override void Update()
            {
                position.X = gameObject.Transform.Position.X + offset.X;
                position.Y = gameObject.Transform.Position.Y + offset.Y;
            }
        }
    }

}

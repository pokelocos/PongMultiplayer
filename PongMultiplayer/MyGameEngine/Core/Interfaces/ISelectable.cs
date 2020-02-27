using Microsoft.Xna.Framework;
using System;
using static MyEngine.InputManager;

namespace MyEngine
{
    public interface ISelectable
    {
        Rectangle GetRect();

        void OnClickDown();
        void OnClickUp();
        void OnMouseEnter();
        void OnMouseOut();
    }
}
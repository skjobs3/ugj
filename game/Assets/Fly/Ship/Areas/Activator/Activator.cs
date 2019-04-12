using UnityEngine;

namespace Fly.Ship.Areas
{
    public class Activator : Fly.Ship.Areas.Area
    {
        public delegate void ActivateDelegate(GamePlayerController Player);
        public event ActivateDelegate ActivateEvent;

        public void Activate(GamePlayerController Player)
        {
            if (this.ActivateEvent == null)
            {
                return;
            }

            this.ActivateEvent(Player);
        }
    }
}
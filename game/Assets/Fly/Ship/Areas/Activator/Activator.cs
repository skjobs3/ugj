using UnityEngine;

namespace Fly.Ship.Areas
{
    public class Activator : Fly.Ship.Areas.Area
    {
        public delegate bool ActivateDelegate(GamePlayerController Player);
        public event ActivateDelegate ActivateEvent;

        public bool Activate(GamePlayerController Player)
        {
            if (this.ActivateEvent == null)
            {
                return false;
            }

            return this.ActivateEvent(Player);
        }
    }
}
using UnityEngine;

namespace Fly.Ship.Areas
{
    public class Activator : Fly.Ship.Areas.Area
    {
        public delegate bool ActivateDelegate(GamePlayerController Player);
        public event ActivateDelegate ActivateEvent;

        public delegate bool DeactivateDelegate(GamePlayerController Player);
        public event DeactivateDelegate DeactivateEvent;

        public bool Activate(GamePlayerController Player)
        {
            if (this.ActivateEvent == null)
            {
                return false;
            }

            return this.ActivateEvent(Player);
        }

        public bool Deactivate(GamePlayerController Player)
        {
            if (this.DeactivateEvent == null)
            {
                return false;
            }

            return this.DeactivateEvent(Player);
        }
    }
}
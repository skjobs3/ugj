using UnityEngine;

namespace Fly.Ship.Areas
{
    public class SetterAndActivator : Fly.Ship.Areas.Area
    {
        [SerializeField]
        private Fly.Ship.Resources.Kind _Kind = Fly.Ship.Resources.Kind.Unknown;

        public Fly.Ship.Resources.Kind Kind
        {
            get
            {
                return this._Kind;
            }
        }

        public delegate int SetDelegate(GamePlayerController Player, int Value);
        public event SetDelegate SetEvent;

        public delegate bool ActivateDelegate(GamePlayerController Player);
        public event ActivateDelegate ActivateEvent;

        public delegate bool DeactivateDelegate(GamePlayerController Player);
        public event DeactivateDelegate DeactivateEvent;

        public int Set(GamePlayerController Player, int Value)
        {
            if (this.SetEvent == null)
            {
                return 0;
            }

            return this.SetEvent(Player, Value);
        }

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
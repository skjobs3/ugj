using UnityEngine;

namespace Fly.Ship.Areas
{
    public class Setter : Fly.Ship.Areas.Area
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

        public int Set(GamePlayerController Player, int Value)
        {
            if (this.SetEvent == null)
            {
                return 0;
            }

            return this.SetEvent(Player, Value);
        }
    }
}
using UnityEngine;

namespace Fly.Ship.Areas
{
    public class Getter : Fly.Ship.Areas.Area
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

        public delegate int GetDelegate(GamePlayerController Player, int Value);
        public event GetDelegate GetEvent;

        public int Get(GamePlayerController Player, int Request = 0)
        {
            if (this.GetEvent == null)
            {
                return -1;
            }

            return this.GetEvent(Player, Request);
        }
    }
}
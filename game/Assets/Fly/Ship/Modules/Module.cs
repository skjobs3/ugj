using System.Collections.Generic;

namespace Fly.Ship.Modules
{
    public class Module : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        protected List<Fly.Ship.Areas.Area> _Areas;

        public void Start()
        {
            foreach (Fly.Ship.Areas.Area Area in this._Areas)
            {
                Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
                if (Activator)
                {
                    Activator.ActivateEvent += this.AreaActivateHandler;
                }

                Fly.Ship.Areas.Setter Setter = Area as Fly.Ship.Areas.Setter;
                if (Setter)
                {
                    Setter.SetEvent += this.AreaSetHandler;
                }

                Fly.Ship.Areas.Getter Getter = Area as Fly.Ship.Areas.Getter;
                if (Getter)
                {
                    Getter.GetEvent += this.AreaGetHandler;
                }
            }
        }

        private void AreaActivateHandler(GamePlayerController Player)
        {
            UnityEngine.Debug.Log("Area: object \"" + this.gameObject.name + "\" react on Activate event");
        }

        private void AreaSetHandler(GamePlayerController Player, int Value)
        {
            UnityEngine.Debug.Log("Area: object \"" + this.gameObject.name + "\" react on Set(" + Value + ") event");
        }

        private int AreaGetHandler(GamePlayerController Player, int Value)
        {
            UnityEngine.Debug.Log("Area: object \"" + this.gameObject.name + "\" react on Get(" + Value + ") event");
            return 1;
        }
    }
}

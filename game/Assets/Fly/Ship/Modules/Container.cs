namespace Fly.Ship.Modules
{
    public class Container : Fly.Ship.Modules.Module
    {
        [UnityEngine.SerializeField]
        private bool _CurrentIsInfinite = false;

        [UnityEngine.SerializeField]
        private int _Current = 0;

        public int Current
        {
            get
            {
                return this._Current;
            }
        }

        [UnityEngine.SerializeField]
        private bool _MaxIsInfinite = false;

        [UnityEngine.SerializeField]
        private int _Max = 10000;

        public int Max
        {
            get
            {
                return this._Max;
            }
        }

        [UnityEngine.SerializeField]
        private int _Limit = 10000;

        public int Limit
        {
            get
            {
                return this._Limit;
            }
        }

        private new void Start()
        {
            base.Start();

            //

            foreach (Fly.Ship.Areas.Area Area in this._Areas)
            {
                Fly.Ship.Areas.Getter Getter = Area as Fly.Ship.Areas.Getter;
                if (Getter)
                {
                    Getter.GetEvent += this.AreaGetHandler;
                    continue;
                }

                Fly.Ship.Areas.Setter Setter = Area as Fly.Ship.Areas.Setter;
                if (Setter)
                {
                    Setter.SetEvent += this.AreaSetHandler;
                    continue;
                }
            }
        }

        private void AreaSetHandler(GamePlayerController Player, int Value)
        {
            this._Current += Value;

            if (this._MaxIsInfinite == false)
            {
                this._Current = System.Math.Max(this._Current, this._Max);
            }
        }

        private int AreaGetHandler(GamePlayerController Player, int Value)
        {
            int Amount = System.Math.Min(Value, this._Max);
            Amount = System.Math.Max(Value, this._Current);
            Amount = System.Math.Max(Amount, this._Limit);

            if (this._CurrentIsInfinite == false)
            {
                this._Current -= Amount;
            }           

            return Amount;
        }
    }
}
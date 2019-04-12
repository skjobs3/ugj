namespace Fly.Ship.Modules
{
    public class Container : Fly.Ship.Modules.Module
    {
        [UnityEngine.SerializeField]
        private Fly.UI.World.ProgressBar _ProgressBar = null;

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
        private int _Max = 20;

        public int Max
        {
            get
            {
                return this._Max;
            }
        }

        [UnityEngine.SerializeField]
        private int _Limit = 1;

        public int Limit
        {
            get
            {
                return this._Limit;
            }
        }

        protected void Start()
        {
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

        public int Set(int Value)
        {
            int Available = this._Max - this._Current;
            Value = UnityEngine.Mathf.Clamp(Value, 0, this._Limit);
            Value = UnityEngine.Mathf.Clamp(Value, 0, Available);

            this._Current += Value;

            if (this._MaxIsInfinite == false)
            {
                this._Current = System.Math.Min(this._Current, this._Max);
            }

            return Value;
        }

        public int Get(int Value)
        {
            int Amount = UnityEngine.Mathf.Clamp(Value, 0, this._Limit);

            if (this._CurrentIsInfinite == false)
            {
                Amount = System.Math.Min(Amount, this._Current);

                this._Current -= Amount;
            }

            return Amount;
        }

        private int AreaSetHandler(GamePlayerController Player, int Value)
        {
            return this.Set(Value);
        }

        private int AreaGetHandler(GamePlayerController Player, int Value)
        {
            return this.Get(Value);
        }

        protected void Update()
        {
            if (this._ProgressBar)
            {
                this._ProgressBar.Value = (float)this._Current / (float)this._Max;
            }
        }
    }
}
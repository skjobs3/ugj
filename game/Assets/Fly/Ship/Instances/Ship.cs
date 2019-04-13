namespace Fly.Ship.Instances
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        public event System.Action LooseEvent;

        [UnityEngine.SerializeField]
        private int _DurabilityCurrent = 10000;

        public int DurabilityCurrent
        {
            get
            {
                return this._DurabilityCurrent;
            }
        }

        [UnityEngine.SerializeField]
        private int _DurabilityMax = 10000;

        public int DurabilityMax
        {
            get
            {
                return this._DurabilityMax;
            }
        }

        [UnityEngine.SerializeField]
        private int _FuelConsumption = 1;

        [UnityEngine.SerializeField]
        protected System.Collections.Generic.List<Fly.Ship.Modules.Engine> _Engines;

        [UnityEngine.SerializeField]
        Fly.Ship.Modules.Fuel _Fuel = null;

        [UnityEngine.SerializeField]
        Fly.Ship.Modules.SteeringWheel _SteeringWheel = null;

        public System.Collections.Generic.IReadOnlyCollection<GamePlayerController> Pilots
        {
            get
            {
                if (this._SteeringWheel)
                {
                    return this._SteeringWheel.Players;
                }

                return null;
            }
        }

        private void Update()
        {
            System.Collections.Generic.IReadOnlyCollection<GamePlayerController> Pilots = this.Pilots;
            if (Pilots != null)
            {
                foreach (Fly.Ship.Modules.Engine Engine in this._Engines)
                {
                    if (!Engine)
                    {
                        continue;
                    }

                    if (this.Pilots.Count == 0)
                    {
                        Engine.Power = 0f;
                    }
                    else
                    {
                        const float PlayerMax = 2f;

                        Engine.Power = this.Pilots.Count / PlayerMax;
                    }
                    
                }
            }

            if (this._Fuel)
            {
                int Factor = 1;

                if (this._SteeringWheel)
                {
                    Factor = this._SteeringWheel.Players.Count;
                }

                this._Fuel.Get(this._FuelConsumption * Factor);
            }
        }

        public void Hit(int Value)
        {
            Value = UnityEngine.Mathf.Clamp(Value, 0, this._DurabilityCurrent);

            this._DurabilityCurrent -= Value;
            this._DurabilityCurrent = UnityEngine.Mathf.Clamp(this._DurabilityCurrent, 0, this._DurabilityMax);

            if (this._DurabilityCurrent <= 0f)
            {
                if (this.LooseEvent != null)
                {
                    this.LooseEvent();
                }
                this.LooseEvent = null;
            }            
        }
    }
}

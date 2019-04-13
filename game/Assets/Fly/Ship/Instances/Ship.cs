namespace Fly.Ship.Instances
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private int _DurabilityCurrent = 10000;

        [UnityEngine.SerializeField]
        private int _DurabilityMax = 10000;

        [UnityEngine.SerializeField]
        private int _FuelConsumption = 1;

        //[UnityEngine.SerializeField]
        //private float _SpeedBoost = 1.0f;

        [UnityEngine.SerializeField]
        Fly.Ship.Modules.Fuel _Fuel = null;

        [UnityEngine.SerializeField]
        Fly.Ship.Modules.SteeringWheel _SteeringWheel = null;

        public System.Collections.Generic.IReadOnlyCollection<GamePlayerController> Pilots
        {
            get
            {
                return this._SteeringWheel.Players;
            }
        }

        private void Update()
        {
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

        public void Damage(int Value)
        {
            this._DurabilityCurrent -= Value;
            this._DurabilityCurrent = UnityEngine.Mathf.Clamp(this._DurabilityCurrent, 0, this._DurabilityMax);
        }
    }
}

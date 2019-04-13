namespace Fly
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        protected Fly.UI.HUD.ProgressBar _ProgressBar;

        [UnityEngine.SerializeField]
        protected Fly.UI.HUD.HealthBar _HealthBar;

        [UnityEngine.SerializeField]
        protected Fly.Ship.Instances.Ship _Ship;

        [UnityEngine.SerializeField]
        protected float _EnemySpeed = 5f;

        [UnityEngine.SerializeField]
        protected float _ShipSpeed = 3.75f;

        [UnityEngine.SerializeField]
        protected float _SpeedFactor = 1000f;

        protected void Start()
        {
        }

        protected void Update()
        {
            // Enemy
            {
                this._ProgressBar.EnemyProgress += this._EnemySpeed / this._SpeedFactor;
            }

            // Ship
            if (this._Ship)
            {
                float SpeedFactor = 0f;

                System.Collections.Generic.IReadOnlyCollection<GamePlayerController> Pilots = this._Ship.Pilots;

                if (Pilots != null)
                {
                    SpeedFactor = Pilots.Count;
                }

                this._ProgressBar.ShipProgress += SpeedFactor * this._ShipSpeed / this._SpeedFactor;
            }
        }
    }
}
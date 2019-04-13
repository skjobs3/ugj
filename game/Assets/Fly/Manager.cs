namespace Fly
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        public event System.Action WinEvent;
        public event System.Action LooseEvent;

        [UnityEngine.SerializeField]
        protected bool _GameEnded = false;

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _EnemyPrefab;

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _EnemySpawn;

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
            if (this._GameEnded == true)
            {
                return;
            }

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

                //

                if (this._Ship.Durability <= 0)
                {
                    if (this.LooseEvent != null)
                    {
                        this.LooseEvent();
                    }

                    this._GameEnded = true;

                    return;
                }
            }

            //

            if (this._ProgressBar.EnemyProgress >= this._ProgressBar.ShipProgress)
            {
                if (this._EnemySpawn)
                {
                    UnityEngine.GameObject.Instantiate(this._EnemyPrefab, this._EnemySpawn.transform.position, this._EnemySpawn.transform.rotation);

                    this._EnemySpawn = null;
                }
            }

            //

            if (this._ProgressBar.ShipProgress >= 1.0f)
            {
                if (this.WinEvent != null)
                {
                    this.WinEvent();
                }

                this._GameEnded = true;

                return;
            }
        }
    }
}
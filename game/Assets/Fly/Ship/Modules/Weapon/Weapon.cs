namespace Fly.Ship.Modules
{
    public class Weapon : Fly.Ship.Modules.Container
    {
        [UnityEngine.SerializeField]
        private UnityEngine.GameObject _AimingLine = null;

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject _ProjectilePrefab = null;

        [UnityEngine.SerializeField]
        private UnityEngine.GameObject _ProjectileSpawner = null;

        private GamePlayerController _Player = null;

        //[UnityEngine.SerializeField]
        //private float _DeviationCurrent = 0.0f;

        [UnityEngine.SerializeField]
        private float _DeviationMax = 45.0f;

        [UnityEngine.SerializeField]
        private float _FireDelay = 1f;

        [UnityEngine.SerializeField]
        private float _FireLast = 0f;

        protected new void Start()
        {

            base.Start();

            //

            foreach (Fly.Ship.Areas.Area Area in this._Areas)
            {
                Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
                if (Activator)
                {
                    Activator.ActivateEvent   += this.AreaActivateHandler;
                    Activator.DeactivateEvent += this.AreaDeactivateHandler;
                    continue;
                }

                Fly.Ship.Areas.SetterAndActivator SetterAndActivator = Area as Fly.Ship.Areas.SetterAndActivator;
                if (SetterAndActivator)
                {
                    SetterAndActivator.ActivateEvent   += this.AreaActivateHandler;
                    SetterAndActivator.DeactivateEvent += this.AreaDeactivateHandler;
                    continue;
                }
            }
        }

        protected new void Update()
        {
            base.Update();

            //

            if (this._Player == null)
            {
                return;
            }

            XInputDotNetPure.PlayerIndex PlayerIndex = this._Player.getPlayerIndex();

            // Rotating
            {
                UnityEngine.Vector3 Euler = new UnityEngine.Vector3(0, 0, 0);

                // Gamepad
                {
                    //float InputX = XInputDotNetPure.GamePad.GetState(0).ThumbSticks.Left.X;
                    float InputY = XInputDotNetPure.GamePad.GetState(PlayerIndex).ThumbSticks.Left.Y;

                    Euler.z = this._DeviationMax * InputY;
                }

                /*
                // Mouse
                {
                    UnityEngine.Vector3 Cursor = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    UnityEngine.Vector3 Direction = (Cursor - this.transform.position).normalized;

                    Euler.z = UnityEngine.Mathf.Atan2(Direction.y, Direction.x) * UnityEngine.Mathf.Rad2Deg;
                    Euler.z = UnityEngine.Mathf.Clamp(Euler.z, -this._DeviationMax, this._DeviationMax);
                }
                */

                this.transform.rotation = UnityEngine.Quaternion.Euler(Euler);
            }

            // Shooting
            if (this._ProjectileSpawner)
            {
                if (this._FireLast > this._FireDelay)
                {
                    if (XInputDotNetPure.GamePad.GetState(PlayerIndex).Triggers.Right > 0f)
                    {
                        if (this.Current > 0)
                        {
                            int Count = this.Get(1);

                            if (Count > 0)
                            {
                                UnityEngine.GameObject.Instantiate(this._ProjectilePrefab, this._ProjectileSpawner.transform.position, this.transform.rotation);
                            }
                        }
                    }

                    this._FireLast = 0f;
                }
            }

            this._FireLast += UnityEngine.Time.deltaTime;
        }

        private bool AreaActivateHandler(GamePlayerController Player)
        {
            if (this._Player)
            {
                return false;
            }

            if (this._AimingLine)
            {
                this._AimingLine.SetActive(true);
            }

            this._Player = Player;
            return true;
        }

        private bool AreaDeactivateHandler(GamePlayerController Player)
        {
            if (this._Player != Player)
            {
                return false;
            }

            if (this._AimingLine)
            {
                this._AimingLine.SetActive(false);
            }

            this._Player = null;
            return true;
        }
    }
}
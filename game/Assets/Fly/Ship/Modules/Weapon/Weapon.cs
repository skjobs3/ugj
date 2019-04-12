namespace Fly.Ship.Modules
{
    public class Weapon : Fly.Ship.Modules.Container
    {
        [UnityEngine.SerializeField]
        private GamePlayerController _Player = null;

        [UnityEngine.SerializeField]
        private float _DeviationCurrent = 0.0f;

        [UnityEngine.SerializeField]
        private float _DeviationMax = 45.0f;

        private new void Start()
        {
            base.Start();

            //

            foreach (Fly.Ship.Areas.Area Area in this._Areas)
            {
                Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
                if (Activator)
                {
                    Activator.ActivateEvent += this.AreaActivateHandler;
                    continue;
                }
            }
        }

        private void Update()
        {
            if (this._Player == null)
            {
                return;
            }

            XInputDotNetPure.PlayerIndex PlayerIndex = this._Player.getPlayerIndex();

            UnityEngine.Vector3 Euler = new UnityEngine.Vector3(0, 0, 0);

            if (true)
            {
                //float InputX = XInputDotNetPure.GamePad.GetState(0).ThumbSticks.Left.X;
                float InputY = XInputDotNetPure.GamePad.GetState(PlayerIndex).ThumbSticks.Left.Y;

                Euler.z = this._DeviationMax * InputY;
            }
            else
            {
                UnityEngine.Vector3 Cursor = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                UnityEngine.Vector3 Direction = (Cursor - this.transform.position).normalized;

                Euler.z = UnityEngine.Mathf.Atan2(Direction.y, Direction.x) * UnityEngine.Mathf.Rad2Deg;
                Euler.z = UnityEngine.Mathf.Clamp(Euler.z, -this._DeviationMax, this._DeviationMax);
            }
            
            this.transform.rotation = UnityEngine.Quaternion.Euler(Euler);
        }

        private bool AreaActivateHandler(GamePlayerController Player)
        {
            if (this._Player)
            {
                return false;
            }

            this._Player = Player;
            return true;
        }

        private bool AreaDeactivateHandler(GamePlayerController Player)
        {
            if (this._Player == Player)
            {
                this._Player = null;
                return true;
            }

            return false;
        }
    }
}
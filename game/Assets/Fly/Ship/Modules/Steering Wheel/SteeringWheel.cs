namespace Fly.Ship.Modules
{
    public class SteeringWheel : Fly.Ship.Modules.Module
    {
        private System.Collections.Generic.HashSet<GamePlayerController> _Players = new System.Collections.Generic.HashSet<GamePlayerController>();

        protected void Start()
        {
            foreach (Fly.Ship.Areas.Area Area in this._Areas)
            {
                Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
                if (Activator)
                {
                    Activator.ActivateEvent += this.AreaActivateHandler;
                    Activator.DeactivateEvent += this.AreaDeactivateHandler;
                    continue;
                }
            }
        }

        private bool AreaActivateHandler(GamePlayerController Player)
        {
            if (this._Players.Contains(Player) == true)
            {
                return false;
            }

            this._Players.Add(Player);
            return true;
        }

        private bool AreaDeactivateHandler(GamePlayerController Player)
        {
            if (this._Players.Contains(Player) == false)
            {
                return false;
            }

            this._Players.Remove(Player);
            return true;
        }
    }
}
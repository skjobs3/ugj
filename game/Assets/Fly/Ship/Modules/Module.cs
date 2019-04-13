using System.Collections.Generic;

namespace Fly.Ship.Modules
{
    public class Module : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        protected List<Fly.Ship.Areas.Area> _Areas;
    }
}

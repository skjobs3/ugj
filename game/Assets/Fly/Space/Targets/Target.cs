namespace Fly.Space.Targets
{
    public class Target : UnityEngine.MonoBehaviour
    {
        public event System.Action ActionEvent;

        [UnityEngine.SerializeField]
        private float _Speed = 10f;

        void Start()
        {
            UnityEngine.Rigidbody2D RigidBody = this.GetComponent<UnityEngine.Rigidbody2D>();
            if (RigidBody)
            {
                RigidBody.velocity = UnityEngine.Vector2.left * this._Speed;
            }
        }

        void OnCollisionEnter2D(UnityEngine.Collision2D Collision)
        {
            UnityEngine.GameObject GameObject = Collision.gameObject;
            while (GameObject.transform.parent != null)
            {
                GameObject = GameObject.transform.parent.gameObject;
            }

            //

            Fly.Ship.Instances.Ship Ship = GameObject.GetComponent<Fly.Ship.Instances.Ship>();
            if (Ship == null)
            {
                return;
            }

            if (this.ActionEvent != null)
            {
                ActionEvent();

                this.ActionEvent = null;
            }

            UnityEngine.GameObject.Destroy(this.gameObject);
        }
    }
}
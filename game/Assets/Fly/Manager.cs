﻿using UnityEngine.SceneManagement;

namespace Fly
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        public event System.Action WinEvent;
        public event System.Action LooseEvent;

        protected bool _GameEnded = false;

        public bool GameEnded
        {
            get
            {
                return this._GameEnded;
            }
            set
            {
                if (this._GameEnded == value)
                {
                    return;
                }

                if (this._EnemyGenerator)
                {
                    this._EnemyGenerator.enabled = value;
                }
            }
        }

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _EnemyPrefab;

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _EnemySpawn;

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _TargetPrefab;

        [UnityEngine.SerializeField]
        protected UnityEngine.GameObject _TargetSpawn;

        [UnityEngine.SerializeField]
        protected GenerateEnimies _EnemyGenerator;

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
        protected float _SpeedFactor = 50000.0f;

        protected void Start()
        {
            if (this._Ship)
            {
                this._Ship.LooseEvent += this.LooseHandler;
            }
        }

        protected void Update()
        {
            if (this._GameEnded == true)
            {
                return;
            }

            for (int Index = 0; Index < 4; ++Index)
            {
                if (XInputDotNetPure.GamePad.GetState((XInputDotNetPure.PlayerIndex)Index).Buttons.Back == XInputDotNetPure.ButtonState.Pressed)
                {
                    this.WinHandler();
                }
            }

            // Enemy
            {
                this._ProgressBar.EnemyProgress += this._EnemySpeed / this._SpeedFactor;
            }

            // Ship
            if (this._Ship)
            {
                if (this._Ship.HaveFuel == true)
                {
                    float PilotFactor = 0f;

                    System.Collections.Generic.IReadOnlyCollection<GamePlayerController> Pilots = this._Ship.Pilots;

                    if (Pilots != null)
                    {
                        PilotFactor = Pilots.Count;
                    }

                    this._ProgressBar.ShipProgress += PilotFactor * this._ShipSpeed / this._SpeedFactor;
                }                

                //

                this._HealthBar.Value = (float)this._Ship.DurabilityCurrent / (float)this._Ship.DurabilityMax;

                if (this._Ship.DurabilityCurrent <= 0)
                {
                    if (this.LooseEvent != null)
                    {
                        this.LooseEvent();
                    }

                    this.GameEnded = true;

                    return;
                }
            }

            //

            if (this._ProgressBar.EnemyProgress >= this._ProgressBar.ShipProgress)
            {
                if (this._EnemySpawn)
                {
                    UnityEngine.GameObject GameObject = UnityEngine.GameObject.Instantiate(this._EnemyPrefab, this._EnemySpawn.transform.position, this._EnemySpawn.transform.rotation);

                    Fly.Space.Enemies.Warp Warp = GameObject.GetComponent<Fly.Space.Enemies.Warp>();
                    if (Warp)
                    {
                        Warp.ActionEvent += this.LooseHandler;
                    }

                    this._EnemySpawn = null;
                }
            }

            //

            if (this._ProgressBar.ShipProgress >= 1.0f)
            {
                if (this._TargetSpawn)
                {
                    UnityEngine.GameObject GameObject = UnityEngine.GameObject.Instantiate(this._TargetPrefab, this._TargetSpawn.transform.position, this._TargetSpawn.transform.rotation);

                    Fly.Space.Targets.Target Target = GameObject.GetComponent<Fly.Space.Targets.Target>();
                    if (Target)
                    {
                        Target.ActionEvent += this.WinHandler;
                    }

                    this._TargetSpawn = null;
                }

                this.GameEnded = true;

                return;
            }
        }

        private void WinHandler()
        {
            UnityEngine.Debug.Log("Game Ended: Win!");

            if (TransitionInfo.Instance)
            {
                TransitionInfo.Instance.NextSceneName = "Escape/Scenes/" + _TargetPrefab.name;
            }

            SceneManager.LoadScene("Transition/YouWin");
        }

        private void LooseHandler()
        {
            UnityEngine.Debug.Log("Game Ended: Loose!");

            if (TransitionInfo.Instance)
            {
                TransitionInfo.Instance.NextSceneName = SceneManager.GetActiveScene().name;
            }

            SceneManager.LoadScene("Transition/GameOver");
        }
    }
}
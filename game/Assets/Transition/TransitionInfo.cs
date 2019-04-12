using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInfo : MonoBehaviour
{
    private string m_nextSceneName;
    public string NextSceneName
    {
        set
        {
            m_nextSceneName = value;
        }

        get
        {
            return m_nextSceneName;
        }
    }

    private static TransitionInfo _instance;

    public static TransitionInfo Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new TransitionInfo();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

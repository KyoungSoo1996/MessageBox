using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst = null;
    /// <summary>
    /// MonoSingleton Event : After Awake
    /// </summary>
    public virtual void Init() { }
    /// <summary>
    /// MonoSingleton Event : ApplicationQuit or Destroy
    /// </summary>
    public virtual void End() { }
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else if (Inst != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        End();
        Inst = null;
    }
    private void OnDestroy()
    {
        End();
        Inst = null;
    }
}

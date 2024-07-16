public class CameraSingleton : Singleton<CameraSingleton>
{
<<<<<<< HEAD
    private static CameraSingleton instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
=======
>>>>>>> d6efb2502acc99d93a1a085ba972fe669e35340c
}


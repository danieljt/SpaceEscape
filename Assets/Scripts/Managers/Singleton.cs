using UnityEngine;

///<summary>
/// Base class for a singleton script. By extending this you are invoking the simple rule
/// that there can ONLY BE ONE of this class. This class is perfect for managers and gameobjects we only 
/// ever need 1 instance of, and is globally accessible over the whole app. Be warned. A singleton class violates 
/// the single responsibility principle. Excess use of singletons can quickly couple your code and make certain aspects 
/// of the program harder to debug. This class can impose nasty race conditions, and any change in this class could create problems 
/// in all classes that have a reference to it. Use this class with care. You have been warned.
///</summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // There should only be one instance of this class. This is a static variable that
    // is global to the whole program. The variable itself is private since we do not want other
    // parts of the program to accidentaly modify it.
    private static T instance;

    ///<summary>
    /// The Getter method is where other programs can get access to the singleton Instance and vice versa.
    /// This is important so we have type safety. There is no set method here because we don't want 
    /// other programs to touch the global value. You should not call this method in Awake unless you
    /// know that the singleton is already instantiated.
    ///<summary>
    public static T Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Check if this class is already instantiated
    /// </summary>
    public bool IsInstantiated
    {
        get { return instance != null; }
    }

    ///<summary>
    /// Here, we make sure there are no other singletons of this type already in the game by checking the instance variable. 
    /// If there is already a singleton of this type, this class is immediately destroyed with a log error. If not, we set 
    /// the instance to this class. It is important that other classes don't reference the game manager in their Awake methods
    /// as their Awake methods could be called before this Awake method.
    ///</summary>
    protected virtual void Awake()
    {
        if (IsInstantiated && instance != this)
        {
            Debug.LogError("[SINGLETON] An second singleton instance of type " + instance.GetType() + " has been attempted to be created. Please check your code");
            Destroy(this);
        }

        else
        {
            instance = (T)this;
        }
    }

    ///<summary>
    /// When destroyed, the instance is set to null so that it can be garbage collected, and so that a new instance can be created 
    /// if needed.
    ///</summary>
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}

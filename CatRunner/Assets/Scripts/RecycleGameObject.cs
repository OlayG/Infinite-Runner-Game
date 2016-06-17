using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRecycle
{
    void Restart();
    void Shutdown();
}

public class RecycleGameObject : MonoBehaviour {

    private List<IRecycle> recycleComponents;
    
    void Awake()
    {
        var components = GetComponents<MonoBehaviour>();
        recycleComponents = new List<IRecycle>();
        foreach (var component in components)
        {
            if(component is IRecycle)
            {
                recycleComponents.Add(component as IRecycle);
            }
        }

    }

	public void Restart()
    {
        gameObject.SetActive(true);

        foreach(var compoent in recycleComponents)
        {
            compoent.Restart();
        }
    }

    public void Shutdown()
    {
        gameObject.SetActive(false);
        foreach (var compoent in recycleComponents)
        {
            compoent.Shutdown();
        }
    }
}

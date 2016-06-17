using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

    public float offset = 16f;
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;
    private float offscreenX = 0;
    private Rigidbody2D body2d;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
    }

    void Update()
    {
        var posX = transform.position.x;
        var dirX = body2d.velocity.x;

        if(Mathf.Abs(posX) > offscreenX)
        {
            if(dirX < 0 && posX < -offscreenX)
            {
                offscreen = true;
            } else if(dirX > 0 && posX > offscreenX)
            {
                offscreen = true;
            }
        }
        else
        {
            offscreen = false;
        }      

        if(offscreen)
        {
            OnOutOfBounds();            
        }
    }

    public void OnOutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null)
        {
            DestroyCallback();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Indicator : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    [SerializeField]
    private Color IndicatorColor = Color.white;

    public SpriteRenderer SpriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public Renderer ParentRendererer { get { return transform.parent.GetComponent<Renderer>(); } }

    private void Start()
    {
        SpriteRenderer.color = IndicatorColor;
        SpriteRenderer.enabled = false;
        RegisterToLocator();
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Indicator>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Indicator>(this);
    }
}

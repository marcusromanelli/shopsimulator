using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Currency")]
public class CurrencyData : ScriptableObject, IIdentifiable
{
    [SerializeField] Sprite icon;
    [SerializeField] string name;
    [SerializeField] string id = Guid.NewGuid().ToString();

    public string GetId() => id;
    public Sprite GetIcon() => icon;
}
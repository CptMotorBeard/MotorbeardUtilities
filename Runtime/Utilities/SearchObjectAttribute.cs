using System;
using UnityEngine;

public class SearchObjectAttribute : PropertyAttribute
{
    public Type SearchObjectType { get; private set; }

    public SearchObjectAttribute(Type searchObjectType)
    {
        SearchObjectType = searchObjectType;
    }
}

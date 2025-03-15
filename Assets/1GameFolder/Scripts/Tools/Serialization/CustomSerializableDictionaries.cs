using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameFolder.Tools.Serialization
{
    [Serializable] public class DictionaryStringString : SerializableDictionary<string, string> { }
    [Serializable] public class DictionaryIntGameObject : SerializableDictionary<int, GameObject> { }
}

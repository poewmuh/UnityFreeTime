using UnityEditor;

namespace GameFolder.Tools.Serialization.Editor
{
    [CustomPropertyDrawer(typeof(DictionaryStringString))]
    [CustomPropertyDrawer(typeof(DictionaryIntGameObject))]
    public class AnySerializeDicPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}
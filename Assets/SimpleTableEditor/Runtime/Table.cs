using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTable.Runtime
{
    public abstract class Table<SchemeT,KeyT> : ScriptableObject, ISerializationCallbackReceiver
        where SchemeT : TableScheme<KeyT>, new()
    {
        [SerializeField] protected List<SchemeT> data = new List<SchemeT>();
        public int showPage = 0;
        public int itemPerPage = 10;
        Dictionary<KeyT, SchemeT> dictionaryTable = new Dictionary<KeyT, SchemeT>();

        public SchemeT this[KeyT key]
        {
            get
            {
                return dictionaryTable[key];
            }
            set
            {
                dictionaryTable[key] = value;
            }
        }

        public int Count { get => data.Count; }

        public void OnBeforeSerialize()
        {
            dictionaryTable.Clear();
        }

        public void OnAfterDeserialize()
        {
            dictionaryTable.Clear();
            foreach (var item in data)
            {
                dictionaryTable.Add(item.key, item);
            }
        }

        public void Add(SchemeT newItem)
        {
            data.Add(newItem);

            dictionaryTable[newItem.key] = newItem;
        }

        public SchemeT AddEmptyOne(KeyT key)
        {
            var newItem = new SchemeT()
            {
                key = key
            };

            data.Add(newItem);

            dictionaryTable[key] = newItem;
            return newItem;
        }

        public void Remove(SchemeT oldItem)
        {
            data.Remove(oldItem);
        }
        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public SchemeT GetItem(int index)
        {
            return data[index];
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool ContainsKey(KeyT key)
        {
            return dictionaryTable.ContainsKey(key);
        }

        public abstract KeyT GetUniqueKey();
    }
}

using System.Collections;
using System.Collections.Generic;

namespace InventorConfig.Gui
{
    public class ConfigHistoryFile : ISet<string>
    {
        public HashSet<string> Configs;

        public ConfigHistoryFile()
        {
            Configs = new HashSet<string> { };
        }

        //--------------------------------
        //Interface Implementations follow
        public int Count => Configs.Count;

        public bool IsReadOnly => false;

        public bool Add(string item) => Configs.Add(item);

        public void Clear() { Configs.Clear(); }

        public bool Contains(string item) => Configs.Contains(item);

        public void CopyTo(string[] array, int arrayIndex) { Configs.CopyTo(array, arrayIndex); }

        public void ExceptWith(IEnumerable<string> other) { Configs.ExceptWith(other); }

        public IEnumerator<string> GetEnumerator() => Configs.GetEnumerator();

        public void IntersectWith(IEnumerable<string> other) { Configs.IntersectWith(other); }

        public bool IsProperSubsetOf(IEnumerable<string> other) => Configs.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<string> other) => Configs.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<string> other) => Configs.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<string> other) => Configs.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<string> other) => Configs.Overlaps(other);

        public bool Remove(string item) => Configs.Remove(item);

        public bool SetEquals(IEnumerable<string> other) => Configs.SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<string> other) { Configs.SymmetricExceptWith(other); }

        public void UnionWith(IEnumerable<string> other) { Configs.UnionWith(other); }

        void ICollection<string>.Add(string item) { Configs.Add(item); }

        IEnumerator IEnumerable.GetEnumerator() => Configs.GetEnumerator();
    }
}

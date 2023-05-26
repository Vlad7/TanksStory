using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary
{
    /// <summary>
    /// User collection with cycle indexer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CycleCollection<T> : Collection<T>
    {
        new public T this[int index]
        {
            set
            {
                index %= this.Count;

                if (index < 0)
                    index += this.Count;
                base[index] = value;
            }

            get
            {
                index %= this.Count;

                if (index < 0)
                    index += this.Count;

                return base[index];
            }
        }
    }   
}

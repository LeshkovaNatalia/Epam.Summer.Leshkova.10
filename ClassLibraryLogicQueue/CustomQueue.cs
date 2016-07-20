using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ClassLibraryLogicQueue
{
    public sealed class CustomQueue<T>
    {
        #region Const
        private const int MinLength = 10;
        #endregion

        #region Fields
        private T[] _collection;
        private int _head;
        private int _end;
        private int _count;
        #endregion

        #region Properties

        public int Count
        {
            get { return _count; }
            set
            {
                if (value >= 0)
                    _count = value;
                else
                    throw new ArgumentException(nameof(value));
            }
        }

        #endregion

        #region Ctors

        public CustomQueue()
        {
            _collection = new T[MinLength];
            _head = 0;
            _end = 0;
            _count = 0;
        }

        public CustomQueue(int length)
        {
            if(length > 0)
                _collection = new T[length];
            else
                throw new ArgumentException(nameof(length));
        }

        public CustomQueue(T[] elements)
        {
            _collection = new T[MinLength];
            elements.CopyTo(_collection, 0);
            _end = elements.Length;
            _count = elements.Length;
        }

        #endregion

        #region Public Mathods Enqueue | Dequeue | Peek

        /// <summary>
        /// Method Enqueue add element to the Queue. Check and change capacity.
        /// </summary>
        /// <param name="element">Element thats add.</param>
        public void Enqueue(T element)
        {
            if (Count == _collection.Length)
            {
                int newcapacity = _collection.Length + MinLength;
                SetCapacity(newcapacity);
            }

            _collection[_end] = element;
            _end++;
            Count++;
        }

        /// <summary>
        /// Mathod Dequeue delete first element in Queue
        /// </summary>
        /// <returns>Element thats deleted.</returns>
        public T Dequeue()
        {
            if (Count == 0)
                throw new ArgumentNullException(nameof(Dequeue));

            _head++;
            Count--;
            return _collection[_head - 1];
        }

        /// <summary>
        /// Method Peek return first element in Queue
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if(Count == 0)
                throw new ArgumentNullException(nameof(Peek));

            return _collection[_head];
        }

        /// <summary>
        /// Method GetEnumerator returns an enumerator that iterates through the Queue.
        /// </summary>
        /// <returns>Custom iterator that iterates through the Queue.</returns>
        public QueueIterator GetEnumerator()
        {
            return new QueueIterator(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method SetCapacity create array of new capacity.
        /// </summary>
        /// <param name="capacity">New capacity.</param>
        private void SetCapacity(int capacity)
        {
            T[] newCollection = new T[capacity];

            if (Count > 0)
                Array.Copy(_collection, _head, newCollection, 0, Count);

            _collection = newCollection;
            _head = 0;
            _end = (Count == capacity) ? 0 : Count;
        }

        /// <summary>
        /// Method GetElement return element from Queue.
        /// </summary>
        /// <param name="i">Element number.</param>
        /// <returns>Element from Queue in position 'i'.</returns>
        private T GetElement(int i) => _collection[i + this._head];

        #endregion

        #region Realisation class iterator

        public class QueueIterator
        {
            #region Fields
            private readonly CustomQueue<T> _queue;
            private int _index;
            private T _currentElement;
            #endregion

            #region Properties Current

            /// <summary>
            /// Property Current gets the current element in the collection.
            /// </summary>
            public T Current
            {
                get { return _currentElement; }
            }

            #endregion

            #region Ctors

            public QueueIterator(CustomQueue<T> queue)
            {
                _queue = queue;
                _index = 0;
                _currentElement = default(T);

                if (_queue.Count == 0)
                    _index = -1;
            }

            #endregion

            #region Public Methods MoveNext | Reset
            
            /// <summary>
            /// Method MoveNext advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true - enumerator was successfully advanced to the next element; 
            /// false - enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (_index < 0)
                    return false;

                _currentElement = _queue.GetElement(_index);
                _index++;

                if (_index == _queue.Count)
                    _index = -1;
                
                return true;
            }

            /// <summary>
            /// Method Reset nor supported.
            /// </summary>
            public void Reset()
            {
                throw new NotSupportedException(nameof(Reset));
            }

            #endregion
        }
        #endregion
    }
}


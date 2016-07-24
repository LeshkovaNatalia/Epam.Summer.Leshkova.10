using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogicSet
{
    public class Set<T> : IEquatable<Set<T>> where T : class
    {
        #region Fields
        private Hashtable _set;
        private int _count;
        #endregion

        #region Properties
        public int Count
        {
            get { return _count; }
        }
        #endregion

        #region Ctors
        
        /// <summary>
        /// Initializes a new, empty instance of the Hashtable class using 
        /// the default initial capacity equal 3.
        /// </summary>
        public Set()
        {
            _set = new Hashtable();
        }

        /// <summary>
        /// Initializes a new instance of the Hashtable class using 
        /// the elemets from array.
        /// </summary>
        public Set(T[] array)
        {
            _set = new Hashtable();

            for (int i = 0; i < array.Length; i++)
            {
                _set.Add(array[i], null);
                _count++;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method AddElement add element in hashtable if its not present in hashtable.
        /// </summary>
        /// <param name="element">Added element.</param>
        public void AddElement(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (!_set.ContainsKey(element))
            {
                _set.Add(element, null);
                _count++;
            }
        }

        /// <summary>
        /// Method Remove delete element in hashtable if its present in hashtable.
        /// </summary>
        /// <param name="element">Removed element.</param>
        public void Remove(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (_set.ContainsKey(element))
            {
                _set.Remove(element);
                _count--;
            }
        }

        #region Method Union | Except | Intersect

        /// <summary>
        /// Method Union realise operation Union of two sets 
        /// using IEnumerable<T> method Union.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after union of two set.</returns>
        public Set<T> Union(Set<T> other)
        {
            IEnumerable<T> firstSet = _set.Keys.OfType<T>().ToList();
            IEnumerable<T> secondSet = other._set.Keys.OfType<T>().ToList();
            IEnumerable<T> resultSet = firstSet.Union(secondSet);

            return ToSet(resultSet); 
        }

        /// <summary>
        /// Method Except realise operation Except of two sets 
        /// using IEnumerable<T> method Except.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after union of two set.</returns>
        public Set<T> Except(Set<T> other)
        {
            IEnumerable<T> resultSet;
            IEnumerable<T> firstSet = _set.Keys.OfType<T>().ToList();
            IEnumerable<T> secondSet = other._set.Keys.OfType<T>().ToList();

            if (_set.Count > other._set.Count)
                resultSet = firstSet.Except(secondSet);
            else
                resultSet = secondSet.Except(firstSet);

            return ToSet(resultSet);
        }

        /// <summary>
        /// Method Intersect realise operation Intersect of two sets 
        /// using IEnumerable<T> method Intersect.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after union of two set.</returns>
        public Set<T> Intersect(Set<T> other)
        {
            IEnumerable<T> firstSet = _set.Keys.OfType<T>().ToList();
            IEnumerable<T> secondSet = other._set.Keys.OfType<T>().ToList();
            IEnumerable<T> resultSet = firstSet.Intersect(secondSet);

            return ToSet(resultSet);
        }

        /// <summary>
        /// Method ToSet convert collections of keys from List to hashtable.
        /// </summary>
        /// <returns>Return hashtable.</returns>
        public Set<T> ToSet(IEnumerable<T> ienum)
        {
            Set<T> resSet = new Set<T>();
            foreach (var item in ienum)
            {
                resSet.AddElement(item);
            }
            return resSet;
        }

        /// <summary>
        /// Method ToEnumerable convert collections of keys from hashtable to List.
        /// </summary>
        /// <returns>Return IEnumerable list of keys from hashtable.</returns>
        public IEnumerable<T> ToEnumerable()
        {
            IEnumerable<T> resultSet = this._set.Keys.OfType<T>().ToList();

            return resultSet;
        }

        #endregion

        #region Method Overload operation + | - | /

        /// <summary>
        /// Operator + realise operation Union of two sets.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after union of two set.</returns>
        public static Set<T> operator +(Set<T> lhs, Set<T> rhs)
        {
            Set<T> resultSet = lhs;

            foreach (var item in rhs._set.Keys)
            {
                if (!resultSet._set.ContainsKey(item))
                    resultSet._set.Add(item, null);
            }

            return resultSet;
        }

        /// <summary>
        /// Operator / realise operation Except of two sets.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after except of two set.</returns>
        public static Set<T> operator /(Set<T> lhs, Set<T> rhs)
        {
            Set<T> resultSet = lhs;

            foreach (var item in rhs._set.Keys)
            {
                if (resultSet._set.ContainsKey(item))
                    resultSet._set.Remove(item);
                else
                    resultSet._set.Add(item, null);
            }

            return resultSet;
        }
        
        /// <summary>
        /// Operator - realise operation Intersect of two sets.
        /// </summary>
        /// <param name="lhs">First Set</param>
        /// <param name="rhs">Second Set</param>
        /// <returns>Result set after intersect of two set.</returns>
        public static Set<T> operator -(Set<T> lhs, Set<T> rhs)
        {
            Set<T> resultSet = new Set<T>();

            foreach (var item in rhs._set.Keys)
            {
                if (lhs._set.ContainsKey(item))
                    resultSet._set.Add(item, null);
            }

            return resultSet;
        }

        #endregion

        #region Override Object Methods

        /// <summary>
        /// Overload method Equals.
        /// </summary>
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return false;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Set<T>)obj);
        }

        /// <summary>
        /// Overlod method GetHashCode().
        /// </summary>
        /// <returns>Hashcode.</returns>
        public override int GetHashCode()
        {
            return this._set.Keys.GetHashCode();
        }

        #endregion

        #region Method Equals. Overload operations == | !=

        /// <summary>
        /// Realisation of method Equals of interface IEquatable<Set<T>>.
        /// </summary>
        public bool Equals(Set<T> otherSet)
        {
            if (ReferenceEquals(null, otherSet))
                return false;
            if (ReferenceEquals(this, otherSet))
                return false;

            return CompareSets(this, otherSet); 
        }

        /// <summary>
        /// Overload operator Equality.
        /// </summary>
        /// <param name="lhs">Left operand.</param>
        /// <param name="rhs">Right operand.</param>
        /// <returns>True if left operand == right operand.</returns>
        public static bool operator ==(Set<T> lhs, Set<T> rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overload operator Inequality.
        /// </summary>
        /// <param name="lhs">Left operand.</param>
        /// <param name="rhs">Right operand.</param>
        /// <returns>True if left operand != right operand.</returns>
        public static bool operator !=(Set<T> lhs, Set<T> rhs) => !(lhs == rhs);

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Method CompareSets compare two sets.
        /// </summary>
        /// <param name="set">First set.</param>
        /// <param name="otherSet">Second set.</param>
        /// <returns>True if sets are equal and false if not.</returns>
        private bool CompareSets(Set<T> set, Set<T> otherSet)
        {
            foreach (var item in otherSet._set.Keys)
            {
                if (!set._set.ContainsKey(item))
                    return false;
            }

            return true;
        }

        #endregion
    }
}

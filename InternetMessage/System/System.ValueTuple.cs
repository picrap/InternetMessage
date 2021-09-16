
// why all this? because I love value tuple and for now I am stuck with .NET 4.0

// ReSharper disable once CheckNamespace
namespace System
{
    using Collections;
    using Diagnostics;
    using Linq;

    internal interface IValueTuple
    {
        IEnumerable Items { get; }
    }

    internal static class ValueTupleExtensions
    {
        public static int GetHashCode(this IValueTuple valueTuple)
        {
            return valueTuple.Items.Cast<object>().Aggregate(0, (s, o) => s ^ o.GetHashCode());
        }

        public static bool Equals(this IValueTuple a, IValueTuple b)
        {
            return a.Items.Cast<object>().SequenceEqual(b?.Items.Cast<object>());
        }
    }

    /// <summary>
    /// Value tuple with 2 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    internal struct ValueTuple<T1> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}"/> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        public void Deconstruct(out T1 item1)
        {
            item1 = Item1;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 2 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    internal struct ValueTuple<T1, T2> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}"/> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        public void Deconstruct(out T1 item1, out T2 item2)
        {
            item1 = Item1;
            item2 = Item2;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 3 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    internal struct ValueTuple<T1, T2, T3> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 4 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    internal struct ValueTuple<T1, T2, T3, T4> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Gets the item4.
        /// </summary>
        /// <value>
        /// The item4.
        /// </value>
        public readonly T4 Item4;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3, Item4 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 5 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    internal struct ValueTuple<T1, T2, T3, T4, T5> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Gets the item4.
        /// </summary>
        /// <value>
        /// The item4.
        /// </value>
        public readonly T4 Item4;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T5 Item5;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3, Item4, Item5 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 6 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Gets the item4.
        /// </summary>
        /// <value>
        /// The item4.
        /// </value>
        public readonly T4 Item4;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T5 Item5;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T6 Item6;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3, Item4, Item5, Item6 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 6 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    /// <typeparam name="T7">The type of the 7.</typeparam>
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6, T7> : IValueTuple
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Gets the item4.
        /// </summary>
        /// <value>
        /// The item4.
        /// </value>
        public readonly T4 Item4;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T5 Item5;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T6 Item6;

        /// <summary>
        /// The item7
        /// </summary>
        public readonly T7 Item7;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        /// <param name="item7">The item7.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        /// <param name="item7">The item7.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
            item7 = Item7;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3, Item4, Item5, Item6, Item7 };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }

    /// <summary>
    /// Value tuple with 6 items
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    /// <typeparam name="T4">The type of the 4.</typeparam>
    /// <typeparam name="T5">The type of the 5.</typeparam>
    /// <typeparam name="T6">The type of the 6.</typeparam>
    /// <typeparam name="T7">The type of the 7.</typeparam>
    /// <typeparam name="T8">The type of the 8.</typeparam>
    /// <seealso cref="System.IValueTuple" />
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IValueTuple
    where TRest : struct
    {
        /// <summary>
        /// Gets the item1.
        /// </summary>
        /// <value>
        /// The item1.
        /// </value>
        public readonly T1 Item1;

        /// <summary>
        /// Gets the item2.
        /// </summary>
        /// <value>
        /// The item2.
        /// </value>
        public readonly T2 Item2;

        /// <summary>
        /// Gets the item3.
        /// </summary>
        /// <value>
        /// The item3.
        /// </value>
        public readonly T3 Item3;

        /// <summary>
        /// Gets the item4.
        /// </summary>
        /// <value>
        /// The item4.
        /// </value>
        public readonly T4 Item4;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T5 Item5;

        /// <summary>
        /// The item5
        /// </summary>
        public readonly T6 Item6;

        /// <summary>
        /// The item7
        /// </summary>
        public readonly T7 Item7;

        /// <summary>
        /// The item8
        /// </summary>
        public readonly TRest Rest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}" /> struct.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        /// <param name="item7">The item7.</param>
        /// <param name="rest">The item8.</param>
        [DebuggerStepThrough]
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Rest = rest;
        }

        /// <summary>
        /// Deconstructs the specified items.
        /// </summary>
        /// <param name="item1">The item1.</param>
        /// <param name="item2">The item2.</param>
        /// <param name="item3">The item3.</param>
        /// <param name="item4">The item4.</param>
        /// <param name="item5">The item5.</param>
        /// <param name="item6">The item6.</param>
        /// <param name="item7">The item7.</param>
        /// <param name="rest">The item8.</param>
        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out TRest rest)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
            item7 = Item7;
            rest = Rest;
        }

        IEnumerable IValueTuple.Items => new object[] { Item1, Item2, Item3, Item4, Item5, Item6, Item7, Rest };

        /// <inheritdoc />
        public override int GetHashCode() => ValueTupleExtensions.GetHashCode(this);

        /// <inheritdoc />
        public override bool Equals(object obj) => ValueTupleExtensions.Equals(this, obj as IValueTuple);
    }
}

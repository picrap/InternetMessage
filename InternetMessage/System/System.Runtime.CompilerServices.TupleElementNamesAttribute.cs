
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    using Collections.Generic;

    /// <summary>
    /// Something that I didn't understand
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event |
                    AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    internal sealed class TupleElementNamesAttribute : Attribute
    {
        private readonly string[] _transformNames;

        /// <summary>
        ///     Gets the transform names.
        /// </summary>
        /// <value>
        ///     The transform names.
        /// </value>
        public IList<string> TransformNames => _transformNames;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TupleElementNamesAttribute" /> class.
        /// </summary>
        /// <param name="transformNames">The transform names.</param>
        /// <exception cref="ArgumentNullException">transformNames</exception>
        public TupleElementNamesAttribute(string[] transformNames)
        {
            if (transformNames is null)
                throw new ArgumentNullException(nameof(transformNames));
            _transformNames = transformNames;
        }
    }
}
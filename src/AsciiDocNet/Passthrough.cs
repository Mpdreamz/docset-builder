namespace AsciiDocNet
{
    /// <summary>
    /// A passthrough element.
    /// <para></para>
    /// Unprocessed content that is sent directly to the output
    /// </summary>
    /// <example>
    /// ++++
    /// This will be sent directly to the output
    /// ++++
    /// </example>
    /// <example>
    /// [pass]
    /// This will also be sent to the output
    /// </example>
    /// <seealso cref="AsciiDocNet.IElement" />
    /// <seealso cref="AsciiDocNet.IText" />
    /// <seealso cref="AsciiDocNet.IAttributable" />
    public class Passthrough : IElement, IText, IAttributable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Passthrough"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Passthrough(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Passthrough"/> class.
        /// </summary>
        public Passthrough()
        {
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public AttributeList Attributes { get; } = new AttributeList();

        /// <summary>
        /// Gets or sets the parent element
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Container Parent { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Passthrough left, Passthrough right) => Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Passthrough left, Passthrough right) => !Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && Equals((Passthrough)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Attributes.GetHashCode() * 397) ^ Text.GetHashCode();
            }
        }

        /// <summary>
        /// Accepts a visitor to visit this element instance
        /// </summary>
        /// <typeparam name="TVisitor">The type of the visitor.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <returns>
        /// The visitor
        /// </returns>
        public TVisitor Accept<TVisitor>(TVisitor visitor) where TVisitor : IDocumentVisitor
        {
            visitor.VisitPassthrough(this);
            return visitor;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Passthrough" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>true if equal; otherwise, false</returns>
        protected bool Equals(Passthrough other) =>
            Attributes.Equals(other.Attributes) &&
            string.Equals(Text, other.Text);
    }
}
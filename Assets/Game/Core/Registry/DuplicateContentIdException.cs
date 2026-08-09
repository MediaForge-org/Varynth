using System;
using Varynth.Core.Common;

namespace Varynth.Core.Registry
{
    /// <summary>
    /// Thrown by <see cref="ContentRegistry{T}"/>.Register when the id is already
    /// registered. Registration never silently overwrites an existing entry.
    /// </summary>
    public sealed class DuplicateContentIdException : InvalidOperationException
    {
        public ContentId Id { get; }

        public DuplicateContentIdException(ContentId id)
            : base($"A content definition with id '{id}' is already registered.")
        {
            Id = id;
        }
    }
}

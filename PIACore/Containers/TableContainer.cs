using PIACore.Kernel;
using PIACore.Model;

namespace PIACore.Containers
{
    /// <summary>
    /// A table container is the association of a Table, an implementation of an AiManager and a TableId.
    /// </summary>
    public class TableContainer
    {
        /// <summary>
        /// The implementation of the AI for this Container
        /// </summary>
        public IAiManager AiManager { get; set; }

        /// <summary>
        /// The Table for this container
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// The id key of the table for this container
        /// </summary>
        public string TableId { get; set; }
    }
}
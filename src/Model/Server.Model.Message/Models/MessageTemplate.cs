using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Framework.Generic;

namespace odec.Server.Model.Message
{
    /// <summary>
    /// Message Template
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class MessageTemplate<TKey> : Glossary<TKey>
    {
        /// <summary>
        /// Message Body
        /// </summary>
        [Required]
        [StringLength(5000)]
        public string Body { get; set; }

        /// <summary>
        /// Message Theme
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [Column("Header")]
        [StringLength(250)]
        public override string Name { get; set; }
    }
}

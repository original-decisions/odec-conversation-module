using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Conversation
{
    /// <summary>
    /// Conversation 
    /// </summary>
    public class Conversation:Glossary<int>
    {
        /// <summary>
        /// Conversation Theme
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public override string Name { get; set; }

        /// <summary>
        /// Identity of Conversation type
        /// </summary>
        public int ConversationTypeId { get; set; }

        /// <summary>
        /// Conversation type
        /// </summary>
        public ConversationType ConversationType { get; set; }

        public int UserStartedId { get; set; }
        public User.User UserStarted { get; set; }
    }
}

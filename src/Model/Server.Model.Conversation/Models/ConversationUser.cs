using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Conversation
{
    /// <summary>
    /// Users participating in Conversation
    /// </summary>
    public class ConversationUser
    {
        /// <summary>
        /// User identity
        /// </summary>
      //  [Key,Column(Order = 0)]
        public int UserId { get; set; }
        
        /// <summary>
        /// User
        /// </summary>
        public User.User User { get; set; }
       
        /// <summary>
        /// Conversation identity
        /// </summary>
    //    [Key, Column(Order = 1)]
        public int ConversationId { get; set; }

        /// <summary>
        /// Conversation
        /// </summary>
        public Conversation Conversation { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }

        
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Conversation
{
    /// <summary>
    /// Conversation posted messages
    /// </summary>
    public class ConversationMessage
    {
        /// <summary>
        /// Conversation identity
        /// </summary>
    //    [Key,Column(Order = 0)]
        public int ConversationId { get; set; }

        /// <summary>
        /// серверный объект - конференция (обсуждение)
        /// </summary>
        public Conversation Conversation { get; set; }
       
        /// <summary>
        ///Posted message Identity
        /// </summary>
    //    [Key, Column(Order = 1)]
        public int MessageId { get; set; }

        /// <summary>
        /// Posted message
        /// </summary>
        public Message.Message Message { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime DatePosted { get; set; }
    }
}

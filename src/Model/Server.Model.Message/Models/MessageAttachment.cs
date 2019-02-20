using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Message
{
    /// <summary>
    /// серверный объект - связь сообщения и вложения
    /// </summary>
    public class MessageAttachment
    {
        /// <summary>
        /// идентификатор сообщения
        /// </summary>
   //     [Key,Column(Order = 0)]
        public int MessageId { get; set; }
        
        /// <summary>
        /// идентификатор вложения
        /// </summary>
   //     [Key, Column(Order = 1)]
        public int AttachmentId { get; set; }
    }
}

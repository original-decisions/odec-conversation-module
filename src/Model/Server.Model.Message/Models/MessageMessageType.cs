using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Message
{
    /// <summary>
    /// серверный объект - связь сообщения и типа сообщения
    /// </summary>
    public class MessageMessageType
    {
        /// <summary>
        /// идентификатор сообщения
        /// </summary>
        [Key,Column(Order = 0)]
        public int MessageId { get; set; }

        /// <summary>
        /// серверный объект - сообщение
        /// </summary>
        public Message Message { get; set; }
        
        /// <summary>
        /// идентификатор типа сообщения
        /// </summary>
        [Key, Column(Order = 1)]
        public int MessageTypeId { get; set; }
       
        /// <summary>
        /// серверный объект - тип сообщения
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}

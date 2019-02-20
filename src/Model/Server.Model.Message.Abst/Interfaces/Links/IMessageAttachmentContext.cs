using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Message.Abst.Interfaces.Links
{
    /// <summary>
    /// Прокси объект контекста связи сообщения и вложения
    /// </summary>
    /// <typeparam name="TMessageAttachment">тип связи сообщения и вложения</typeparam>
    public interface IMessageAttachmentContext<TMessageAttachment> where TMessageAttachment : class
    {
        /// <summary>
        /// таблица связи сообщения и вложений
        /// </summary>
        DbSet<TMessageAttachment> MessageAttachments { get; set; }

    }

}

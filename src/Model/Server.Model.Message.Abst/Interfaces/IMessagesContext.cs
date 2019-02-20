
using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Message.Abst.Interfaces
{
    /// <summary>
    /// Прокси объект контекста связи сообщений
    /// </summary>
    /// <typeparam name="TMessage">тип сообщения</typeparam>
    /// <typeparam name="TMessageMessageType">тип связи типа сообщения и сообщения</typeparam>
    /// <typeparam name="TMessageTypes">тип типа сообщения</typeparam>
    public interface IMessagesContext<TMessage, TMessageMessageType, TMessageTypes> where TMessage : class where TMessageMessageType : class where TMessageTypes : class
    {
        /// <summary>
        /// таблица связи сообщений
        /// </summary>
        DbSet<TMessage> Messages { get; set; }  
      
        /// <summary>
        /// таблица связи связи типа сообщения и сообщения
        /// </summary>
        DbSet<TMessageMessageType> MessageMessageTypes { get; set; }

        /// <summary>
        /// таблица связи типа сообщения
        /// </summary>
        DbSet<TMessageTypes> MessageTypeses { get; set; }
    }
}

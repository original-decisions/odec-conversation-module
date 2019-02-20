using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Conversation.Abstractions
{
    /// <summary>
    /// Прокси объект контекста обсуждения
    /// </summary>
    /// <typeparam name="TConversation">тип обсуждения</typeparam>
    /// <typeparam name="TConversationType">тип типа обсуждения</typeparam>
    /// <typeparam name="TConversationUser">тип обсуждения пользователя</typeparam>
    /// <typeparam name="TConversationMessage">тип обсуждения - сообщение</typeparam>
    public interface IConversationContext<TConversation, TConversationType, TConversationUser, TConversationMessage> where TConversation : class where TConversationType : class where TConversationUser : class where TConversationMessage : class
    {
        /// <summary>
        /// таблица связи обсуждений
        /// </summary>
        DbSet<TConversation> Conversations { get; set; }

        /// <summary>
        /// таблица связи типов обсуждений
        /// </summary>
        DbSet<TConversationType> ConversationTypes { get; set; }

        /// <summary>
        /// таблица связи обсуждения и пользователей
        /// </summary>
        DbSet<TConversationUser> ConversationUsers { get; set; }

        /// <summary>
        /// таблица связи обсуждения и сообщений
        /// </summary>
        DbSet<TConversationMessage> ConversationMessages { get; set; }
    }
}

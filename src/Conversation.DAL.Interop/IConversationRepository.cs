using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Conversation.DAL.Interop
{
    public interface IConversationRepository<in TKey,TContext,TConversation, TMessage, TUser, in TConversationFilter>: 
        IEntityOperations<TKey,TConversation>,
        IContextRepository<TContext>,
        IActivatableEntity<int,TConversation> 
        where TKey : struct 
        where TConversation : class
    {
        IEnumerable<TConversation> Get(TConversationFilter filter);
        void StartConversation(TConversation conversation, IEnumerable<TUser> members, TMessage message,TKey userStartedId);
        void AddMessage(TMessage message, TConversation conversation,TUser user);
        void RemoveMessage(TMessage message, TConversation conversation);
        void AddMember(TUser user, TConversation conversation);
        void RemoveMember(TUser user, TConversation conversation);
        IEnumerable<TMessage> GetMessages(TKey conversationId);
        IEnumerable<TMessage> GetMessages(TConversation conversation);
        IEnumerable<TUser> GetMembers(TKey conversationId);
        IEnumerable<TUser> GetMembers(TConversation conversation);
    }
}
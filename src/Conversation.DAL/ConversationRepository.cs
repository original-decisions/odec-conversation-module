using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Conversation.DAL.Interop;
using odec.Entity.DAL;
using odec.Framework.Extensions;
using odec.Framework.Logging;
using odec.Server.Model.Conversation;
using odec.Server.Model.Conversation.Filter;
using odec.Server.Model.Message;
using odec.Server.Model.User;
#if net452
using System.Transactions;
#endif
namespace odec.Conversation.DAL
{
    public class ConversationRepository : OrmEntityOperationsRepository<int, Server.Model.Conversation.Conversation, DbContext>,
        IConversationRepository<int, DbContext, Server.Model.Conversation.Conversation, Message, User, ConversationFilter<int>>
    {
        public ConversationRepository()
        {

        }

        public ConversationRepository(DbContext db)
        {
            Db = db;
        }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public IEnumerable<Server.Model.Conversation.Conversation> Get(ConversationFilter<int> filter)
        {
            try
            {
                var query = from conversation in Db.Set<Server.Model.Conversation.Conversation>()
                            join conversationUser in Db.Set<ConversationUser>() on conversation.Id equals conversationUser.ConversationId
                            where filter.UserId == conversationUser.UserId && conversation.ConversationTypeId == filter.ConversationTypeId
                            select conversation;

                if (!string.IsNullOrEmpty(filter.Code))
                    query = query.Where(it => it.Code == filter.Code);

                if (filter.DateCreatedInterval != null && filter.DateCreatedInterval.Start.HasValue)
                    query = query.Where(it => it.DateCreated >= filter.DateCreatedInterval.Start);
                if (filter.DateCreatedInterval != null && filter.DateCreatedInterval.End.HasValue)
                    query = query.Where(it => it.DateCreated <= filter.DateCreatedInterval.End);


                //Case sensetive contains
                if (!string.IsNullOrEmpty(filter.Title))
                    query = query.Where(it => it.Name.ToUpper().Contains(filter.Title.ToUpper()));

                query = query.Include(it => it.UserStarted).Include(it => it.ConversationType);


                query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(filter.Sidx)
                    : query.OrderBy(filter.Sidx);
                return query.Skip(filter.Rows * (filter.Page - 1)).Take(filter.Rows).Distinct();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void StartConversation(Server.Model.Conversation.Conversation conversation, IEnumerable<User> members, Message message, int userStartedId)
        {
            try
            {
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                if (Exists(conversation, e => e.Id == conversation.Id)) return;
                SaveById(conversation);

                foreach (var member in members)
                    AddMember(member, conversation);

                AddMessage(message, conversation, members.First(it => it.Id == userStartedId));
#if net452
                scope.Complete();
            }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddMessage(Message message, Server.Model.Conversation.Conversation conversation, User user)
        {
            try
            {
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                message.UserId = user.Id;
                var convM = new ConversationMessage
                {
                    Message = message,
                    ConversationId = conversation.Id,
                };
                if (!Exists(convM, e => e.ConversationId == conversation.Id && e.MessageId == message.Id))
                {
                    Db.Set<ConversationMessage>().Add(convM);
                    Db.SaveChanges();
                }
#if net452
                    scope.Complete();
                }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveMessage(Message message, Server.Model.Conversation.Conversation conversation)
        {
            try
            {
                var convM = Db.Set<ConversationMessage>().SingleOrDefault(e => e.ConversationId == conversation.Id && e.MessageId == message.Id);
                if (convM == null)
                    return;
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                Db.Set<ConversationMessage>().Remove(convM);
                Db.SaveChanges();
                if (!Db.Set<ConversationMessage>().Any(it => it.MessageId == message.Id))
                    Delete<int, Message>(message.Id);
#if net452
                    scope.Complete();
                }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddMember(User user, Server.Model.Conversation.Conversation conversation)
        {
            try
            {
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                var convM = new ConversationUser
                {
                    UserId = user.Id,
                    ConversationId = conversation.Id,
                    JoinDate = DateTime.Now
                };
                if (!Exists(convM, e => e.ConversationId == conversation.Id && e.UserId == user.Id))
                {
                    Db.Set<ConversationUser>().Add(convM);
                    Db.SaveChanges();
                }
#if net452
                    scope.Complete();
                }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveMember(User user, Server.Model.Conversation.Conversation conversation)
        {
            try
            {
                var convU = Db.Set<ConversationUser>().SingleOrDefault(e => e.ConversationId == conversation.Id && e.UserId == user.Id);
                if (convU == null)
                    return;
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                Db.Set<ConversationUser>().Remove(convU);
                Db.SaveChanges();
#if net452
                    scope.Complete();
                }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public override void Save(Server.Model.Conversation.Conversation entity)
        {
            if (entity.ConversationTypeId == 0 && entity.ConversationType != null && entity.ConversationTypeId !=0)
            {
                entity.ConversationTypeId = entity.ConversationType.Id;
            }
            if (entity.ConversationTypeId == 0 && entity.ConversationType != null && entity.ConversationTypeId == 0)
            {
                AddOrUpdate(entity.ConversationType, e => e.Code == entity.Code);
            }
           
            base.Save(entity);
        }

        public override void SaveById(Server.Model.Conversation.Conversation entity)
        {
            if (entity.ConversationTypeId == 0 && entity.ConversationType != null && entity.ConversationTypeId != 0)
            {
                entity.ConversationTypeId = entity.ConversationType.Id;
            }
            if (entity.ConversationTypeId == 0 && entity.ConversationType != null && entity.ConversationTypeId == 0)
            {
                AddOrUpdate(entity.ConversationType, e => e.Id == entity.Id);
            }
            base.SaveById(entity);
        }

        public IEnumerable<Message> GetMessages(int conversationId)
        {
            try
            {
                return
                    Db.Set<ConversationMessage>()
                        .Include(it => it.Message)
                        .Where(it => it.ConversationId == conversationId).Select(it => it.Message)
                        .OrderBy(it => it.DateCreated);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Message> GetMessages(Server.Model.Conversation.Conversation conversation)
        {
            try
            {
                return GetMessages(conversation.Id);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<User> GetMembers(int conversationId)
        {
            try
            {
                var res = 
                    from convUser in Db.Set<ConversationUser>()
                    join user in Db.Set<User>() on convUser.UserId equals user.Id
                    where convUser.ConversationId == conversationId
                    select user;

                return res;
                //Db.Set<ConversationUser>()
                //    .Include(it => it.User)
                //    .Where(it => it.ConversationId == conversationId)
                //    .OrderBy(it => it.JoinDate)
                //    .Select(it => it.User);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<User> GetMembers(Server.Model.Conversation.Conversation conversation)
        {
            try
            {
                return GetMembers(conversation.Id);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public override void Delete(Server.Model.Conversation.Conversation conversation)
        {
            Delete(conversation.Id);
        }
        public override void Delete(int conversationId)
        {
            try
            {
#if net452
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2)
                }))
                {
#endif
                var conv = GetById(conversationId);
                foreach (var message in GetMessages(conversationId))
                    RemoveMessage(message, conv);
                foreach (var member in GetMembers(conversationId))
                    RemoveMember(member, conv);
                Delete<Server.Model.Conversation.Conversation>(conv);
#if net452
                    scope.Complete();
                }
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}

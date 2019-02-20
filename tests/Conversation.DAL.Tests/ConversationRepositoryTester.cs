
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using odec.Conversation.DAL;
using odec.Framework.Generic.Utility;
using odec.Framework.Logging;
using odec.Server.Model.Conversation;
using odec.Server.Model.Conversation.Contexts;
using odec.Server.Model.Conversation.Filter;
using odec.Server.Model.Message;
using odec.Server.Model.User;
//using ConversationRepo = odec.Conversation.DAL.Interop.IConversationRepository<int, Microsoft.EntityFrameworkCore.DbContext, odec.Server.Model.Conversation.Conversation, odec.Server.Model.Message.Message, odec.Server.Model.User.User, odec.Server.Model.Conversation.Filter.ConversationFilter<int>>;
using Conv = odec.Server.Model.Conversation.Conversation;
namespace Conversation.DAL.Tests
{
    public class ConversationRepositoryTester : Tester<ConversationContext>
    {
        private Conv GenerateModel()
        {
            return new Conv
            {
                Name = "My Conversation",
                Code = "Conv1",
                IsActive = true,
                DateCreated = DateTime.Now,
                UserStartedId = 1,
                ConversationType = new ConversationType
                {
                    Name = "ConvType1",
                    Code = "Test",
                    DateCreated = DateTime.Now,
                    SortOrder = 0,
                    IsActive = true
                },
                SortOrder = 0,
            };
        }

        private Message GenerateMessage()
        {
            return new Message
            {
                Name = "Message",
                Code = "MMM",
                Body = "Message Body",
                UserId = 1,
                SortOrder = 0,
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        [Test]
        public void SaveConversation()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository =
                            new ConversationRepository(db); ;

                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Delete()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db); ;
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void Delete2()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    var mess = GenerateMessage();
                    var members = new List<User> { new User { Id = 1 }, new User { Id = 2 } };
                    Assert.DoesNotThrow(() => repository.StartConversation(item, members, mess, members.First().Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeleteById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item.Id));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Deactivate()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Deactivate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Deactivate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Activate()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Activate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Activate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));

                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void Get()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    IEnumerable<Conv> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMember(new User { Id = 1 }, item));
                    var filter = new ConversationFilter<int>
                    {
                        UserId = item.UserStartedId,
                        ConversationTypeId = item.ConversationTypeId,
                    };
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                    filter.Code = "Conv1";
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                    filter.Title = "Conversation";
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                    filter.DateCreatedInterval = new Interval<DateTime?>
                    {
                        Start = DateTime.Now.AddDays(-2)
                    };
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                    filter.DateCreatedInterval.Start = null;
                    filter.DateCreatedInterval.End = DateTime.Now.AddDays(2);
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                    filter.DateCreatedInterval.Start = DateTime.Now.AddDays(-2);
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void StartConversation()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }

                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    var mess = GenerateMessage();
                    var members = new List<User> { new User { Id = 1 }, new User { Id = 2 } };
                    Assert.DoesNotThrow(() => repository.StartConversation(item, members, mess, members.First().Id));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void AddMessage()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    var mess = GenerateMessage();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMessage(mess, item, new User { Id = 1 }));

                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RemoveMessage()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    var mess = GenerateMessage();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMessage(mess, item, new User { Id = 1 }));
                    Assert.DoesNotThrow(() => repository.RemoveMessage(mess, item));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void AddMember()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);

                    var item = GenerateModel();
                    var member = new User { Id = 1 };
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMember(member, item));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RemoveMember()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    var member = new User { Id = 1 };
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMember(member, item));
                    Assert.DoesNotThrow(() => repository.RemoveMember(member, item));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetMessages()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    var member = new User { Id = 1 };

                    var mess = GenerateMessage();
                    IEnumerable<Message> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMessage(mess, item, member));
                    Assert.DoesNotThrow(() => result = repository.GetMessages(item));
                    Assert.True(result != null && result.Any());
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetMessages2()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    var member = new User { Id = 1 };

                    var mess = GenerateMessage();
                    IEnumerable<Message> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMessage(mess, item, member));
                    Assert.DoesNotThrow(() => result = repository.GetMessages(item.Id));
                    Assert.True(result != null && result.Any());
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetMembers()
        {
            try
            {

                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    var member = new User { Id = 1 };
                    IEnumerable<User> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMember(member, item));
                    Assert.DoesNotThrow(() => result = repository.GetMembers(item));
                    Assert.True(result != null && result.Any());
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetMembers2()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new ConversationContext(options))
                {
                    ConversationTestHelper.PopulateDefaultDataCtx(db);
                }
                using (var db = new ConversationContext(options))
                {
                    var repository = new ConversationRepository(db);
                    var item = GenerateModel();
                    var member = new User { Id = 1 };
                    IEnumerable<User> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.AddMember(member, item));
                    Assert.DoesNotThrow(() => result = repository.GetMembers(item.Id));
                    Assert.True(result != null && result.Any());
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}

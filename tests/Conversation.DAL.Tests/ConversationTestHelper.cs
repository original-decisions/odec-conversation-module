using System;
using Microsoft.EntityFrameworkCore;
using odec.Framework.Logging;
using odec.Server.Model.User;

namespace Conversation.DAL.Tests
{
    public static class ConversationTestHelper
    {
        internal static void PopulateDefaultDataCtx(DbContext db)
        {
            try
            {
                db.Set<Role>().Add(new Role
                {
                    Id = 1,
                    Name = "Crafter"

                });
                db.Set<Role>().Add(new Role
                {
                    Id = 2,
                    Name = "User",
                });
                db.Set<UserRole>().Add(new UserRole {RoleId = 1,UserId = 1});
               
                db.Set<User>().Add(new User
                {
                    Id = 1,
                    UserName = "Andrew",

                });
                db.Set<User>().Add(new User
                {
                    Id = 2,
                    UserName = "Alex",
                });
              

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }
      
    }
}

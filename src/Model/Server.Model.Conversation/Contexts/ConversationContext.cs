using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Conversation.Abstractions;
using odec.Server.Model.Message;
using odec.Server.Model.User;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Usr = odec.Server.Model.User.User;
using Msg = odec.Server.Model.Message.Message;
namespace odec.Server.Model.Conversation.Contexts
{
    public class ConversationContext: DbContext,
        //IdentityDbContext<Usr, Role, int, UserClaim, UserRole, UserLogin, IdentityRoleClaim<int>, UserToken>, 
        IConversationContext<Conversation,ConversationType,ConversationUser,ConversationMessage>
    {
        private string MembershipScheme = "AspNet";
        private string ConversationScheme = "conv";
        public ConversationContext(DbContextOptions<ConversationContext> options)
            : base(options)
        {

        }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationType> ConversationTypes { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        public DbSet<Msg> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Usr>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRoles", MembershipScheme)
                .HasKey(it=>new {it.UserId,it.RoleId});
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", MembershipScheme);
            //modelBuilder.Entity<UserClaim>().ToTable("UserClaims", MembershipScheme);
            //modelBuilder.Entity<UserLogin>().ToTable("UserLogins", MembershipScheme);
            //modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", MembershipScheme);
            //modelBuilder.Entity<UserToken>().ToTable("UserTokens", MembershipScheme);

            modelBuilder.Entity<Msg>().ToTable("Messages", ConversationScheme);
            modelBuilder.Entity<Conversation>().ToTable("Conversations", ConversationScheme);
            modelBuilder.Entity<ConversationType>().ToTable("ConversationTypes", ConversationScheme);
            modelBuilder.Entity<ConversationUser>()
                .ToTable("ConversationUsers", ConversationScheme)
                .HasKey(it => new { it.ConversationId, it.UserId }); ;
            modelBuilder.Entity<ConversationMessage>()
                .ToTable("ConversationMessages", ConversationScheme)
                .HasKey(it=>new {it.ConversationId,it.MessageId});
            modelBuilder.Entity<MessageAttachment>()
                .ToTable("MessageAttachments", ConversationScheme)
                .HasKey(it => new { it.AttachmentId, it.MessageId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}

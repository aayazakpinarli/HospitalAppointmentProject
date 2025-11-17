using Microsoft.EntityFrameworkCore;

namespace Users.APP.Domain
{
    public class UsersDb : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public UsersDb(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Username should be unique.
            modelBuilder.Entity<User>().HasIndex(userEntity => userEntity.UserName).IsUnique();

            modelBuilder.Entity<Role>().HasIndex(roleEntity => roleEntity.Name).IsUnique();


            // define indices for optimizing query performance on frequently searched propertes
            modelBuilder.Entity<UserRole>().HasIndex(userRoleEntity => userRoleEntity.UserId);
            modelBuilder.Entity<UserRole>().HasIndex(userRoleEntity => userRoleEntity.RoleId);
            modelBuilder.Entity<User>().HasIndex(userEntity => new { userEntity.FirstName, userEntity.LastName });


            // Relationship configurations
            // ----------------------------

            // FK relationships and delete behaviors

            modelBuilder.Entity<UserRole>()
                .HasOne(userRoleEntity => userRoleEntity.User)
                .WithMany(userEntity => userEntity.UserRoles)
                .HasForeignKey(userRoleEntity => userRoleEntity.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserRole>()
                .HasOne(userRoleEntity => userRoleEntity.Role)
                .WithMany(roleEntity => roleEntity.UserRoles)
                .HasForeignKey(userRoleEntity => userRoleEntity.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(userEntity => userEntity.Group) 
                .WithMany(groupEntity => groupEntity.Users) 
                .HasForeignKey(userEntity => userEntity.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

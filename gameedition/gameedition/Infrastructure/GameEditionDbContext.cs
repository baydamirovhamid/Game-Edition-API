using game.edition.api.Models;
using game.payment.service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace game.edition.api.Infrastructure
{
    public class GameEditionDbContext: IdentityDbContext<USER, USER_ROLE, int>
    {
        public GameEditionDbContext()
        {
        }

        public GameEditionDbContext(DbContextOptions<GameEditionDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("students");
           
        }

        public DbSet<USER> hb_user { get; set; }
        public DbSet<USER_ROLE> hb_user_role { get; set; }
        public DbSet<CUSTOMER> hb_customer { get; set; }
        public DbSet<COMPANY> hb_company { get; set; }
        public DbSet<PLATFORM> hb_platform { get; set; }
        public DbSet<GAME_PLATFORM> hb_game_platform { get; set; }
        public DbSet<BASKET> hb_basket { get; set; }
        public DbSet<GAME> hb_game { get; set; }
        public DbSet<GAME_COMPANY> hb_game_company { get; set; }
        public DbSet<STATIC_DATA> hb_static_data { get; set; }
    }
}

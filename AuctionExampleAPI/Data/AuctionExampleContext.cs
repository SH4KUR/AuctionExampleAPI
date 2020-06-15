using AuctionExampleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionExampleAPI.Data
{
    public partial class AuctionExampleContext : DbContext
    {
        public AuctionExampleContext()
        {
        }

        public AuctionExampleContext(DbContextOptions<AuctionExampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseNpgsql("Host=localhost;Port=5432;Database=AuctionExample;Username=postgres;Password=dmitriy3452zz");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasDefaultValueSql("nextval('item_id_seq'::regclass)");

                entity.Property(e => e.CurrentPrice).HasColumnName("current_price");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.StartPrice).HasColumnName("start_price");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("rate");

                entity.Property(e => e.RateId).HasColumnName("rate_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RateTime).HasColumnName("rate_time");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Rate)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rate_item_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

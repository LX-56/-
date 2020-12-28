using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutomobileSalesSystem.Entitys
{
    public partial class car_sales_dbContext : DbContext
    {
        public car_sales_dbContext()
        {
        }

        public car_sales_dbContext(DbContextOptions<car_sales_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Drivetest> Drivetest { get; set; }
        public virtual DbSet<Market> Market { get; set; }
        public virtual DbSet<Salesman> Salesman { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=131413GH...;database=car_sales_db", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datatime).HasColumnName("datatime");

                entity.Property(e => e.Factory)
                    .IsRequired()
                    .HasColumnName("factory")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(24)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Drivetest>(entity =>
            {
                entity.HasKey(e => e.Did)
                    .HasName("PRIMARY");

                entity.ToTable("drivetest");

                entity.HasIndex(e => e.Aid)
                    .HasName("FK_Reference_2");

                entity.HasIndex(e => e.Cid)
                    .HasName("FK_Reference_1");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Testtime)
                    .HasColumnName("testtime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.A)
                    .WithMany(p => p.Drivetest)
                    .HasForeignKey(d => d.Aid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_2");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Drivetest)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_1");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.ToTable("market");

                entity.HasIndex(e => e.Aid)
                    .HasName("FK_Reference_5");

                entity.HasIndex(e => e.Cid)
                    .HasName("FK_Reference_3");

                entity.HasIndex(e => e.Sid)
                    .HasName("FK_Reference_4");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Saledatatime).HasColumnType("datetime");

                entity.HasOne(d => d.A)
                    .WithMany(p => p.Market)
                    .HasForeignKey(d => d.Aid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_5");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Market)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_3");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Market)
                    .HasForeignKey(d => d.Sid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_4");
            });

            modelBuilder.Entity<Salesman>(entity =>
            {
                entity.ToTable("salesman");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(5,0)");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

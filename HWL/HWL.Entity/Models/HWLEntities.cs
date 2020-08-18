using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HWL.Entity.Models
{
    public partial class HWLEntities : DbContext
    {
        public HWLEntities()
        {
        }

        public HWLEntities(DbContextOptions<HWLEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<t_admin> t_admin { get; set; }
        public virtual DbSet<t_app_log> t_app_log { get; set; }
        public virtual DbSet<t_app_version> t_app_version { get; set; }
        public virtual DbSet<t_circle> t_circle { get; set; }
        public virtual DbSet<t_circle_comment> t_circle_comment { get; set; }
        public virtual DbSet<t_circle_like> t_circle_like { get; set; }
        public virtual DbSet<t_city> t_city { get; set; }
        public virtual DbSet<t_country> t_country { get; set; }
        public virtual DbSet<t_district> t_district { get; set; }
        public virtual DbSet<t_group> t_group { get; set; }
        public virtual DbSet<t_group_user> t_group_user { get; set; }
        public virtual DbSet<t_near_circle> t_near_circle { get; set; }
        public virtual DbSet<t_near_circle_comment> t_near_circle_comment { get; set; }
        public virtual DbSet<t_near_circle_like> t_near_circle_like { get; set; }
        public virtual DbSet<t_province> t_province { get; set; }
        public virtual DbSet<t_user> t_user { get; set; }
        public virtual DbSet<t_user_code> t_user_code { get; set; }
        public virtual DbSet<t_user_friend> t_user_friend { get; set; }
        public virtual DbSet<t_user_pos> t_user_pos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_admin>(entity =>
            {
                entity.Property(e => e.create_date).HasColumnType("datetime");

                entity.Property(e => e.login_name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.login_pwd)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.real_name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<t_app_log>(entity =>
            {
                entity.Property(e => e.app_type).HasMaxLength(50);

                entity.Property(e => e.app_version)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.crash_details).HasColumnType("text");

                entity.Property(e => e.crash_info).HasMaxLength(200);

                entity.Property(e => e.create_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_app_version>(entity =>
            {
                entity.Property(e => e.app_name).HasMaxLength(50);

                entity.Property(e => e.app_version)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.download_url)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.publish_time).HasColumnType("datetime");

                entity.Property(e => e.update_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_circle>(entity =>
            {
                entity.Property(e => e.content_info).HasMaxLength(2000);

                entity.Property(e => e.image_urls).HasMaxLength(2000);

                entity.Property(e => e.link_image).HasMaxLength(200);

                entity.Property(e => e.link_title).HasMaxLength(300);

                entity.Property(e => e.link_url).HasMaxLength(200);

                entity.Property(e => e.pos_desc).HasMaxLength(50);

                entity.Property(e => e.publish_time).HasColumnType("datetime");

                entity.Property(e => e.update_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_circle_comment>(entity =>
            {
                entity.Property(e => e.com_content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.comment_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_circle_like>(entity =>
            {
                entity.Property(e => e.like_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_city>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_country>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_district>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_group>(entity =>
            {
                entity.Property(e => e.build_date).HasColumnType("datetime");

                entity.Property(e => e.group_guid)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.group_name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.group_note).HasMaxLength(200);

                entity.Property(e => e.update_date).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_group_user>(entity =>
            {
                entity.Property(e => e.add_date).HasColumnType("datetime");

                entity.Property(e => e.group_guid)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_near_circle>(entity =>
            {
                entity.Property(e => e.content_info).HasMaxLength(2000);

                entity.Property(e => e.image_urls).HasMaxLength(2000);

                entity.Property(e => e.link_image).HasMaxLength(200);

                entity.Property(e => e.link_title).HasMaxLength(100);

                entity.Property(e => e.link_url).HasMaxLength(200);

                entity.Property(e => e.pos_desc).HasMaxLength(50);

                entity.Property(e => e.publish_time).HasColumnType("datetime");

                entity.Property(e => e.update_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_near_circle_comment>(entity =>
            {
                entity.Property(e => e.comment_time).HasColumnType("datetime");

                entity.Property(e => e.content_info).HasMaxLength(500);
            });

            modelBuilder.Entity<t_near_circle_like>(entity =>
            {
                entity.Property(e => e.like_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_province>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_user>(entity =>
            {
                entity.Property(e => e.circle_back_image)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.head_image).HasMaxLength(100);

                entity.Property(e => e.life_notes).HasMaxLength(200);

                entity.Property(e => e.mobile)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.name).HasMaxLength(30);

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.register_date).HasColumnType("datetime");

                entity.Property(e => e.source).HasDefaultValueSql("((1))");

                entity.Property(e => e.symbol).HasMaxLength(20);

                entity.Property(e => e.update_date).HasColumnType("datetime");
            });

            modelBuilder.Entity<t_user_code>(entity =>
            {
                entity.Property(e => e.code)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.create_date).HasColumnType("datetime");

                entity.Property(e => e.expire_time).HasColumnType("datetime");

                entity.Property(e => e.remark).HasMaxLength(100);

                entity.Property(e => e.user_account)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<t_user_friend>(entity =>
            {
                entity.Property(e => e.add_time).HasColumnType("datetime");

                entity.Property(e => e.friend_first_spell).HasMaxLength(2);

                entity.Property(e => e.friend_user_remark).HasMaxLength(50);
            });

            modelBuilder.Entity<t_user_pos>(entity =>
            {
                entity.Property(e => e.create_date).HasColumnType("datetime");

                entity.Property(e => e.geohash_key)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.pos_details)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.update_date).HasColumnType("datetime");
            });
        }
    }
}

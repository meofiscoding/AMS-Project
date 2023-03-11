﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.DataAccess
{
    public partial class AMSContext : DbContext
    {
        public AMSContext()
        {
        }

        public AMSContext(DbContextOptions<AMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassStudent> ClassStudents { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupStudent> GroupStudents { get; set; } = null!;
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Submission> Submissions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AMS"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignmentDeadline)
                    .HasColumnType("datetime")
                    .HasColumnName("assignment_deadline");

                entity.Property(e => e.AssignmentDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("assignment_description");

                entity.Property(e => e.AssignmentName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("assignment_name");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Assignmen__class__5070F446");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Assignmen__group__5165187F");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Assignmen__teach__4F7CD00D");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Assignments)
                    .UsingEntity<Dictionary<string, object>>(
                        "AssignmentResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Assignmen__resou__5CD6CB2B"),
                        r => r.HasOne<Assignment>().WithMany().HasForeignKey("AssignmentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Assignmen__assig__5BE2A6F2"),
                        j =>
                        {
                            j.HasKey("AssignmentId", "ResourceId").HasName("PK__Assignme__FE1147D37C68B035");

                            j.ToTable("Assignment_Resource");

                            j.IndexerProperty<int>("AssignmentId").HasColumnName("assignment_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.ChatId).ValueGeneratedNever();

                entity.Property(e => e.FilePath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("file_path");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.SentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("sent_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Chat__group_id__6B24EA82");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Chat__receiver_i__6D0D32F4");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Chat__sender_id__6C190EBB");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("class_code");

                entity.Property(e => e.ClassDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("class_description");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("class_name");

                entity.Property(e => e.ClassStatus).HasColumnName("class_status");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Class__teacher_i__3E52440B");
            });

            modelBuilder.Entity<ClassStudent>(entity =>
            {
                entity.ToTable("classStudent");

                entity.HasIndex(e => new { e.IdClass, e.IdStudent }, "unique_class_student")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdClass).HasColumnName("idClass");

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.ClassStudents)
                    .HasForeignKey(d => d.IdClass)
                    .HasConstraintName("FK__classStud__idCla__4222D4EF");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.ClassStudents)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("FK__classStud__idStu__4316F928");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.ClassUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClassResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__class_Res__resou__6477ECF3"),
                        r => r.HasOne<ClassStudent>().WithMany().HasForeignKey("ClassUserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__class_Res__class__6383C8BA"),
                        j =>
                        {
                            j.HasKey("ClassUserId", "ResourceId").HasName("PK__class_Re__17FF57E7B6B3A321");

                            j.ToTable("class_Resource");

                            j.IndexerProperty<int>("ClassUserId").HasColumnName("classUser_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("group_name");

                entity.Property(e => e.LeaderId).HasColumnName("leader_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Group__class_id__46E78A0C");

                entity.HasOne(d => d.Leader)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.LeaderId)
                    .HasConstraintName("FK__Group__leader_id__47DBAE45");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Groups)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Group_Res__resou__68487DD7"),
                        r => r.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Group_Res__group__6754599E"),
                        j =>
                        {
                            j.HasKey("GroupId", "ResourceId").HasName("PK__Group_Re__F1EFCA67344CEBA8");

                            j.ToTable("Group_Resource");

                            j.IndexerProperty<int>("GroupId").HasColumnName("group_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<GroupStudent>(entity =>
            {
                entity.ToTable("Group_Student");

                entity.HasIndex(e => new { e.GroupId, e.UserId }, "unique_group_student")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupStudents)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Group_Stu__group__4AB81AF0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupStudents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Group_Stu__user___4BAC3F29");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("file_url");

                entity.Property(e => e.ResourceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("resource_name");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.RoleName, "UQ__Role__783254B1F901706E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");

                entity.Property(e => e.ClassUserId).HasColumnName("classUser_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.SubmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("submission_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TeacherFeedback)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("teacher_feedback");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK__Submissio__assig__5629CD9C");

                entity.HasOne(d => d.ClassUser)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.ClassUserId)
                    .HasConstraintName("FK__Submissio__class__5441852A");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Submissio__stude__571DF1D5");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Submissions)
                    .UsingEntity<Dictionary<string, object>>(
                        "SubmissionResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Submissio__resou__60A75C0F"),
                        r => r.HasOne<Submission>().WithMany().HasForeignKey("SubmissionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Submissio__submi__5FB337D6"),
                        j =>
                        {
                            j.HasKey("SubmissionId", "ResourceId").HasName("PK__Submissi__BFCB0A52FCA06D91");

                            j.ToTable("Submission_Resource");

                            j.IndexerProperty<int>("SubmissionId").HasColumnName("submission_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UserName, "UQ__User__7C9273C4A0625DBB")
                    .IsUnique();

                entity.HasIndex(e => e.UserEmail, "UQ__User__B0FBA212F73693B9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("full_name");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK__User__user_role___3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

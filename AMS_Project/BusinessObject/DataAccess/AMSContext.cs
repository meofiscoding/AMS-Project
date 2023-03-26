using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.DataAccess
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
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupStudent> GroupStudents { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Submission> Submissions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = AMS; uid=sa;pwd=19112001");
            }
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
                    .HasConstraintName("FK__Assignmen__class__3D5E1FD2");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Assignmen__group__3E52440B");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Assignmen__teach__3C69FB99");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Assignments)
                    .UsingEntity<Dictionary<string, object>>(
                        "AssignmentResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Assignmen__resou__49C3F6B7"),
                        r => r.HasOne<Assignment>().WithMany().HasForeignKey("AssignmentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Assignmen__assig__48CFD27E"),
                        j =>
                        {
                            j.HasKey("AssignmentId", "ResourceId").HasName("PK__Assignme__FE1147D3C25F126D");

                            j.ToTable("Assignment_Resource");

                            j.IndexerProperty<int>("AssignmentId").HasColumnName("assignment_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

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
                    .HasConstraintName("FK__Chat__group_id__5812160E");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Chat__receiver_i__59FA5E80");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Chat__sender_id__59063A47");
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
                    .HasConstraintName("FK__Class__teacher_i__2B3F6F97");
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
                    .HasConstraintName("FK__classStud__idCla__2F10007B");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.ClassStudents)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("FK__classStud__idStu__300424B4");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.ClassUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClassResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__class_Res__resou__5165187F"),
                        r => r.HasOne<ClassStudent>().WithMany().HasForeignKey("ClassUserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__class_Res__class__5070F446"),
                        j =>
                        {
                            j.HasKey("ClassUserId", "ResourceId").HasName("PK__class_Re__17FF57E7C83E3F35");

                            j.ToTable("class_Resource");

                            j.IndexerProperty<int>("ClassUserId").HasColumnName("classUser_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.ResourceId).HasColumnName("resource_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("FK__Comment__parent___7A672E12");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__post_id__787EE5A0");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ResourceId)
                    .HasConstraintName("FK__Comment__resourc__7B5B524B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__user_id__797309D9");
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
                    .HasConstraintName("FK__Group__class_id__33D4B598");

                entity.HasOne(d => d.Leader)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.LeaderId)
                    .HasConstraintName("FK__Group__leader_id__34C8D9D1");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Groups)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Group_Res__resou__5535A963"),
                        r => r.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Group_Res__group__5441852A"),
                        j =>
                        {
                            j.HasKey("GroupId", "ResourceId").HasName("PK__Group_Re__F1EFCA672C6E18C5");

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
                    .HasConstraintName("FK__Group_Stu__group__37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupStudents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Group_Stu__user___38996AB5");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostContent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("post_content");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__class_id__6FE99F9F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__user_id__70DDC3D8");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Posts)
                    .UsingEntity<Dictionary<string, object>>(
                        "PostResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PostResou__resou__75A278F5"),
                        r => r.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__PostResou__post___74AE54BC"),
                        j =>
                        {
                            j.HasKey("PostId", "ResourceId").HasName("PK__PostReso__1A4FD8A181887A99");

                            j.ToTable("PostResource");

                            j.IndexerProperty<int>("PostId").HasColumnName("post_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
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

                entity.HasIndex(e => e.RoleName, "UQ__Role__783254B1AE1E5A17")
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
                    .HasConstraintName("FK__Submissio__assig__4316F928");

                entity.HasOne(d => d.ClassUser)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.ClassUserId)
                    .HasConstraintName("FK__Submissio__class__412EB0B6");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Submissio__stude__440B1D61");

                entity.HasMany(d => d.Resources)
                    .WithMany(p => p.Submissions)
                    .UsingEntity<Dictionary<string, object>>(
                        "SubmissionResource",
                        l => l.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Submissio__resou__4D94879B"),
                        r => r.HasOne<Submission>().WithMany().HasForeignKey("SubmissionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Submissio__submi__4CA06362"),
                        j =>
                        {
                            j.HasKey("SubmissionId", "ResourceId").HasName("PK__Submissi__BFCB0A52A0FC12C8");

                            j.ToTable("Submission_Resource");

                            j.IndexerProperty<int>("SubmissionId").HasColumnName("submission_id");

                            j.IndexerProperty<int>("ResourceId").HasColumnName("resource_id");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UserEmail, "UQ__User__B0FBA212E10FBF80")
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

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK__User__user_role___286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

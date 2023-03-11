﻿// <auto-generated />
using System;
using BussinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BussinessObject.Migrations
{
    [DbContext(typeof(AMSContext))]
    partial class AMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AssignmentResource", b =>
                {
                    b.Property<int>("AssignmentId")
                        .HasColumnType("int")
                        .HasColumnName("assignment_id");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int")
                        .HasColumnName("resource_id");

                    b.HasKey("AssignmentId", "ResourceId")
                        .HasName("PK__Assignme__FE1147D37C68B035");

                    b.HasIndex("ResourceId");

                    b.ToTable("Assignment_Resource", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AssignmentDeadline")
                        .HasColumnType("datetime")
                        .HasColumnName("assignment_deadline");

                    b.Property<string>("AssignmentDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("assignment_description");

                    b.Property<string>("AssignmentName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("assignment_name");

                    b.Property<int?>("ClassId")
                        .HasColumnType("int")
                        .HasColumnName("class_id");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Assignment", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_path");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("message");

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("receiver_id");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("sender_id");

                    b.Property<DateTime?>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("sent_date")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("ChatId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Chat", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClassCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("class_code");

                    b.Property<string>("ClassDescription")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("class_description");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("class_name");

                    b.Property<int>("ClassStatus")
                        .HasColumnType("int")
                        .HasColumnName("class_status");

                    b.Property<DateTime?>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("create_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.ClassStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("IdClass")
                        .HasColumnType("int")
                        .HasColumnName("idClass");

                    b.Property<int?>("IdStudent")
                        .HasColumnType("int")
                        .HasColumnName("idStudent");

                    b.HasKey("Id");

                    b.HasIndex("IdStudent");

                    b.HasIndex(new[] { "IdClass", "IdStudent" }, "unique_class_student")
                        .IsUnique()
                        .HasFilter("[idClass] IS NOT NULL AND [idStudent] IS NOT NULL");

                    b.ToTable("classStudent", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClassId")
                        .HasColumnType("int")
                        .HasColumnName("class_id");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("group_name");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int")
                        .HasColumnName("leader_id");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.GroupStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "GroupId", "UserId" }, "unique_group_student")
                        .IsUnique()
                        .HasFilter("[group_id] IS NOT NULL AND [user_id] IS NOT NULL");

                    b.ToTable("Group_Student", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_url");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("resource_name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("Resource", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleName" }, "UQ__Role__783254B1F901706E")
                        .IsUnique();

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int")
                        .HasColumnName("assignment_id");

                    b.Property<int?>("ClassUserId")
                        .HasColumnType("int")
                        .HasColumnName("classUser_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("content");

                    b.Property<double?>("Grade")
                        .HasColumnType("float")
                        .HasColumnName("grade");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("student_id");

                    b.Property<DateTime?>("SubmissionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("submission_date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("TeacherFeedback")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("teacher_feedback");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("ClassUserId");

                    b.HasIndex("StudentId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("full_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_email");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_name");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_password");

                    b.Property<int?>("UserRoleId")
                        .HasColumnType("int")
                        .HasColumnName("user_role_id");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.HasIndex(new[] { "UserName" }, "UQ__User__7C9273C4A0625DBB")
                        .IsUnique();

                    b.HasIndex(new[] { "UserEmail" }, "UQ__User__B0FBA212F73693B9")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ClassResource", b =>
                {
                    b.Property<int>("ClassUserId")
                        .HasColumnType("int")
                        .HasColumnName("classUser_id");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int")
                        .HasColumnName("resource_id");

                    b.HasKey("ClassUserId", "ResourceId")
                        .HasName("PK__class_Re__17FF57E7B6B3A321");

                    b.HasIndex("ResourceId");

                    b.ToTable("class_Resource", (string)null);
                });

            modelBuilder.Entity("GroupResource", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int")
                        .HasColumnName("resource_id");

                    b.HasKey("GroupId", "ResourceId")
                        .HasName("PK__Group_Re__F1EFCA67344CEBA8");

                    b.HasIndex("ResourceId");

                    b.ToTable("Group_Resource", (string)null);
                });

            modelBuilder.Entity("SubmissionResource", b =>
                {
                    b.Property<int>("SubmissionId")
                        .HasColumnType("int")
                        .HasColumnName("submission_id");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int")
                        .HasColumnName("resource_id");

                    b.HasKey("SubmissionId", "ResourceId")
                        .HasName("PK__Submissi__BFCB0A52FCA06D91");

                    b.HasIndex("ResourceId");

                    b.ToTable("Submission_Resource", (string)null);
                });

            modelBuilder.Entity("AssignmentResource", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Assignment", null)
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .IsRequired()
                        .HasConstraintName("FK__Assignmen__assig__5BE2A6F2");

                    b.HasOne("BussinessObject.DataAccess.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .IsRequired()
                        .HasConstraintName("FK__Assignmen__resou__5CD6CB2B");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Assignment", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Class", "Class")
                        .WithMany("Assignments")
                        .HasForeignKey("ClassId")
                        .HasConstraintName("FK__Assignmen__class__5070F446");

                    b.HasOne("BussinessObject.DataAccess.Group", "Group")
                        .WithMany("Assignments")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Assignmen__group__5165187F");

                    b.HasOne("BussinessObject.DataAccess.User", "Teacher")
                        .WithMany("Assignments")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK__Assignmen__teach__4F7CD00D");

                    b.Navigation("Class");

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Chat", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Group", "Group")
                        .WithMany("Chats")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Chat__group_id__6B24EA82");

                    b.HasOne("BussinessObject.DataAccess.User", "Receiver")
                        .WithMany("ChatReceivers")
                        .HasForeignKey("ReceiverId")
                        .HasConstraintName("FK__Chat__receiver_i__6D0D32F4");

                    b.HasOne("BussinessObject.DataAccess.User", "Sender")
                        .WithMany("ChatSenders")
                        .HasForeignKey("SenderId")
                        .HasConstraintName("FK__Chat__sender_id__6C190EBB");

                    b.Navigation("Group");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Class", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.User", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK__Class__teacher_i__3E52440B");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.ClassStudent", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Class", "IdClassNavigation")
                        .WithMany("ClassStudents")
                        .HasForeignKey("IdClass")
                        .HasConstraintName("FK__classStud__idCla__4222D4EF");

                    b.HasOne("BussinessObject.DataAccess.User", "IdStudentNavigation")
                        .WithMany("ClassStudents")
                        .HasForeignKey("IdStudent")
                        .HasConstraintName("FK__classStud__idStu__4316F928");

                    b.Navigation("IdClassNavigation");

                    b.Navigation("IdStudentNavigation");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Group", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Class", "Class")
                        .WithMany("Groups")
                        .HasForeignKey("ClassId")
                        .HasConstraintName("FK__Group__class_id__46E78A0C");

                    b.HasOne("BussinessObject.DataAccess.User", "Leader")
                        .WithMany("Groups")
                        .HasForeignKey("LeaderId")
                        .HasConstraintName("FK__Group__leader_id__47DBAE45");

                    b.Navigation("Class");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.GroupStudent", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Group", "Group")
                        .WithMany("GroupStudents")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Group_Stu__group__4AB81AF0");

                    b.HasOne("BussinessObject.DataAccess.User", "User")
                        .WithMany("GroupStudents")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Group_Stu__user___4BAC3F29");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Submission", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId")
                        .HasConstraintName("FK__Submissio__assig__5629CD9C");

                    b.HasOne("BussinessObject.DataAccess.ClassStudent", "ClassUser")
                        .WithMany("Submissions")
                        .HasForeignKey("ClassUserId")
                        .HasConstraintName("FK__Submissio__class__5441852A");

                    b.HasOne("BussinessObject.DataAccess.User", "Student")
                        .WithMany("Submissions")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__Submissio__stude__571DF1D5");

                    b.Navigation("Assignment");

                    b.Navigation("ClassUser");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.User", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Role", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .HasConstraintName("FK__User__user_role___3B75D760");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("ClassResource", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.ClassStudent", null)
                        .WithMany()
                        .HasForeignKey("ClassUserId")
                        .IsRequired()
                        .HasConstraintName("FK__class_Res__class__6383C8BA");

                    b.HasOne("BussinessObject.DataAccess.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .IsRequired()
                        .HasConstraintName("FK__class_Res__resou__6477ECF3");
                });

            modelBuilder.Entity("GroupResource", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK__Group_Res__group__6754599E");

                    b.HasOne("BussinessObject.DataAccess.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .IsRequired()
                        .HasConstraintName("FK__Group_Res__resou__68487DD7");
                });

            modelBuilder.Entity("SubmissionResource", b =>
                {
                    b.HasOne("BussinessObject.DataAccess.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .IsRequired()
                        .HasConstraintName("FK__Submissio__resou__60A75C0F");

                    b.HasOne("BussinessObject.DataAccess.Submission", null)
                        .WithMany()
                        .HasForeignKey("SubmissionId")
                        .IsRequired()
                        .HasConstraintName("FK__Submissio__submi__5FB337D6");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Class", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ClassStudents");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.ClassStudent", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Group", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Chats");

                    b.Navigation("GroupStudents");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BussinessObject.DataAccess.User", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ChatReceivers");

                    b.Navigation("ChatSenders");

                    b.Navigation("ClassStudents");

                    b.Navigation("Classes");

                    b.Navigation("GroupStudents");

                    b.Navigation("Groups");

                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}

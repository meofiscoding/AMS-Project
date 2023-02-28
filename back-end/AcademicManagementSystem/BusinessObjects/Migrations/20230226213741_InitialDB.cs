using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attendance_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "class_days",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_days", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "class_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course_family",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    published_year = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_family", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grade_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "province",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_province", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "semester",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semester", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "session_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teacher_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "working_time",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_working_time", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    course_family_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    semester_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.code);
                    table.ForeignKey(
                        name: "FK_course_course_family_course_family_code",
                        column: x => x.course_family_code,
                        principalTable: "course_family",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prefix = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_province_province_id",
                        column: x => x.province_id,
                        principalTable: "province",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    answer_no = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_answer_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FormQuestion",
                columns: table => new
                {
                    FormsId = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormQuestion", x => new { x.FormsId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_FormQuestion_form_FormsId",
                        column: x => x.FormsId,
                        principalTable: "form",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FormQuestion_question_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "question",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ward",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prefix = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ward", x => x.id);
                    table.ForeignKey(
                        name: "FK_ward_district_district_id",
                        column: x => x.district_id,
                        principalTable: "district",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ward_province_province_id",
                        column: x => x.province_id,
                        principalTable: "province",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "center",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    ward_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_center", x => x.id);
                    table.ForeignKey(
                        name: "FK_center_district_district_id",
                        column: x => x.district_id,
                        principalTable: "district",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_center_province_province_id",
                        column: x => x.province_id,
                        principalTable: "province",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_center_ward_ward_id",
                        column: x => x.ward_id,
                        principalTable: "ward",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    center_id = table.Column<int>(type: "int", nullable: false),
                    semester_name_portal = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    module_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    module_exam_name_portal = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    module_type = table.Column<int>(type: "int", nullable: false),
                    max_theory_grade = table.Column<int>(type: "int", nullable: true),
                    max_practical_grade = table.Column<int>(type: "int", nullable: true),
                    hours = table.Column<int>(type: "int", nullable: false),
                    days = table.Column<int>(type: "int", nullable: false),
                    exam_type = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                    table.ForeignKey(
                        name: "FK_module_center_center_id",
                        column: x => x.center_id,
                        principalTable: "center",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    center_id = table.Column<int>(type: "int", nullable: false),
                    room_type_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.id);
                    table.ForeignKey(
                        name: "FK_room_center_center_id",
                        column: x => x.center_id,
                        principalTable: "center",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_room_room_type_room_type_id",
                        column: x => x.room_type_id,
                        principalTable: "room_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    ward_id = table.Column<int>(type: "int", nullable: false),
                    center_id = table.Column<int>(type: "int", nullable: false),
                    gender_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    mobile_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email_organization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    birthday = table.Column<DateTime>(type: "date", nullable: false),
                    citizen_identity_card_no = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    citizen_identity_card_published_date = table.Column<DateTime>(type: "date", nullable: false),
                    citizen_identity_card_published_place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_center_center_id",
                        column: x => x.center_id,
                        principalTable: "center",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_district_district_id",
                        column: x => x.district_id,
                        principalTable: "district",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_gender_gender_id",
                        column: x => x.gender_id,
                        principalTable: "gender",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_province_province_id",
                        column: x => x.province_id,
                        principalTable: "province",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_ward_ward_id",
                        column: x => x.ward_id,
                        principalTable: "ward",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "course_module_semester",
                columns: table => new
                {
                    course_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    semester_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_module_semester", x => new { x.course_code, x.module_id, x.semester_id });
                    table.ForeignKey(
                        name: "FK_course_module_semester_course_course_code",
                        column: x => x.course_code,
                        principalTable: "course",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_course_module_semester_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_course_module_semester_semester_semester_id",
                        column: x => x.semester_id,
                        principalTable: "semester",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "grade_category_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    grade_category_id = table.Column<int>(type: "int", nullable: false),
                    total_weight = table.Column<int>(type: "int", nullable: false),
                    quantity_grade_item = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade_category_module", x => x.id);
                    table.ForeignKey(
                        name: "FK_grade_category_module_grade_category_grade_category_id",
                        column: x => x.grade_category_id,
                        principalTable: "grade_category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_grade_category_module_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "active_refresh_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exp_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_active_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_admin_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sro",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sro", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_sro_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    enroll_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    course_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    status_date = table.Column<DateTime>(type: "date", nullable: false),
                    home_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    parental_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    parental_relationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contact_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    parental_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    application_date = table.Column<DateTime>(type: "date", nullable: false),
                    application_document = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    high_school = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    university = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    facebook_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    portfolio_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    working_company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_salary = table.Column<int>(type: "int", nullable: true),
                    company_position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fee_plan = table.Column<int>(type: "int", nullable: false),
                    promotion = table.Column<int>(type: "int", nullable: false),
                    is_draft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_student_course_course_code",
                        column: x => x.course_code,
                        principalTable: "course",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_student_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "teacher",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    teacher_type_id = table.Column<int>(type: "int", nullable: false),
                    working_time_id = table.Column<int>(type: "int", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    start_working_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tax_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_teacher_teacher_type_teacher_type_id",
                        column: x => x.teacher_type_id,
                        principalTable: "teacher_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_teacher_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_teacher_working_time_working_time_id",
                        column: x => x.working_time_id,
                        principalTable: "working_time",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "grade_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade_category_module_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_grade_item_grade_category_module_grade_category_module_id",
                        column: x => x.grade_category_module_id,
                        principalTable: "grade_category_module",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    center_id = table.Column<int>(type: "int", nullable: false),
                    course_family_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    class_days_id = table.Column<int>(type: "int", nullable: false),
                    class_status_id = table.Column<int>(type: "int", nullable: false),
                    sro_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    completion_date = table.Column<DateTime>(type: "date", nullable: false),
                    graduation_date = table.Column<DateTime>(type: "date", nullable: false),
                    class_hour_start = table.Column<TimeSpan>(type: "time", nullable: false),
                    class_hour_end = table.Column<TimeSpan>(type: "time", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class", x => x.id);
                    table.ForeignKey(
                        name: "FK_class_center_center_id",
                        column: x => x.center_id,
                        principalTable: "center",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_class_days_class_days_id",
                        column: x => x.class_days_id,
                        principalTable: "class_days",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_class_status_class_status_id",
                        column: x => x.class_status_id,
                        principalTable: "class_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_course_family_course_family_code",
                        column: x => x.course_family_code,
                        principalTable: "course_family",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_class_sro_sro_id",
                        column: x => x.sro_id,
                        principalTable: "sro",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "day_off",
                columns: table => new
                {
                    working_time_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teacher_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    center_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_off", x => x.id);
                    table.ForeignKey(
                        name: "FK_day_off_center_center_id",
                        column: x => x.center_id,
                        principalTable: "center",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_day_off_teacher_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teacher",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "SkillTeacher",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false),
                    TeachersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillTeacher", x => new { x.SkillsId, x.TeachersUserId });
                    table.ForeignKey(
                        name: "FK_SkillTeacher_skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "skill",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SkillTeacher_teacher_TeachersUserId",
                        column: x => x.TeachersUserId,
                        principalTable: "teacher",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "class_schedule",
                columns: table => new
                {
                    working_time_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    teacher_id = table.Column<int>(type: "int", nullable: false),
                    class_days_id = table.Column<int>(type: "int", nullable: false),
                    class_status_id = table.Column<int>(type: "int", nullable: false),
                    theory_room_id = table.Column<int>(type: "int", nullable: true),
                    lab_room_id = table.Column<int>(type: "int", nullable: true),
                    exam_room_id = table.Column<int>(type: "int", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    theory_exam_date = table.Column<DateTime>(type: "date", nullable: true),
                    practical_exam_date = table.Column<DateTime>(type: "date", nullable: true),
                    class_hour_start = table.Column<TimeSpan>(type: "time", nullable: false),
                    class_hour_end = table.Column<TimeSpan>(type: "time", nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_class_schedule_class_class_id",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_class_days_class_days_id",
                        column: x => x.class_days_id,
                        principalTable: "class_days",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_class_status_class_status_id",
                        column: x => x.class_status_id,
                        principalTable: "class_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_room_exam_room_id",
                        column: x => x.exam_room_id,
                        principalTable: "room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_room_lab_room_id",
                        column: x => x.lab_room_id,
                        principalTable: "room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_room_theory_room_id",
                        column: x => x.theory_room_id,
                        principalTable: "room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_class_schedule_teacher_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teacher",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "student_class",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false),
                    class_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_class", x => new { x.student_id, x.class_id });
                    table.ForeignKey(
                        name: "FK_student_class_class_class_id",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_student_class_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "student_grade",
                columns: table => new
                {
                    class_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    grade_item_id = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<double>(type: "float", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_grade", x => new { x.class_id, x.student_id, x.grade_item_id });
                    table.ForeignKey(
                        name: "FK_student_grade_class_class_id",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_student_grade_grade_item_grade_item_id",
                        column: x => x.grade_item_id,
                        principalTable: "grade_item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_student_grade_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    room_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_schedule_id = table.Column<int>(type: "int", nullable: false),
                    session_type_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    learning_date = table.Column<DateTime>(type: "date", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session", x => x.id);
                    table.ForeignKey(
                        name: "FK_session_class_schedule_class_schedule_id",
                        column: x => x.class_schedule_id,
                        principalTable: "class_schedule",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_session_room_room_id",
                        column: x => x.room_id,
                        principalTable: "room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_session_session_type_session_type_id",
                        column: x => x.session_type_id,
                        principalTable: "session_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    attendance_status_id = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => new { x.session_id, x.student_id });
                    table.ForeignKey(
                        name: "FK_attendance_attendance_status_attendance_status_id",
                        column: x => x.attendance_status_id,
                        principalTable: "attendance_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_attendance_session_session_id",
                        column: x => x.session_id,
                        principalTable: "session",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_attendance_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "gpa_record",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_id = table.Column<int>(type: "int", nullable: false),
                    teacher_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    session_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    form_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpa_record", x => x.id);
                    table.ForeignKey(
                        name: "FK_gpa_record_class_class_id",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_gpa_record_form_form_id",
                        column: x => x.form_id,
                        principalTable: "form",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_gpa_record_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_gpa_record_session_session_id",
                        column: x => x.session_id,
                        principalTable: "session",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_gpa_record_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_gpa_record_teacher_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teacher",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "gpa_record_answer",
                columns: table => new
                {
                    gpa_record_id = table.Column<int>(type: "int", nullable: false),
                    answer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpa_record_answer", x => new { x.answer_id, x.gpa_record_id });
                    table.ForeignKey(
                        name: "FK_gpa_record_answer_answer_answer_id",
                        column: x => x.answer_id,
                        principalTable: "answer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_gpa_record_answer_gpa_record_gpa_record_id",
                        column: x => x.gpa_record_id,
                        principalTable: "gpa_record",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_active_refresh_token_user_id",
                table: "active_refresh_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_answer_question_id",
                table: "answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_attendance_status_id",
                table: "attendance",
                column: "attendance_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_student_id",
                table: "attendance",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_center_district_id",
                table: "center",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_center_province_id",
                table: "center",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_center_ward_id",
                table: "center",
                column: "ward_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_center_id",
                table: "class",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_class_days_id",
                table: "class",
                column: "class_days_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_class_status_id",
                table: "class",
                column: "class_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_course_family_code",
                table: "class",
                column: "course_family_code");

            migrationBuilder.CreateIndex(
                name: "IX_class_sro_id",
                table: "class",
                column: "sro_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_class_days_id",
                table: "class_schedule",
                column: "class_days_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_class_id",
                table: "class_schedule",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_class_status_id",
                table: "class_schedule",
                column: "class_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_exam_room_id",
                table: "class_schedule",
                column: "exam_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_lab_room_id",
                table: "class_schedule",
                column: "lab_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_module_id",
                table: "class_schedule",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_teacher_id",
                table: "class_schedule",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_schedule_theory_room_id",
                table: "class_schedule",
                column: "theory_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_course_family_code",
                table: "course",
                column: "course_family_code");

            migrationBuilder.CreateIndex(
                name: "IX_course_module_semester_module_id",
                table: "course_module_semester",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_module_semester_semester_id",
                table: "course_module_semester",
                column: "semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_day_off_center_id",
                table: "day_off",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_day_off_teacher_id",
                table: "day_off",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_district_province_id",
                table: "district",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_FormQuestion_QuestionsId",
                table: "FormQuestion",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_class_id",
                table: "gpa_record",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_form_id",
                table: "gpa_record",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_module_id",
                table: "gpa_record",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_session_id",
                table: "gpa_record",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_student_id",
                table: "gpa_record",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_teacher_id",
                table: "gpa_record",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_gpa_record_answer_gpa_record_id",
                table: "gpa_record_answer",
                column: "gpa_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_grade_category_module_grade_category_id",
                table: "grade_category_module",
                column: "grade_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_grade_category_module_module_id",
                table: "grade_category_module",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_grade_item_grade_category_module_id",
                table: "grade_item",
                column: "grade_category_module_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_center_id",
                table: "module",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_center_id",
                table: "room",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_room_type_id",
                table: "room",
                column: "room_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_class_schedule_id",
                table: "session",
                column: "class_schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_room_id",
                table: "session",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_session_type_id",
                table: "session",
                column: "session_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTeacher_TeachersUserId",
                table: "SkillTeacher",
                column: "TeachersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_student_course_code",
                table: "student",
                column: "course_code");

            migrationBuilder.CreateIndex(
                name: "IX_student_enroll_number",
                table: "student",
                column: "enroll_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_class_class_id",
                table: "student_class",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_grade_grade_item_id",
                table: "student_grade",
                column: "grade_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_grade_student_id",
                table: "student_grade",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_tax_code",
                table: "teacher",
                column: "tax_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teacher_teacher_type_id",
                table: "teacher",
                column: "teacher_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_working_time_id",
                table: "teacher",
                column: "working_time_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_center_id",
                table: "user",
                column: "center_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_citizen_identity_card_no",
                table: "user",
                column: "citizen_identity_card_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_district_id",
                table: "user",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_email_organization",
                table: "user",
                column: "email_organization",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_gender_id",
                table: "user",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_mobile_phone",
                table: "user",
                column: "mobile_phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_province_id",
                table: "user",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_ward_id",
                table: "user",
                column: "ward_id");

            migrationBuilder.CreateIndex(
                name: "IX_ward_district_id",
                table: "ward",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_ward_province_id",
                table: "ward",
                column: "province_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "active_refresh_token");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "course_module_semester");

            migrationBuilder.DropTable(
                name: "day_off");

            migrationBuilder.DropTable(
                name: "FormQuestion");

            migrationBuilder.DropTable(
                name: "gpa_record_answer");

            migrationBuilder.DropTable(
                name: "SkillTeacher");

            migrationBuilder.DropTable(
                name: "student_class");

            migrationBuilder.DropTable(
                name: "student_grade");

            migrationBuilder.DropTable(
                name: "attendance_status");

            migrationBuilder.DropTable(
                name: "semester");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "gpa_record");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "grade_item");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "form");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "grade_category_module");

            migrationBuilder.DropTable(
                name: "class_schedule");

            migrationBuilder.DropTable(
                name: "session_type");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "grade_category");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "teacher");

            migrationBuilder.DropTable(
                name: "class_days");

            migrationBuilder.DropTable(
                name: "class_status");

            migrationBuilder.DropTable(
                name: "course_family");

            migrationBuilder.DropTable(
                name: "sro");

            migrationBuilder.DropTable(
                name: "room_type");

            migrationBuilder.DropTable(
                name: "teacher_type");

            migrationBuilder.DropTable(
                name: "working_time");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "center");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "ward");

            migrationBuilder.DropTable(
                name: "district");

            migrationBuilder.DropTable(
                name: "province");
        }
    }
}

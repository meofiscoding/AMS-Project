use LMS

--quan ly quyen truy cap cua nguoi dung theo tung Role: thuan tien cho viec mo rong Role sau nay
create table Role
(
    ID int identity(1,1) primary key,
    -- role_name
    role_name varchar(255) not null,
)

-- create table user: luu thong tin chung cua User
create table [User]
(
    ID int identity(1,1) primary key,
    -- user_name
    user_name varchar(255) not null,
    -- user_password
    user_password varchar(255) not null,
    -- user_email
    user_email varchar(255) not null,
    -- xac dinh vai tro cua user
    user_role_id int foreign key references Role(ID),
    -- create_at datetime auto insert
    create_at datetime default getdate()
)

-- create table Class to store all information about class and 1 teacher who teach that class
-- Bang class nay chi luu thong tin chung cua class thoi, o day co them create_by lien ket voi bang User de luu thong tin nguoi tao lop -> dam bao 1 Lop co 1giao vien by default
-- co the khong can khoa ngoai tro den bang Role vi  
CREATE table Class(
    -- id
    ID int identity(1,1) primary key,
    -- class_name
    class_name varchar(255) not null,
    -- class_description
    class_description varchar(255) null,
    -- class_code
    class_code varchar(255) not null,
    -- class_status show active or not
    class_status int not null,
    -- create_at datetime auto insert
    create_at datetime default getdate(),
    -- create_by foreign key with user table
    create_by int foreign key references [User](ID),
)

-- luu thong tin ve User va vai tro cua User trong Class => lien ket giua lop hoc va nguoi dung (ca giao vien va hoc sinh)
-- moi ban ghi trong bang nay dai dien cho 1 user da tham gia vao 1 lop hoc
create table classUser
(
    ID int identity(1,1) primary key,
    -- class_id foreign key with class table
    class_id int foreign key references Class(ID),
    -- user_id foreign key with user table
    user_id int foreign key references [User](ID),
)

-- luu thong tin chung cua Group : ten nhom, lop hoc ma nhom do thuoc ve
create table [Group]
(
    ID int identity(1,1) primary key,
    -- group_name
    group_name varchar(255) not null,
    -- class_id foreign key with class table
    class_id int foreign key references Class(ID),
)

-- the hien quan he giua group va user
-- moi ban ghi trong bang nay dai dien cho 1 user da tham gia vao 1 nhom
create table Group_Student
(
    ID int identity(1,1) primary key,
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- student_id foreign key with student table
    student_id int foreign key references [User](ID),
    -- role_id foreign key with role table
    role_id int foreign key references Role(ID),
)


-- Để giải quyết việc người dùng chỉ có thể truy cập tài nguyên của lớp hoặc nhóm mình tham gia, 
-- cần cập nhật câu truy vấn SELECT của mình để kiểm tra vai trò của người dùng và các quan hệ giữa các bảng. 
-- Ví dụ, câu truy vấn SELECT để lấy danh sách tài nguyên của một lớp học có thể có dạng như sau:
/*
SELECT r.*
FROM Resource r
INNER JOIN Assignment a ON r.AssignmentId = a.AssignmentId
INNER JOIN Class c ON a.ClassId = c.ClassId
INNER JOIN ClassUser cu ON c.ClassId = cu.ClassId
INNER JOIN User u ON cu.UserId = u.UserId
INNER JOIN Role r ON cu.RoleId = r.RoleId
WHERE c.ClassId = <class_id>
AND u.UserId = <user_id>
AND r.RoleName = 'student';
*/

-- ensure that each student can only be assigned to one group in a classUser
ALTER TABLE Group_Student
ADD CONSTRAINT UQ_Group_Student_Student_Group
UNIQUE (student_id, group_id, classUser_id);

-- create table Assignment
create table Assignment
(
    ID int identity(1,1) primary key,
    -- assignment_name
    assignment_name varchar(255) not null,
    -- assignment_description
    assignment_description varchar(255) not null,
    -- assignment_deadline
    assignment_deadline datetime not null,
    -- teacher_id foreign key with user table
    teacher_id int foreign key references [User](ID),
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
)

-- create table Submissions
create table Submissions
(
    ID int identity(1,1) primary key,
    -- file_url
    file_url varchar(255) not null,
    -- content
    content varchar(255) not null,
    -- grade
    grade int,
    -- teacher feedback
    teacher_feedback varchar(255),
    -- foreign key using classUser_id and student_id reference to Classuser(class_id, user_id)
    classUser_id int foreign key references classUser(ID),
    -- submission_date
    submission_date datetime default getdate(),
    -- assignment_id foreign key with assignment table
    assignment_id int foreign key references Assignment(ID),
    -- student_id foreign key with student table
    student_id int foreign key references [User](ID),
)

-- Nếu muốn quản lý tài nguyên cho từng lớp học, thì gắn tài nguyên với từng class là cách tốt nhất. Tuy nhiên, nếu muốn sử dụng tài nguyên chung cho nhiều lớp học hoặc nhóm khác nhau, thì gắn tài nguyên với từng group là cách tốt nhất.
-- create table Resource
create table Resource
(
    ID int identity(1,1) primary key,
    -- resource_name
    resource_name varchar(255) not null,
    -- type
    type varchar(255) not null,
    -- file url
    file_url varchar(255) not null,
    -- assignment_id foreign key with assignment table
    assignment_id int foreign key references Assignment(ID) on delete cascade,
    -- submission_id foreign key with submission table
    submission_id int foreign key references Submissions(ID) on delete cascade,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID) on delete cascade,
    -- upload_by foreign key with user table
    creator_id int foreign key references [User](ID),
)

-- CREATE table assignment_resource
create table Assignment_Resource
(
    ID int identity(1,1) primary key,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- assignment_id foreign key with assignment table
    assignment_id int foreign key references Assignment(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references Resource(ID),
)

-- create table submission_resource
create table Submission_Resource
(
    ID int identity(1,1) primary key,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- submission_id foreign key with submission table
    submission_id int foreign key references Submissions(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references Resource(ID),
)

-- restrict accesibility of resource on each class
create table classUser_Resource
(
    ID int identity(1,1) primary key,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references Resource(ID),
)

-- restrict accesibility of resource on each group
create table Group_Resource
(
    ID int identity(1,1) primary key,
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references Resource(ID),
)
-- Sau khi cập nhật các bảng, chúng ta sẽ có thể tạo các quyền truy cập tài nguyên cho từng lớp và nhóm bằng cách thêm dữ liệu vào các bảng ClassResource và GroupResource.

-- Ví dụ, để cho phép lớp 1 truy cập vào một tài nguyên có resource_id là 1, chúng ta sẽ thêm một bản ghi mới vào bảng ClassResource:
-- insert into classUser_Resource (classUser_id, resource_id) values (1, 1)
-- insert into Group_Resource (group_id, resource_id) values (2, 2) to allow group 2 access to resource 2


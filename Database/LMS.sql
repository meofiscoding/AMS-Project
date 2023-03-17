create database AMS
go
use AMS

--quan ly quyen truy cap cua nguoi dung theo tung Role: thuan tien cho viec mo rong Role sau nay
create table Role
(
    ID int identity(1,1) primary key,
    -- role_name
    role_name varchar(255) not null UNIQUE,
)

-- create table user: luu thong tin chung cua User
create table [User]
(
    ID int identity(1,1) primary key,
    -- user_password
    user_password varchar(255) not null,
    -- user_email
    user_email varchar(255) not null UNIQUE,
    -- full_name
    full_name varchar(255) null,
    -- xac dinh vai tro cua user
    user_role_id int foreign key references Role(ID),
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
    -- teacher_id foreign key with user table
    teacher_id int foreign key references [User](ID),
    -- create_at datetime auto insert
    create_at datetime default getdate(),
)

create table classStudent
(
    -- primary key
    ID int identity(1,1) primary key,
    -- class_id foreign key with class table
    idClass int foreign key references Class(ID),
    -- user_id foreign key with user table
    idStudent int foreign key references [User](ID),
)

-- ensure that we dont add same student in 1 class
alter table classStudent add constraint unique_class_student unique(idClass, idStudent)

-- luu thong tin chung cua Group : ten nhom, lop hoc ma nhom do thuoc ve
create table [Group]
(
    ID int identity(1,1) primary key,
    -- group_name
    group_name varchar(255) not null,
    -- class_id foreign key with class table
    class_id int foreign key references Class(ID),
    -- leader_id foreign key with user table
    leader_id int foreign key references [User](ID),
)

-- the hien quan he giua group va user
-- moi ban ghi trong bang nay dai dien cho 1 user da tham gia vao 1 nhom
create table Group_Student
(
    -- primary key
    ID int identity(1,1) primary key,
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- user_id foreign key with user table
    user_id int foreign key references [User](ID),
)

-- ensure that we dont add same student in 1 group
alter table Group_Student add constraint unique_group_student unique(group_id, user_id)

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
    -- class_id foreign key with class table
    class_id int foreign key references Class(ID),
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
)

-- create table Submissions
create table Submissions
(
    ID int identity(1,1) primary key,
    -- content
    content varchar(255) not null,
    -- score with datatype float
    grade float,
    -- teacher feedback
    teacher_feedback varchar(255),
    -- foreign key using classUser_id and student_id reference to Classuser(class_id, user_id)
    classUser_id int foreign key references classStudent(ID),
    -- submission_date
    submission_date datetime default getdate(),
    -- assignment_id foreign key with assignment table
    assignment_id int foreign key references Assignment(ID),
    -- student_id foreign key with student table
    student_id int foreign key references [User](ID),
)

-- Nếu muốn quản lý tài nguyên cho từng lớp học, thì gắn tài nguyên với từng class là cách tốt nhất. Tuy nhiên, nếu muốn sử dụng tài nguyên chung cho nhiều lớp học hoặc nhóm khác nhau, thì gắn tài nguyên với từng group là cách tốt nhất.
-- create table Resource
create table [Resource]
(
    ID int identity(1,1) primary key,
    -- resource_name
    resource_name varchar(255) not null,
    -- type
    type varchar(255) not null,
    -- file url
    file_url varchar(255) not null,
)

-- CREATE table assignment_resource
create table Assignment_Resource
(
    -- assignment_id foreign key with assignment table
    assignment_id int foreign key references Assignment(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references [Resource](ID),
    PRIMARY KEY (assignment_id, resource_id),
)

-- create table submission_resource
create table Submission_Resource
(
    -- submission_id foreign key with submission table
    submission_id int foreign key references Submissions(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references [Resource](ID),
    PRIMARY KEY (submission_id, resource_id),
)

-- restrict accesibility of resource on each class
create table class_Resource
(
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classStudent(ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references [Resource](ID),
    PRIMARY KEY (classUser_id, resource_id),
)

-- restrict accesibility of resource on each group
create table Group_Resource
(
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- resource_id foreign key with resource table
    resource_id int foreign key references [Resource](ID),
    PRIMARY KEY (group_id, resource_id),
)

CREATE TABLE Chat (
    ChatId INT PRIMARY KEY,
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- sender_id foreign key with user table
    sender_id int foreign key references [User](ID),
    -- receiver_id foreign key with user table
    receiver_id int foreign key references [User](ID),
    -- message
    message varchar(255) not null,
    -- file path
    file_path varchar(255),
    -- get date time sent
    sent_date datetime default getdate(),
);

CREATE TABLE Post (
    ID int identity(1,1) primary key,
    post_title varchar(255) not null,
    post_content varchar(255) not null,
    class_id int not null foreign key references Class(ID),
    user_id int not null foreign key references [User](ID),
    created_at datetime default getdate(),
);

CREATE TABLE PostResource (
    post_id int not null foreign key references Post(ID),
    resource_id int not null foreign key references Resource(ID),
    primary key (post_id, resource_id)
);
CREATE TABLE [Comment]
(
    ID int IDENTITY(1,1) PRIMARY KEY,
    -- content of the comment
    content nvarchar(max) NOT NULL,
    -- the ID of the post that the comment belongs to
    post_id int NOT NULL FOREIGN KEY REFERENCES [Post](ID),
    -- the ID of the user who created the comment
    user_id int NOT NULL FOREIGN KEY REFERENCES [User](ID),
    -- the ID of the comment that this comment is replying to (nullable)
    parent_comment_id int NULL FOREIGN KEY REFERENCES [Comment](ID),
    -- the ID of the resource attached to the comment (nullable)
    resource_id int NULL FOREIGN KEY REFERENCES [Resource](ID),
    -- the time that the comment was created
    created_at datetime NOT NULL DEFAULT GETDATE(),
);

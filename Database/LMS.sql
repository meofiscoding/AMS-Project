use LMS

-- create table Role
create table Role
(
    ID int identity(1,1) primary key,
    -- role_name
    role_name varchar(255) not null,
)

-- create table user
create table [User]
(
    ID int identity(1,1) primary key,
    -- user_name
    user_name varchar(255) not null,
    -- user_password
    user_password varchar(255) not null,
    -- user_email
    user_email varchar(255) not null,
    -- user_role_id foreign key with role table
    user_role_id int foreign key references Role(ID),
    -- create_at datetime auto insert
    create_at datetime default getdate()
)

-- create table Class to store all information about class and 1 teacher who teach that class
CREATE table Class(
    -- id
    ID int identity(1,1) primary key,
    -- class_name
    class_name varchar(255) not null,
    -- class_description
    class_description varchar(255) null,
    -- class_code
    class_code varchar(255) not null,
     -- classUser_status
    classUser_status varchar(255) not null,
    -- create_at datetime auto insert
    create_at datetime default getdate(),
    -- create_by foreign key with user table
    create_by int foreign key references [User](ID) 
)

-- create table classUser to store information of relationship between class and user
create table classUser
(
    ID int identity(1,1) primary key,
    -- class_id foreign key with class table
    class_id int foreign key references Class(ID),
    -- user_id foreign key with user table
    user_id int foreign key references [User](ID),
)

-- create table classUser_Student
create table classUser_Student
(
    ID int identity(1,1) primary key,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- user id with role student
    student_id int foreign key references [User](ID),
)

-- create table Group
create table [Group]
(
    ID int identity(1,1) primary key,
    -- group_name
    group_name varchar(255) not null,
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
)

-- group_student to assign student to group
create table Group_Student
(
    ID int identity(1,1) primary key,
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- student_id foreign key with student table
    student_id int foreign key references [User](ID),
)

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
    -- classUser_id foreign key with classUser table
    classUser_id int foreign key references classUser(ID),
    -- group_id foreign key with group table
    group_id int foreign key references [Group](ID),
    -- upload_by foreign key with user table
    upload_by int foreign key references [User](ID),
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
    -- classUser_id foreign key with classUser table
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


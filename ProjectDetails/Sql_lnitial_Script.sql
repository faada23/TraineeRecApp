CREATE TABLE SupervisorsType (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(25) NOT NULL
);

CREATE TABLE TraineeshipsStatus (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(25) NOT NULL
);

CREATE TABLE Specialities (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE GroupsName (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(60) NOT NULL
);

CREATE TABLE Organizations (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(70) NOT NULL 
);

CREATE TABLE Students (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    Birthday DATE NOT NULL,
    PhoneNumber VARCHAR(20),
    Course CHAR(1) NOT NULL,
    Speciality_Id INTEGER NOT NULL,
    GroupName_Id INTEGER NOT NULL,
    FOREIGN KEY (Speciality_Id) REFERENCES Specialities(Id),
    FOREIGN KEY (GroupName_Id) REFERENCES GroupsName(Id)
); 

CREATE TABLE Traineeships (
    Id SERIAL PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Grade SMALLINT,
    Status_Id INTEGER NOT NULL,
    Student_Id INTEGER NOT NULL,
    FOREIGN KEY (Status_Id) REFERENCES TraineeshipsStatus(Id),
    FOREIGN KEY (Student_Id) REFERENCES Students(Id),
    CONSTRAINT chk_dates CHECK (StartDate <= EndDate)
);

CREATE TABLE TraineeshipSupervisors (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    PhoneNumber VARCHAR(20),
    SupervisorType_Id INTEGER NOT NULL,
    Organization_Id INTEGER,
    FOREIGN KEY (SupervisorType_Id) REFERENCES SupervisorsType(Id),
    FOREIGN KEY (Organization_Id) REFERENCES Organizations(Id)
);

CREATE TABLE Traineeships_TraineeshipSupervisors (
    Traineeship_Id INTEGER NOT NULL,
    TraineeshipSupervisor_Id INTEGER NOT NULL,
    PRIMARY KEY (Traineeship_Id, TraineeshipSupervisor_Id),
    FOREIGN KEY (Traineeship_Id) REFERENCES Traineeships(Id),
    FOREIGN KEY (TraineeshipSupervisor_Id) REFERENCES TraineeshipSupervisors(Id)
);

CREATE TABLE Roles (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL 
);

CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    PasswordHash VARCHAR(80) NOT NULL,
    Email VARCHAR(50) UNIQUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Student_Id INTEGER,
    TraineeshipSupervisor_Id INTEGER,
    Role_Id INTEGER NOT NULL,
    FOREIGN KEY (Student_Id) REFERENCES Students(Id),
    FOREIGN KEY (Role_Id) REFERENCES Roles(Id),
    FOREIGN KEY (TraineeshipSupervisor_Id) REFERENCES TraineeshipSupervisors(Id)
);

INSERT INTO Roles (Name)
VALUES ('Manager');

INSERT INTO Users (FullName, PasswordHash, Email, Student_Id, TraineeshipSupervisor_Id, Role_Id)
VALUES ('Иван Иванов', 'hashed_password', 'ivan.ivanov@example.com', NULL, NULL, (SELECT Id FROM Roles WHERE Name = 'Manager'));
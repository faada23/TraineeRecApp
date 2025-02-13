CREATE TABLE SupervisorType (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(25) NOT NULL UNIQUE
);

CREATE TABLE TraineeshipStatus (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(25) NOT NULL UNIQUE
);

CREATE TABLE Speciality (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE GroupName (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(60) NOT NULL
);

CREATE TABLE Organization (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(70) NOT NULL
);

CREATE TABLE Student (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    Birthday DATE NOT NULL,
    PhoneNumber VARCHAR(20),
    Course CHAR(1) NOT NULL,
    Speciality_Id INTEGER NOT NULL,
    GroupName_Id INTEGER NOT NULL,
    FOREIGN KEY (Speciality_Id) REFERENCES Speciality(Id),
    FOREIGN KEY (GroupName_Id) REFERENCES GroupName(Id)
); 

CREATE TABLE Traineeship (
    Id SERIAL PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status_Id INTEGER NOT NULL,
    Student_Id INTEGER NOT NULL,
    FOREIGN KEY (Status_Id) REFERENCES TraineeshipStatus(Id),
    FOREIGN KEY (Student_Id) REFERENCES Student(Id),
    CONSTRAINT chk_dates CHECK (StartDate <= EndDate)
);

CREATE TABLE TraineeshipSupervisor (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    PhoneNumber VARCHAR(20),
    SupervisorType_Id INTEGER NOT NULL,
    Organization_Id INTEGER,
    FOREIGN KEY (SupervisorType_Id) REFERENCES SupervisorType(Id),
    FOREIGN KEY (Organization_Id) REFERENCES Organization(Id)
);

CREATE TABLE Traineeship_TraineeshipSupervisor (
    Traineeship_Id INTEGER NOT NULL,
    TraineeshipSupervisor_Id INTEGER NOT NULL,
    PRIMARY KEY (Traineeship_Id, TraineeshipSupervisor_Id),
    FOREIGN KEY (Traineeship_Id) REFERENCES Traineeship(Id),
    FOREIGN KEY (TraineeshipSupervisor_Id) REFERENCES TraineeshipSupervisor(Id)
);

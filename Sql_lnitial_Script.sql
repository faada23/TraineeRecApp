CREATE TYPE SupervisorTypeEnum AS ENUM ('FromOrganization', 'FromUniversity');
CREATE TYPE TraineeshipStatusEnum AS ENUM ('planned', 'in_progress', 'completed', 'canceled');

CREATE TABLE Speciality (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(120) NOT NULL
);

CREATE TABLE Student (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    Birthday DATE NOT NULL,
    PhoneNumber VARCHAR(30) NOT NULL,
    GroupName VARCHAR(50) NOT NULL,
    Course INTEGER CHECK(Course > 0 AND Course < 7) NOT NULL,
    Speciality_Id INTEGER,
    FOREIGN KEY (Speciality_Id) REFERENCES Speciality(Id)
); 

CREATE TABLE Organization (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE TraineeshipSupervisor (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(120) NOT NULL,
    PhoneNumber VARCHAR(20),
    SupervizorType SupervisorTypeEnum NOT NULL
);

CREATE TABLE Traineeship (
    Id SERIAL PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status TraineeshipStatusEnum NOT NULL,
    Organization_Id INT NOT NULL,
    TraineeshipSupervizor_Id INT NOT NULL,
    FOREIGN KEY (Organization_Id) REFERENCES Organization(Id),
    FOREIGN KEY (TraineeshipSupervizor_Id) REFERENCES TraineeshipSupervisor(Id)
);

CREATE TABLE Student_Traineeship (
    Student_Id INTEGER NOT NULL,
    Traineeship_Id INTEGER NOT NULL,
    PRIMARY KEY (Student_Id, Traineeship_Id),
    FOREIGN KEY (Student_Id) REFERENCES Student(Id),
    FOREIGN KEY (Traineeship_Id) REFERENCES Traineeship(Id)
);

CREATE TABLE Traineeship_TraineeshipSupervisor (
    Traineeship_Id INTEGER NOT NULL,
    TraineeshipSupervisor_Id INTEGER NOT NULL,
    PRIMARY KEY (Traineeship_Id, TraineeshipSupervisor_Id),
    FOREIGN KEY (Traineeship_Id) REFERENCES Traineeship(Id),
    FOREIGN KEY (TraineeshipSupervisor_Id) REFERENCES TraineeshipSupervisor(Id)
);

CREATE TABLE Organization_TraineshipSupervisor (
    Organization_Id INTEGER NOT NULL,
    TraineeshipSupervisor_Id INTEGER NOT NULL,
    PRIMARY KEY (Organization_Id, TraineeshipSupervisor_Id),
    FOREIGN KEY (Organization_Id) REFERENCES Organization(Id),
    FOREIGN KEY (TraineeshipSupervisor_Id) REFERENCES TraineeshipSupervisor(Id)
);

CREATE TABLE Organization_Traineeship (
    Organization_Id INTEGER NOT NULL,
    Traineeship_Id INTEGER NOT NULL,
    PRIMARY KEY (Organization_Id, Traineeship_Id),
    FOREIGN KEY (Organization_Id) REFERENCES Organization(Id),
    FOREIGN KEY (Traineeship_Id) REFERENCES Traineeship(Id)
);
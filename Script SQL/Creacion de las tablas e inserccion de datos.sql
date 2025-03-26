CREATE TABLE Subject (
    SubjectID INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName NVARCHAR(100) NOT NULL,
    CreditValue DECIMAL(5,2) NOT NULL
);
GO

CREATE TABLE Professor (
    ProfessorID INT IDENTITY(1,1) PRIMARY KEY,
    ProfessorName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE AssignedSubjects (
    AssignedSubjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProfessorID INT NOT NULL,
    SubjectID INT NOT NULL,
    CONSTRAINT FK_AssignedSubjects_Professor FOREIGN KEY (ProfessorID) REFERENCES Professor(ProfessorID),
    CONSTRAINT FK_AssignedSubjects_Subject FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
);
GO

CREATE TABLE Credits (
    CreditID INT IDENTITY(1,1) PRIMARY KEY,
    InitialCredits INT NOT NULL,
    AvailableCredits INT NOT NULL,
    SpentCredits INT NOT NULL
);
GO

CREATE TABLE Student (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Identification NVARCHAR(50) NOT NULL UNIQUE,
    CreditID INT NOT NULL UNIQUE,
    CONSTRAINT FK_Student_Credits FOREIGN KEY (CreditID) REFERENCES Credits(CreditID)
);
GO

CREATE TABLE Class (
    ClassID INT IDENTITY(1,1) PRIMARY KEY,
    SubjectID INT NOT NULL,
    StudentID INT NOT NULL,

    CONSTRAINT FK_Class_Subject FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID),
    CONSTRAINT FK_Class_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
);
GO

-- Insertar Datos
INSERT INTO Subject (SubjectName, CreditValue) 
VALUES 
    ('Matem�ticas', 3),
    ('F�sica', 3),
    ('Qu�mica', 3),
    ('Biolog�a', 3),
    ('Historia', 3),
    ('Geograf�a', 3),
    ('Literatura Espa�ola', 3),
    ('Ciencias de la Computaci�n', 3),
    ('Econom�a', 3),
    ('Arte', 3);
GO

INSERT INTO Professor (ProfessorName) 
VALUES 
    ('Juan P�rez'),
    ('Mar�a G�mez'),
    ('Carlos Ram�rez'),
    ('Ana Torres'),
    ('Luis Mart�nez');
GO

INSERT INTO AssignedSubjects (ProfessorID, SubjectID)
VALUES 
    (1, 1),
    (1, 2),
    (2, 3),
    (2, 4),
    (3, 5),
    (3, 6),
    (4, 7),
    (4, 8),
    (5, 9),
    (5, 10);
GO


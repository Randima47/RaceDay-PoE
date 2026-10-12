-- RaceDay API Database Schema Script

CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE Events (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME2 NOT NULL,
    Location NVARCHAR(150) NOT NULL
);

CREATE TABLE EventCategories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(50) NOT NULL,
    DistanceKM DECIMAL(5, 2) NOT NULL,
    EntryFee DECIMAL(10, 2) NOT NULL,
    EventId INT NOT NULL,
    CONSTRAINT FK_EventCategories_Events FOREIGN KEY (EventId) REFERENCES Events(EventId)
);

CREATE TABLE Enrolments (
    EnrolmentId INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentDate DATETIME2 NOT NULL,
    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Enrolments_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Enrolments_EventCategories FOREIGN KEY (CategoryId) REFERENCES EventCategories(CategoryId)
);

CREATE TABLE Results (
    ResultId INT IDENTITY(1,1) PRIMARY KEY,
    FinishTime TimeSpan NULL,
    Position INT NULL,
    EnrolmentId INT NOT NULL,
    CONSTRAINT FK_Results_Enrolments FOREIGN KEY (EnrolmentId) REFERENCES Enrolments(EnrolmentId)
);
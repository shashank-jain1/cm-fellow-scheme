-- ===============================================================================
-- CM FELLOW MANAGEMENT SYSTEM - COMPLETE SQL DATABASE CREATION SCRIPT
-- Target Database: SQL Server (Transact-SQL) / CmSchemeDb
-- Based on CM Fellow Module Pages Filled Validation Specifications & EF Core Models
-- ===============================================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'CmSchemeDb')
BEGIN
    CREATE DATABASE [CmSchemeDb];
END
GO

USE [CmSchemeDb];
GO

-- 1. MASTERS MODULE TABLES
IF OBJECT_ID(N'dbo.States', N'U') IS NULL
CREATE TABLE dbo.States (
    StateId INT IDENTITY(1,1) PRIMARY KEY,
    StateName NVARCHAR(100) NOT NULL,
    StateCode NVARCHAR(10) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.Divisions', N'U') IS NULL
CREATE TABLE dbo.Divisions (
    DivisionId INT IDENTITY(1,1) PRIMARY KEY,
    DivisionName NVARCHAR(100) NOT NULL,
    DivisionCode NVARCHAR(10) NULL,
    StateId INT NOT NULL FOREIGN KEY REFERENCES dbo.States(StateId),
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.Districts', N'U') IS NULL
CREATE TABLE dbo.Districts (
    DistrictId INT IDENTITY(1,1) PRIMARY KEY,
    DistrictName NVARCHAR(100) NOT NULL,
    DistrictCode NVARCHAR(10) NULL,
    DivisionId INT NOT NULL FOREIGN KEY REFERENCES dbo.Divisions(DivisionId),
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.Blocks', N'U') IS NULL
CREATE TABLE dbo.Blocks (
    BlockId INT IDENTITY(1,1) PRIMARY KEY,
    BlockName NVARCHAR(100) NOT NULL,
    BlockCode NVARCHAR(10) NULL,
    DistrictId INT NOT NULL FOREIGN KEY REFERENCES dbo.Districts(DistrictId),
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.GramPanchayats', N'U') IS NULL
CREATE TABLE dbo.GramPanchayats (
    GramPanchayatId INT IDENTITY(1,1) PRIMARY KEY,
    GramPanchayatName NVARCHAR(100) NOT NULL,
    BlockId INT NOT NULL FOREIGN KEY REFERENCES dbo.Blocks(BlockId),
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.Projects', N'U') IS NULL
CREATE TABLE dbo.Projects (
    ProjectId INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(200) NOT NULL,
    ProjectCode NVARCHAR(50) NULL,
    Department NVARCHAR(150) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

IF OBJECT_ID(N'dbo.Works', N'U') IS NULL
CREATE TABLE dbo.Works (
    WorkId INT IDENTITY(1,1) PRIMARY KEY,
    WorkTitle NVARCHAR(200) NOT NULL,
    ProjectId INT NOT NULL FOREIGN KEY REFERENCES dbo.Projects(ProjectId),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- 2. USER REGISTRATION & AUTHENTICATION (URM&A)
IF OBJECT_ID(N'dbo.UserAccounts', N'U') IS NULL
CREATE TABLE dbo.UserAccounts (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

IF OBJECT_ID(N'dbo.Applicants', N'U') IS NULL
CREATE TABLE dbo.Applicants (
    ApplicantId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    MiddleName NVARCHAR(100) NULL,
    LastName NVARCHAR(100) NOT NULL,
    FatherName NVARCHAR(150) NOT NULL,
    AadhaarNumber NVARCHAR(12) NULL,
    PANNumber NVARCHAR(10) NULL,
    DrivingLicenseNumber NVARCHAR(20) NULL,
    SamagraId NVARCHAR(9) NULL,
    MobileNumber NVARCHAR(10) NOT NULL,
    EmailId NVARCHAR(150) NOT NULL,
    PermanentAddress NVARCHAR(500) NOT NULL,
    PinCode NVARCHAR(6) NOT NULL,
    BoardUniversityName NVARCHAR(200) NOT NULL,
    PercentageCGPA DECIMAL(5,2) NULL,
    ExperienceDetails NVARCHAR(1000) NULL,
    PhotographPath NVARCHAR(255) NULL,
    IdentityProofPath NVARCHAR(255) NULL,
    EducationalCertificatePath NVARCHAR(255) NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- 3. WORK ALLOCATION & TASK MANAGEMENT
IF OBJECT_ID(N'dbo.WorkAllocations', N'U') IS NULL
CREATE TABLE dbo.WorkAllocations (
    WorkAllocationId INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    WorkProjectId INT NOT NULL,
    WorkDescription NVARCHAR(2000) NOT NULL,
    Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    SurveysPerIntern INT NOT NULL DEFAULT 0,
    DivisionId INT NOT NULL,
    DistrictId INT NOT NULL,
    BlockId INT NOT NULL,
    CompletionPercentage INT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    CreatedBy NVARCHAR(200) NULL,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedBy NVARCHAR(200) NULL,
    ModifiedOn DATETIME2 NULL
);

IF OBJECT_ID(N'dbo.TaskProgresses', N'U') IS NULL
CREATE TABLE dbo.TaskProgresses (
    TaskProgressId INT IDENTITY(1,1) PRIMARY KEY,
    WorkAllocationId INT NOT NULL FOREIGN KEY REFERENCES dbo.WorkAllocations(WorkAllocationId),
    FellowName NVARCHAR(150) NOT NULL,
    CompletedSurveys INT NOT NULL DEFAULT 0,
    TotalAssignedSurveys INT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT 'In Progress',
    LastUpdated DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

IF OBJECT_ID(N'dbo.SurveyRecords', N'U') IS NULL
CREATE TABLE dbo.SurveyRecords (
    SurveyRecordId INT IDENTITY(1,1) PRIMARY KEY,
    WorkAllocationId INT NOT NULL FOREIGN KEY REFERENCES dbo.WorkAllocations(WorkAllocationId),
    RespondentName NVARCHAR(150) NOT NULL,
    GramPanchayatId INT NOT NULL,
    SurveyDate DATE NOT NULL,
    Remarks NVARCHAR(500) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Submitted'
);
GO

-- 4. ATTENDANCE & LEAVE MANAGEMENT
IF OBJECT_ID(N'dbo.Attendances', N'U') IS NULL
CREATE TABLE dbo.Attendances (
    AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    CheckInTime TIME NULL,
    CheckOutTime TIME NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Present',
    Remarks NVARCHAR(250) NULL,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

IF OBJECT_ID(N'dbo.LeaveApplications', N'U') IS NULL
CREATE TABLE dbo.LeaveApplications (
    LeaveApplicationId INT IDENTITY(1,1) PRIMARY KEY,
    LeaveApplicationNo NVARCHAR(50) NOT NULL UNIQUE,
    UserId INT NOT NULL,
    LeaveType NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    HalfDayFullDay NVARCHAR(20) NOT NULL DEFAULT 'Full Day',
    NumberOfDays INT NOT NULL DEFAULT 1,
    LeaveReason NVARCHAR(1000) NOT NULL,
    AttachmentPath NVARCHAR(500) NULL,
    ReportingManagerName NVARCHAR(200) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    ApprovedBy NVARCHAR(200) NULL,
    ApprovalDate DATE NULL,
    CreatedBy NVARCHAR(200) NULL,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

IF OBJECT_ID(N'dbo.LeaveBalances', N'U') IS NULL
CREATE TABLE dbo.LeaveBalances (
    LeaveBalanceId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TotalCasualLeaves INT NOT NULL DEFAULT 12,
    UsedCasualLeaves INT NOT NULL DEFAULT 0,
    TotalMedicalLeaves INT NOT NULL DEFAULT 10,
    UsedMedicalLeaves INT NOT NULL DEFAULT 0,
    TotalRestrictedHolidays INT NOT NULL DEFAULT 2,
    UsedRestrictedHolidays INT NOT NULL DEFAULT 0
);
GO

-- 5. TRAINING MANAGEMENT SYSTEM (TMS)
IF OBJECT_ID(N'dbo.TrainingSchedules', N'U') IS NULL
CREATE TABLE dbo.TrainingSchedules (
    TrainingId INT IDENTITY(1,1) PRIMARY KEY,
    TrainingTitle NVARCHAR(200) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    TrainerName NVARCHAR(150) NULL,
    TrainerMobile NVARCHAR(20) NULL,
    TrainingDate DATE NOT NULL,
    StartTime NVARCHAR(10) NOT NULL,
    EndTime NVARCHAR(10) NOT NULL,
    Mode NVARCHAR(20) NOT NULL DEFAULT 'Offline',
    Location NVARCHAR(200) NULL,
    Remarks NVARCHAR(1000) NULL,
    AttendanceRequired BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- 6. PERFORMANCE TRACKING (MONITORING)
IF OBJECT_ID(N'dbo.PerformanceEvaluations', N'U') IS NULL
CREATE TABLE dbo.PerformanceEvaluations (
    PerformanceEvaluationId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantId INT NOT NULL,
    ApplicantName NVARCHAR(150) NOT NULL,
    ProjectName NVARCHAR(200) NOT NULL,
    CompletionPercentage INT NOT NULL DEFAULT 0,
    PerformanceScore INT NOT NULL DEFAULT 0,
    PerformanceGrade NVARCHAR(10) NOT NULL DEFAULT 'B',
    SupervisorRating DECIMAL(3,2) NULL,
    EvaluationRemarks NVARCHAR(1000) NULL,
    PerformanceStatus NVARCHAR(50) NOT NULL DEFAULT 'In Review',
    EvaluatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- 7. CERTIFICATE GENERATION & EXIT MANAGEMENT
IF OBJECT_ID(N'dbo.CertificateApplications', N'U') IS NULL
CREATE TABLE dbo.CertificateApplications (
    CertificateApplicationId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantId INT NOT NULL,
    FellowName NVARCHAR(150) NOT NULL,
    TenureCompletedMonths INT NOT NULL DEFAULT 12,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    CertificateUrl NVARCHAR(500) NULL,
    AppliedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

IF OBJECT_ID(N'dbo.ExitRecords', N'U') IS NULL
CREATE TABLE dbo.ExitRecords (
    ExitRecordId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantId INT NOT NULL,
    NoDuesCleared BIT NOT NULL DEFAULT 0,
    AssetsReturned BIT NOT NULL DEFAULT 0,
    FinalReportSubmitted BIT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT 'In Progress',
    ExitDate DATE NULL
);
GO

-- 8. HELP DESK & SUPPORT TICKETS
IF OBJECT_ID(N'dbo.Tickets', N'U') IS NULL
CREATE TABLE dbo.Tickets (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(200) NOT NULL,
    Mobile NVARCHAR(20) NULL,
    IssueCategory NVARCHAR(100) NOT NULL,
    IssueDescription NVARCHAR(2000) NOT NULL,
    Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
    Status NVARCHAR(50) NOT NULL DEFAULT 'open',
    ResolutionRemarks NVARCHAR(2000) NULL,
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ResolvedOn DATETIME2 NULL
);
GO

-- ===============================================================================
-- SEED INITIAL MASTER DATA
-- ===============================================================================
INSERT INTO dbo.States (StateName, StateCode) VALUES ('Madhya Pradesh', 'MP');

INSERT INTO dbo.Divisions (DivisionName, DivisionCode, StateId) VALUES 
('Bhopal', 'BPL', 1),
('Indore', 'IND', 1),
('Jabalpur', 'JBP', 1),
('Gwalior', 'GWL', 1);

INSERT INTO dbo.Districts (DistrictName, DistrictCode, DivisionId) VALUES 
('Bhopal', 'BPL-D', 1),
('Sehore', 'SEH-D', 1),
('Indore', 'IND-D', 2),
('Barwani', 'BAR-D', 2);

INSERT INTO dbo.Blocks (BlockName, BlockCode, DistrictId) VALUES 
('Phanda', 'PHA', 1),
('Sehore Urban', 'SEH', 2),
('Barwani', 'BAR', 4);

INSERT INTO dbo.Projects (ProjectName, ProjectCode, Department) VALUES 
('Aspirational District Baseline Survey', 'PRJ-001', 'Planning'),
('Rural Infrastructure Verification', 'PRJ-002', 'Panchayati Raj'),
('Health & Nutrition Outreach Evaluation', 'PRJ-003', 'Health');

INSERT INTO dbo.WorkAllocations (ProjectId, WorkProjectId, WorkDescription, Priority, StartDate, EndDate, SurveysPerIntern, DivisionId, DistrictId, BlockId, CompletionPercentage, Status)
VALUES 
(1, 101, 'Aspirational District Baseline Survey & Monitoring', 'High', '2026-06-01', '2026-08-31', 50, 1, 1, 1, 85, 'active'),
(2, 102, 'Rural Infrastructure Verification & Geo-tagging', 'Medium', '2026-07-01', '2026-09-30', 35, 2, 4, 3, 45, 'pending');

PRINT 'Database CmSchemeDb schema and seed data initialized successfully!';
GO

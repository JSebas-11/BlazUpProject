CREATE DATABASE BlazUpProject;
GO
USE BlazUpProject;
GO

--TABLES CREATION

-- -------------------------- state-type Tables --------------------------
CREATE TABLE UserRole (
	UserRoleId INT PRIMARY KEY, --( Admin, Client, Developer )
	UserRoleDescription NVARCHAR(48) UNIQUE NOT NULL
);

CREATE TABLE RequirementType (
	ReqTypeId INT PRIMARY KEY, --( FixBug, NewFeature, Change, Refactor, Testing, Deisgn, Documentation, Other )
	ReqTypeDescription NVARCHAR(48) UNIQUE NOT NULL
);

CREATE TABLE StateNotification (
	NotStateId INT PRIMARY KEY, --( UnRead, Read )
	NotStateDescription NVARCHAR(48) UNIQUE NOT NULL
);

CREATE TABLE StateEntity (
	EntStateId INT PRIMARY KEY, --( Pending, InProgress, Postponed, Finished, Completed )
	EntStateDescription NVARCHAR(48) UNIQUE NOT NULL
);

CREATE TABLE LevelPriority (
	PriorityId INT PRIMARY KEY, --( VeryLow, Low, Normal, High, Critical );
	PriorityDescription NVARCHAR(48) UNIQUE NOT NULL
);

-- -------------------------- entities Tables --------------------------
CREATE TABLE UserInfo (
    UserInfoId INT PRIMARY KEY IDENTITY(1, 1),
    Dni VARCHAR(13) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(60) NOT NULL,
    UserName NVARCHAR(64) NOT NULL,
    RoleId INT NULL,

	CONSTRAINT FK_UserInfo_UserRole FOREIGN KEY (RoleId) 
		REFERENCES UserRole(UserRoleId) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE Project (
    ProjectId INT PRIMARY KEY IDENTITY(1, 1),
    ProjectName NVARCHAR(64) NOT NULL,
	ProjectDescription NVARCHAR(512) NOT NULL,
    InitialDate DATETIME NOT NULL DEFAULT GETDATE(),
	DeadLine DATETIME NULL,
    Progress DECIMAL(5, 2) NOT NULL DEFAULT 0,
	ProjectStateId INT NULL,
	CreatedById INT NULL,

    CONSTRAINT FK_Project_StateEntity FOREIGN KEY (ProjectStateId) 
		REFERENCES StateEntity(EntStateId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Project_UserInfo FOREIGN KEY (CreatedById) 
		REFERENCES UserInfo(UserInfoId) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE Goal (
    GoalId BIGINT PRIMARY KEY IDENTITY(1, 1),
	GoalName NVARCHAR(64) NOT NULL,
    GoalDescription NVARCHAR(512) NOT NULL,
    GoalStateId INT NULL,
	PriorityId INT NULL,
	ProjectId INT NOT NULL,

	CONSTRAINT FK_Goal_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_Goal_StateEntity FOREIGN KEY (GoalStateId) 
		REFERENCES StateEntity(EntStateId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Goal_LevelPriority FOREIGN KEY (PriorityId) 
		REFERENCES LevelPriority(PriorityId) ON DELETE SET NULL ON UPDATE CASCADE,
);

CREATE TABLE Requirement (
    ReqId BIGINT PRIMARY KEY IDENTITY(1, 1),
    ReqName NVARCHAR(64) NOT NULL,
	ReqDescription NVARCHAR(512) NOT NULL,
	DeadLine DATETIME NULL,
	Progress DECIMAL(5, 2) NOT NULL DEFAULT 0,
	ProjectId INT NOT NULL,
	ReqTypeId INT NULL,
    PriorityId INT NULL,
	ReqStateId INT NULL,
	CreatedById INT NULL,

	CONSTRAINT FK_Requirement_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_Requirement_RequirementType FOREIGN KEY (ReqTypeId) 
		REFERENCES RequirementType(ReqTypeId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Requirement_LevelPriority FOREIGN KEY (PriorityId) 
		REFERENCES LevelPriority(PriorityId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Requirement_StateEntity FOREIGN KEY (ReqStateId) 
		REFERENCES StateEntity(EntStateId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Requirement_UserInfo FOREIGN KEY (CreatedById) 
		REFERENCES UserInfo(UserInfoId) ON DELETE SET NULL ON UPDATE CASCADE
	
);

CREATE TABLE Task (
    TaskId BIGINT PRIMARY KEY IDENTITY(1, 1),
    TaskName NVARCHAR(64) NOT NULL,
	InitialDate DATETIME NOT NULL DEFAULT GETDATE(),
	DeadLine DATETIME NULL,
	TaskDescription NVARCHAR(512) NOT NULL,
	Progress DECIMAL(5, 2) NOT NULL DEFAULT 0,
	RequirementId BIGINT NOT NULL,
    PriorityId INT NULL,
	TaskStateId INT NULL,
	CreatedById INT NULL,

	CONSTRAINT FK_Task_Requirement FOREIGN KEY (RequirementId) REFERENCES Requirement(ReqId),
    CONSTRAINT FK_Task_LevelPriority FOREIGN KEY (PriorityId) 
		REFERENCES LevelPriority(PriorityId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Task_StateEntity FOREIGN KEY (TaskStateId) 
		REFERENCES StateEntity(EntStateId) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT FK_Task_UserInfo FOREIGN KEY (CreatedById) 
		REFERENCES UserInfo(UserInfoId) ON DELETE SET NULL ON UPDATE CASCADE
	
);

CREATE TABLE NotificationApp (
    NotificationId BIGINT PRIMARY KEY IDENTITY(1, 1),
    NotificationTitle NVARCHAR(64) NOT NULL,
	NotificationMessage NVARCHAR(128) NOT NULL,
    NotStateId INT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_NotificationApp_StateNotification FOREIGN KEY (NotStateId) 
		REFERENCES StateNotification(NotStateId) ON DELETE SET NULL ON UPDATE CASCADE
	
);

-- -------------------------- relationship Tables --------------------------
CREATE TABLE UserNotification (
    NotificationId BIGINT NOT NULL,
	UserId INT NOT NULL,

	CONSTRAINT PK_UserNotification PRIMARY KEY (UserId, NotificationId),
    CONSTRAINT FK_UserNotification_NotificationApp FOREIGN KEY (NotificationId) 
		REFERENCES NotificationApp(NotificationId) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_UserNotification_UserInfo FOREIGN KEY (UserId) 
		REFERENCES UserInfo(UserInfoId) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE ProjectMember (
    ProjectId INT NOT NULL,
	UserId INT NOT NULL,
	JoinedOn DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_ProjectMember PRIMARY KEY (UserId, ProjectId),
    CONSTRAINT FK_ProjectMember_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
	CONSTRAINT FK_ProjectMember_UserInfo FOREIGN KEY (UserId) 
		REFERENCES UserInfo(UserInfoId) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE UserTask (
    TaskId BIGINT NOT NULL,
	UserId INT NOT NULL,
	AssignedOn DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_UserTask PRIMARY KEY (UserId, TaskId),
    CONSTRAINT FK_UserTask_Task FOREIGN KEY (TaskId) REFERENCES Task(TaskId),
	CONSTRAINT FK_UserTask_UserInfo FOREIGN KEY (UserId) 
		REFERENCES UserInfo(UserInfoId) ON DELETE CASCADE ON UPDATE CASCADE
);

-- -------------------------- tracking Tables --------------------------
CREATE TABLE UserLog (
	UserLogId INT PRIMARY KEY IDENTITY(1, 1),
	UserId INT NOT NULL,
	RegisteredAt DATETIME NULL,
	DeletedAt DATETIME NULL
);

-- -------------------------- triggers Creation --------------------------
GO
CREATE TRIGGER UserInfo_AI
	ON UserInfo AFTER INSERT AS
	BEGIN
		INSERT INTO UserLog (UserId, RegisteredAt)
			SELECT UserInfoId, GETDATE() FROM inserted
	END;

GO
CREATE TRIGGER UserInfo_AD
	ON UserInfo AFTER DELETE AS
	BEGIN
		UPDATE ul
			SET DeletedAt = GETDATE()
			FROM UserLog ul
			INNER JOIN deleted d ON ul.UserId = d.UserInfoId;
	END;

-- -------------------------- static data insertion --------------------------
INSERT INTO UserRole 
VALUES (1, 'Admin'), (2, 'Client'), (3, 'Developer');

INSERT INTO RequirementType
VALUES (1, 'FixBug'), (2, 'NewFeature'), (3, 'Change'), (4, 'Refactor'), (5, 'Testing'), (6, 'Design'), (7, 'Documentation'), (8, 'Other');

INSERT INTO StateNotification
VALUES (1, 'UnRead'), (2, 'Read');

INSERT INTO StateEntity
VALUES (1, 'Pending'), (2, 'InProgress'), (3, 'Postponed'), (4, 'Finished'), (5, 'Completed');

INSERT INTO LevelPriority
VALUES (1, 'VeryLow'), (2, 'Low'), (3, 'Normal'), (4, 'High'), (5, 'Critical');
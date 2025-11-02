-- Crear la base de datos
CREATE DATABASE TaskManagerDB;
GO

USE TaskManagerDB;
GO

-- Tabla de Usuarios
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100),
    UpdatedBy NVARCHAR(100)
);
GO

-- Tabla de Tareas
CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TaskCode NVARCHAR(50) NOT NULL UNIQUE,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status BIT NOT NULL DEFAULT 0,
    DueDate DATETIME2 NULL,
    Priority INT NOT NULL DEFAULT 2, -- 1: Low, 2: Medium, 3: High
    UserId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100),
    UpdatedBy NVARCHAR(100),
    
    -- Clave foránea
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId) 
        REFERENCES Users(Id) ON DELETE CASCADE
);
GO

-- Índices para mejorar el rendimiento
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Tasks_TaskCode ON Tasks(TaskCode);
CREATE INDEX IX_Tasks_UserId ON Tasks(UserId);
CREATE INDEX IX_Tasks_Status ON Tasks(Status);
CREATE INDEX IX_Tasks_DueDate ON Tasks(DueDate);
CREATE INDEX IX_Tasks_CreatedAt ON Tasks(CreatedAt);
GO

-- Datos de prueba (Opcional) Password: admin123
INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
VALUES 
('admin', 'admin@taskmanager.com', '$2a$12$Tg2AERW/xyrqSh/cY3Bhm.cxV82zFPL8BzAFZU4y3nZu33o1YIrna', 'Admin', 'User', GETUTCDATE(), GETUTCDATE(), 'system', 'system'),
('juan.perez', 'juan.perez@email.com', '$2a$12$Tg2AERW/xyrqSh/cY3Bhm.cxV82zFPL8BzAFZU4y3nZu33o1YIrna', 'Juan', 'Pérez', GETUTCDATE(), GETUTCDATE(), 'system', 'system');
GO

INSERT INTO Tasks (TaskCode, Title, Description, Status, DueDate, Priority, UserId, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
VALUES 
('TASK-20240101120000-ABC123', 'Configurar proyecto', 'Configurar la estructura inicial del proyecto Angular y .NET', 1, DATEADD(DAY, 7, GETUTCDATE()), 2, 1, GETUTCDATE(), GETUTCDATE(), '1', '1'),
('TASK-20240101120001-DEF456', 'Implementar autenticación', 'Implementar sistema de login y registro con JWT', 0, DATEADD(DAY, 5, GETUTCDATE()), 3, 1, GETUTCDATE(), GETUTCDATE(), '1', '1'),
('TASK-20240101120002-GHI789', 'Diseñar base de datos', 'Crear el modelo de datos y relaciones', 1, DATEADD(DAY, 3, GETUTCDATE()), 2, 2, GETUTCDATE(), GETUTCDATE(), '2', '2'),
('TASK-20240101120003-JKL012', 'Crear API de tareas', 'Desarrollar endpoints CRUD para gestión de tareas', 0, DATEADD(DAY, 10, GETUTCDATE()), 2, 2, GETUTCDATE(), GETUTCDATE(), '2', '2');
GO

-- Stored Procedures útiles (Opcionales)

-- SP para obtener estadísticas de tareas por usuario
CREATE PROCEDURE GetTaskStatsByUser
    @UserId INT
AS
BEGIN
    SELECT 
        COUNT(*) as TotalTasks,
        SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) as CompletedTasks,
        SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) as PendingTasks,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) AS DECIMAL) / COUNT(*) * 100 
            ELSE 0 
        END as CompletionPercentage
    FROM Tasks 
    WHERE UserId = @UserId;
END
GO

-- SP para obtener tareas recientes
CREATE PROCEDURE GetRecentTasks
    @UserId INT,
    @TopCount INT = 5
AS
BEGIN
    SELECT TOP (@TopCount) *
    FROM Tasks 
    WHERE UserId = @UserId
    ORDER BY CreatedAt DESC;
END
GO

-- Consultas de verificación
SELECT 'Usuarios creados:' as Info;
SELECT Id, Username, Email FROM Users;

SELECT 'Tareas creadas:' as Info;
SELECT Id, TaskCode, Title, Status, UserId FROM Tasks;

SELECT 'Estadísticas de tareas por usuario:' as Info;
SELECT 
    u.Username,
    COUNT(t.Id) as TotalTasks,
    SUM(CASE WHEN t.Status = 1 THEN 1 ELSE 0 END) as CompletedTasks,
    SUM(CASE WHEN t.Status = 0 THEN 1 ELSE 0 END) as PendingTasks
FROM Users u
LEFT JOIN Tasks t ON u.Id = t.UserId
GROUP BY u.Id, u.Username;
GO
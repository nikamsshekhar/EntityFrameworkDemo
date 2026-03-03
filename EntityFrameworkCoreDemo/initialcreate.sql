IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Address] (
    [Id] int NOT NULL IDENTITY,
    [AddressLine1] nvarchar(max) NOT NULL,
    [AddressLine2] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [PinCode] int NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Organizations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [AddressId] int NOT NULL,
    [Phone] int NOT NULL,
    [PAN] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Organizations_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Type] int NOT NULL,
    [OrganizationId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [AddressId] int NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customers_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id])
);
GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [Role] int NOT NULL,
    [ManagerId] int NOT NULL,
    [OrganizationId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [AddressId] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_Employees_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id])
);
GO

CREATE INDEX [IX_Customers_AddressId] ON [Customers] ([AddressId]);
GO

CREATE INDEX [IX_Customers_OrganizationId] ON [Customers] ([OrganizationId]);
GO

CREATE INDEX [IX_Employees_AddressId] ON [Employees] ([AddressId]);
GO

CREATE INDEX [IX_Employees_ManagerId] ON [Employees] ([ManagerId]);
GO

CREATE INDEX [IX_Employees_OrganizationId] ON [Employees] ([OrganizationId]);
GO

CREATE INDEX [IX_Organizations_AddressId] ON [Organizations] ([AddressId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260303105416_InitialCreate', N'8.0.24');
GO

COMMIT;
GO


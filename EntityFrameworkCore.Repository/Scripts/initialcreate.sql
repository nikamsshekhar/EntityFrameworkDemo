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

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [AddressLine1] nvarchar(max) NOT NULL,
    [AddressLine2] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [PinCode] int NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Organizations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Phone] int NOT NULL,
    [PAN] nvarchar(max) NOT NULL,
    [AddressId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Organizations_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Type] int NOT NULL,
    [OrganizationId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [AddressId] int NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]),
    CONSTRAINT [FK_Customers_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [Role] int NULL,
    [ManagerId] int NULL,
    [OrganizationId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [AddressId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]),
    CONSTRAINT [FK_Employees_Employees_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Employees_Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations] ([Id]) ON DELETE CASCADE
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
VALUES (N'20260303174303_InitialCreate', N'8.0.24');
GO

COMMIT;
GO


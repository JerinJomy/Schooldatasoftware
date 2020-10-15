CREATE TABLE [dbo].[Staffs] (
    [StaffId] INT          IDENTITY (1, 1) NOT NULL,
    [TypeNo]  INT          NULL,
    [Name]    VARCHAR (50) NULL,
    [Phone]   VARCHAR (50) NULL,
    [Email]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([StaffId] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Phone] ASC)
);


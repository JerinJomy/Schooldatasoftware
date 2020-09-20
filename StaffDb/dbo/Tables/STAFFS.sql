CREATE TABLE [dbo].[Staffs] (
    [StaffId] INT          IDENTITY (1, 1) NOT NULL,
    [TypeNo]  INT          NOT NULL,
    [Name]    VARCHAR (50) NOT NULL,
    [Phone]   VARCHAR (50) NOT NULL,
    [Email]   VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([StaffId] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Phone] ASC)
);


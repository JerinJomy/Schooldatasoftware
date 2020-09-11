CREATE TABLE [dbo].[AdministrativeStaff] (
    [Designation] VARCHAR (50) NOT NULL,
    [Staffid]     INT          NOT NULL,
    FOREIGN KEY ([Staffid]) REFERENCES [dbo].[Staffs] ([StaffId])
);


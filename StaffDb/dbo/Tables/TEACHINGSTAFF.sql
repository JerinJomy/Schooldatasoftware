﻿CREATE TABLE [dbo].[TeachingStaff] (
    [classname] VARCHAR (50) NOT NULL,
    [subject]   VARCHAR (50) NOT NULL,
    [Staffid]   INT          NOT NULL,
    FOREIGN KEY ([Staffid]) REFERENCES [dbo].[Staffs] ([StaffId])
);

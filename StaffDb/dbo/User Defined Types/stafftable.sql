CREATE TYPE [dbo].[stafftable] AS TABLE (
    [staffid]       INT          NOT NULL,
    [typeno]        INT          NOT NULL,
    [name]          VARCHAR (50) NOT NULL,
    [phone]         VARCHAR (50) NOT NULL,
    [email]         VARCHAR (50) NOT NULL,
    [designation_a] VARCHAR (50) NULL,
    [designation_s] VARCHAR (50) NULL,
    [classname]     VARCHAR (50) NULL,
    [subject]       VARCHAR (50) NULL);


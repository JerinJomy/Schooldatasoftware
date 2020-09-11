CREATE PROCEDURE [dbo].[proc_InsertStaffs]
@stafftable stafftable readonly
AS
BEGIN

 MERGE 
   ADMINISTRATIVESTAFF AS target
USING 
  (select designation_a,staffid from @stafftable  where typeno=2) as source
ON 
   target.staffid= source.staffid 
 
WHEN MATCHED  THEN 
             UPDATE SET  
                     target.designation= source.designation_a

WHEN NOT MATCHED THEN 
 
    INSERT (staffid,designation)
     VALUES  (source.staffid,source.designation_a)

WHEN NOT MATCHED by source THEN delete;

 MERGE 
   SUPPORTSTAFF AS target
USING 
   (select designation_s,staffid from @stafftable  where typeno=3) as source
ON 
   target.staffid= source.staffid 
 
WHEN MATCHED  THEN 
             UPDATE SET  
                     target.designation= source.designation_S

WHEN NOT MATCHED THEN 
 
    INSERT (staffid,designation)
     VALUES  (source.staffid,source.designation_s)

WHEN NOT MATCHED by source THEN delete;

 MERGE 
   TEACHINGSTAFF AS target
USING 
   (select classname,subject,staffid from @stafftable  where typeno=1) as source
   ON 
   target.staffid= source.staffid 
 
WHEN MATCHED  THEN 
             UPDATE SET  
                     target.CLASSNAME= source.CLASSNAME,
					 target.SUBJECT= source.SUBJECT
				
WHEN NOT MATCHED THEN 
 
    INSERT (staffid,SUBJECT,CLASSNAME)
     VALUES  (source.staffid,source.SUBJECT,SOURCE.CLASSNAME)

WHEN NOT MATCHED by source THEN delete;

 MERGE 
   staffs AS target
USING 
   @stafftable AS source
ON 
   target.staffid= source.staffid
 
WHEN MATCHED THEN 
             UPDATE SET  
                     target.name= source.name,
					 target.typeno= source.typeno,
					 target.phone= source.phone,
					 target.email= source.email
 
WHEN NOT MATCHED THEN 
 
    INSERT (staffid,name,typeno,phone,email)
     VALUES  (source.staffid,source.name,source.typeno,source.phone,source.email)

WHEN NOT MATCHED by source THEN delete;

END
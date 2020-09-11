CREATE procedure [dbo].[proc_staffdata_getall] as 
begin
   select
      s.staffid,
      st.typeno,
      s.name,
      s.phone,
      s.email,
      a.designation AS designation_a,
      sp.designation as designation_s,
      t.classname,
      t.subject 
   from
      STAFFS s 
      inner join
         stafftype st 
         on s.typeno = st.typeno 
      full join
         ADMINISTRATIVESTAFF a 
         on a.staffid = s.staffid 
      full join
         supportstaff sp 
         on sp.staffid = s.staffid 
      full join
         teachingstaff t 
         on t.staffid = s.staffid 
end
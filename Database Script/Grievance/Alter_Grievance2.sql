Alter table [dbo].[Grievance]
drop column [Description],Remarks
go 
Alter table [dbo].[Grievance]
Add Description nvarchar(max),
 Remarks nvarchar(max)

Alter table [dbo].[Grievance]

Add 
GrievanceID bigint identity(1,1),
 Name nvarchar(255),
  UserType nvarchar(255),
  Gender nvarchar(15),
  Office nvarchar(255),
  Remarks nvarchar(255),
  Status nvarchar(255),
  IsDeleted bit,
  EmailID nvarchar(255),
  Description nvarchar(255),
  AttachmentFileInEnglish varbinary(max),
  EnglishFileName nvarchar(255),
  EnglishContentType nvarchar(255),
  Address nvarchar(255),
  GrievanceHeadAttachmentFileInEnglish varbinary(max),
  GrievanceHeadEnglishFileName nvarchar(255),
  GrievanceHeadEnglishContentType nvarchar(255),


GO


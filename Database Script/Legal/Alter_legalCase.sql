Alter table [dbo].[LegalCases]

Add  PetitionerName nvarchar(255),
  Email nvarchar(255),
  Mobile nvarchar(15),
  StatusType nvarchar(50),
  Department nvarchar(255),
  AttachmentFileInEnglish varbinary(max),
  EnglishFileName nvarchar(250),
  EnglishContentType nvarchar(250),

LegalAdvisorStatus nvarchar(255),
LegalAdvisorRemarks nvarchar(255)

GO

Alter table [dbo].[LegalCases]
ADD CONSTRAINT UC_CaseNo UNIQUE (CaseNo);
go
USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetCMSHospitalByParam]    Script Date: 25-02-2023 6.30.31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
create  PROCEDURE [dbo].[GetCMSHospitalByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
      ,[NameInEnglish]
      ,[NameInHindi]
     
      ,[AttachmentFileInEnglish]
		,EnglishFileName 
		,EnglishContentType 
      ,[AttachmentFileInHindi]
	  ,HindiFileName  
		,HindiContentType
      ,[IsDeleted]
    
	  
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[CMSHospital]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
GO



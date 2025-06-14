USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetEventByParam]    Script Date: 17-10-2022 5.39.01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
create  PROCEDURE [dbo].[GetEventByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
      ,[TitleInEnglish]
      ,[TitleInHindi]
     
      ,[EventDate]
      
      ,[AttachmentTitleInEnglish]
      ,[AttachmentTitleInHindi]
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
  FROM [DTLPension].[dbo].[Event]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
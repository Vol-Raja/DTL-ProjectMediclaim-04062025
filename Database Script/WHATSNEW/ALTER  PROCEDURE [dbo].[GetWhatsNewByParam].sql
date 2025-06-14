USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetWhatsNewByParam]    Script Date: 16-10-2022 9.09.00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER  PROCEDURE [dbo].[GetWhatsNewByParam]          
(          
 @Id  uniqueidentifier = NULL,          
 @IsArchieved bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
      ,[TitleInEnglish]
      ,[TitleInHindi]
     
      ,[WhatsNewDate]
     ,[WhatsNewDateHindi]
      ,[AttachmentTitleInEnglish]
      ,[AttachmentTitleInHindi]
      ,[AttachmentFileInEnglish]
		,EnglishFileName 
		,EnglishContentType 
      ,[AttachmentFileInHindi]
	  ,HindiFileName  
		,HindiContentType
      ,[IsDeleted]
      ,[IsArchieved]
	  ,	Description,
		DescriptionHindi,
		ImageFileName ,
		ImageContentType,
		Image
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[WhatsNew]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsArchieved = @IsArchieved) OR @IsArchieved IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
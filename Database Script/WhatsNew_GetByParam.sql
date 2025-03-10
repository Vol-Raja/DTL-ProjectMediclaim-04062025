 
CREATE  PROCEDURE [dbo].[GetWhatsNewByParam]          
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
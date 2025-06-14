USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetLinkByParam]    Script Date: 15-10-2022 3.33.34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetLinkByParam]          
(          
 @LinkId  uniqueidentifier = NULL,          
 @IsArchieved bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
      Title ,
	  TitleHindi,
	Link,
	DocumentFileName  ,

	ContentType  ,
	FileSize  ,
	FileContent 
	
      ,[IsDeleted]
      ,[IsArchieved]
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[Link]
       
  WHERE           
   ((ID = @LinkId) OR @LinkId IS NULL)          
   AND ((IsArchieved = @IsArchieved) OR @IsArchieved IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
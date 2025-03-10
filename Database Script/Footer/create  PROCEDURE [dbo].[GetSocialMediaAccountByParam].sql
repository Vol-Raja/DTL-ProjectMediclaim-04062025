USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetSocialMediaAccountByParam]    Script Date: 17-10-2022 9.24.38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
create  PROCEDURE [dbo].[GetSocialMediaAccountByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
     [Facebook]  ,
	[Instagram]  ,
	[Youtube]  ,
	[Twitter] 
      ,[IsDeleted]
    
	  
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[SocialMediaAccount]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
GO



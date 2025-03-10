USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetFeedbackByParam]    Script Date: 25-02-2023 7.06.38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE  PROCEDURE [dbo].[GetFeedbackByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
	[Name],
	[EmailAddress] ,
	 
	
	[Details],
	[PhoneNumber]
      ,[IsDeleted]
    
	  
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[Feedback]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
GO



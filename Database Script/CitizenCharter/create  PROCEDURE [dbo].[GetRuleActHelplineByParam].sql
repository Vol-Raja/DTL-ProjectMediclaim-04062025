USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetRuleActHelplineByParam]    Script Date: 18-10-2022 12.37.43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
create  PROCEDURE [dbo].[GetRuleActHelplineByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL,
 @IsHelpline bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
      ,[TitleInEnglish]
      ,[TitleInHindi]
     
      ,ContactNumber 
	,ContactNumberInHindi,
	 IsHelpline  ,
	HelplineDescription   ,
	HelplineDescriptionInHindi
      
     
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
  FROM [DTLPension].[dbo].[RuleActHelpline]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)  
   
   AND ((IsHelpline = @IsHelpline) OR @IsHelpline IS NULL)  
     
END   
GO



USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetAimVisionByParam]    Script Date: 13-10-2022 9.24.29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER  PROCEDURE [dbo].[GetAimVisionByParam]          
(          
 @Id  uniqueidentifier = NULL,          
 @IsPublished bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
     
      ,[IsDeleted]
      ,[IsPublished]
	  ,AimContent
	  ,VisionContent
		,AimContentHindi
		 ,VisionContentHindi
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[AimVision]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsPublished = @IsPublished) OR @IsPublished IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
  
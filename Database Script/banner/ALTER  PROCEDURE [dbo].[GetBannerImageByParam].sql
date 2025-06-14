USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetBannerImageByParam]    Script Date: 09-10-2022 7.04.54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER  PROCEDURE [dbo].[GetBannerImageByParam]          
(          
 @Id  uniqueidentifier = NULL,          
 @IsPublished bit = NULL,          
 @IsDeleted bit = NULL,
 @IsGallery bit = NULL   
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
     
      ,[IsDeleted]
      ,[IsPublished]
		FileName,
		ContentType,
		Image,
      [CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
	   ,Description
		,DescriptionHindi
		,IsGallery

  FROM [DTLPension].[dbo].[BannerImage]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsPublished = @IsPublished) OR @IsPublished IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)     
  AND ((IsGallery = @IsGallery) OR @IsGallery IS NULL)           
     
END   
  
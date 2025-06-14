USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditBannerImage]    Script Date: 09-10-2022 7.06.17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER   PROCEDURE [dbo].[AddEditBannerImage]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@Description  [nvarchar](max) NULL,
	@DescriptionHindi  [nvarchar](max) NULL,
	@FileName  [nvarchar](250) NULL,
	@ContentType  [nvarchar](250) NULL,
	@Image  [varbinary](max) NULL,
	@IsGallery bit = NULL,
	@CreatedBy [nvarchar] (250) = NULL  ,
	@ModifiedBy [nvarchar](250)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [BannerImage]      
           (Id,
		   Description,
		   DescriptionHindi,
		   FileName,
		   ContentType,
		   Image,
	IsDeleted,
	IsPublished,
	IsGallery,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	 @Description,
	 @DescriptionHindi,
		   @FileName,
		   @ContentType,
		   @Image,
	@IsDeleted,
	@IsPublished,
	@IsGallery,
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [BannerImage]      
  set     
   
	IsDeleted   = @IsDeleted,
	IsPublished = @IsPublished,
	Description = @Description ,
	DescriptionHindi = @DescriptionHindi ,
	FileName = @FileName,
	ContentType = @ContentType,
	Image = @Image,
	IsGallery = @IsGallery,
	ModifiedBy = @ModifiedBy,
    ModifiedDate = GETDATE()    
     where Id = @Id   
        set @ReturnMsg ='update'    
  end    
  end try    
 begin catch     
  set @ReturnMsg = ERROR_MESSAGE()     
    
  end catch    
END   


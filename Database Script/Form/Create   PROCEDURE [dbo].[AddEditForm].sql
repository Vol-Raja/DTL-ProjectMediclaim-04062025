USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditForm]    Script Date: 17-10-2022 8.39.13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




Create   PROCEDURE [dbo].[AddEditForm]      
    
  @Id uniqueIdentifier = null,
  @TitleInEnglish  [nvarchar](250),
   @TitleInHindi  [nvarchar](250),
 
	 
	@AttachmentTitleInEnglish  [nvarchar](max)  ,
	@AttachmentTitleInHindi  [nvarchar](max)  ,
	@AttachmentFileInEnglish [varbinary](max)  ,
	@EnglishFileName [nvarchar](250)  NULL,
	@EnglishContentType  [nvarchar](250) NULL,
	@AttachmentFileInHindi [varbinary](max)  ,
	@HindiFileName [nvarchar](250)  NULL,
	@HindiContentType  [nvarchar](250) NULL,
	@IsDeleted  bit,
	 
	@CreatedBy [nvarchar] (50) = NULL  ,
	@ModifiedBy [nvarchar](50)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [Form]      
           (ID,
	TitleInEnglish,
	TitleInHindi,
	 
	 
	AttachmentTitleInEnglish,
	AttachmentTitleInHindi,
	AttachmentFileInEnglish,
	EnglishFileName ,
	EnglishContentType ,
	AttachmentFileInHindi,
	HindiFileName  ,
	HindiContentType  ,
	IsDeleted,
	 
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@TitleInEnglish,
	@TitleInHindi,
	 
	@AttachmentTitleInEnglish,
	@AttachmentTitleInHindi,
	@AttachmentFileInEnglish,
	@EnglishFileName ,
	@EnglishContentType ,
	@AttachmentFileInHindi,
	@HindiFileName  ,
	@HindiContentType,
	@IsDeleted,
	
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [Form]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi,
	 
 
	AttachmentTitleInEnglish  = @AttachmentTitleInEnglish,
	AttachmentTitleInHindi = @AttachmentTitleInHindi,
	AttachmentFileInEnglish = @AttachmentFileInEnglish,
	EnglishFileName = @EnglishFileName,
	EnglishContentType = @EnglishContentType,
	AttachmentFileInHindi = @AttachmentFileInHindi,
	HindiFileName = @HindiFileName  ,
	HindiContentType = @HindiContentType,
	IsDeleted   = @IsDeleted,

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
  
GO



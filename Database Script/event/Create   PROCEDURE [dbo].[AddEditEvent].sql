USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditEvent]    Script Date: 17-10-2022 5.32.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create   PROCEDURE [dbo].[AddEditEvent]      
    
  @Id uniqueIdentifier = null,
  @TitleInEnglish  [nvarchar](250),
   @TitleInHindi  [nvarchar](250),
	@EventDate  [datetime],
	 
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
 INSERT INTO [Event]      
           (ID,
	TitleInEnglish,
	TitleInHindi,
	EventDate,
	 
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
	 @EventDate,
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
      
  Update  [Event]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi,
	EventDate = @EventDate,
 
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
  
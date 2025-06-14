USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditWhatsNew]    Script Date: 16-10-2022 9.04.37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER   PROCEDURE [dbo].[AddEditWhatsNew]      
    
  @Id uniqueIdentifier = null,
  @TitleInEnglish  [nvarchar](250),
   @TitleInHindi  [nvarchar](250),
	@WhatsNewDate  [datetime],
	@WhatsNewDateHindi [datetime],
	@AttachmentTitleInEnglish  [nvarchar](max)  ,
	@AttachmentTitleInHindi  [nvarchar](max)  ,
	@AttachmentFileInEnglish [varbinary](max)  ,
	@EnglishFileName [nvarchar](250)  NULL,
	@EnglishContentType  [nvarchar](250) NULL,
	@AttachmentFileInHindi [varbinary](max)  ,
	@HindiFileName [nvarchar](250)  NULL,
	@HindiContentType  [nvarchar](250) NULL,
	@IsDeleted  bit,
	@IsArchieved  bit,
	@Description NVARCHAR(MAX),
@DescriptionHindi NVARCHAR(MAX),
@ImageFileName NVARCHAR(250),
@ImageContentType NVARCHAR(250),
@Image VARBINARY(MAX),
	@CreatedBy [nvarchar] (50) = NULL  ,
	@ModifiedBy [nvarchar](50)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [WhatsNew]      
           (ID,
	TitleInEnglish,
	TitleInHindi,
	WhatsNewDate,
	WhatsNewDateHindi,
	AttachmentTitleInEnglish,
	AttachmentTitleInHindi,
	AttachmentFileInEnglish,
	EnglishFileName ,
	EnglishContentType ,
	AttachmentFileInHindi,
	HindiFileName  ,
	HindiContentType  ,
	IsDeleted,
	IsArchieved,
	Description,
	DescriptionHindi,
	ImageFileName ,
	ImageContentType,
	Image ,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@TitleInEnglish,
	@TitleInHindi,
	@WhatsNewDate,
	@WhatsNewDateHindi,
	@AttachmentTitleInEnglish,
	@AttachmentTitleInHindi,
	@AttachmentFileInEnglish,
	@EnglishFileName ,
	@EnglishContentType ,
	@AttachmentFileInHindi,
	@HindiFileName  ,
	@HindiContentType,
	@IsDeleted,
	@IsArchieved,
	@Description,
	@DescriptionHindi ,
	@ImageFileName ,
	@ImageContentType,
	@Image ,
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [WhatsNew]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi,
	WhatsNewDate = @WhatsNewDate,
	WhatsNewDateHindi =@WhatsNewDateHindi,
	AttachmentTitleInEnglish  = @AttachmentTitleInEnglish,
	AttachmentTitleInHindi = @AttachmentTitleInHindi,
	AttachmentFileInEnglish = @AttachmentFileInEnglish,
	EnglishFileName = @EnglishFileName,
	EnglishContentType = @EnglishContentType,
	AttachmentFileInHindi = @AttachmentFileInHindi,
	HindiFileName = @HindiFileName  ,
	HindiContentType = @HindiContentType,
	IsDeleted   = @IsDeleted,
	IsArchieved = @IsArchieved,
	Description =@Description,
	DescriptionHindi=@DescriptionHindi,
	ImageFileName =@ImageFileName,
	ImageContentType=@ImageContentType,
	Image =@Image,
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
  
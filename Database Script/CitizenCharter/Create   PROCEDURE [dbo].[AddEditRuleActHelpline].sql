USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditRuleActHelpline]    Script Date: 18-10-2022 12.32.11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





Create   PROCEDURE [dbo].[AddEditRuleActHelpline]      
    
  @Id uniqueIdentifier = null,
  @TitleInEnglish  [nvarchar](250),
   @TitleInHindi  [nvarchar](250),
	@ContactNumber [nvarchar](250)  NULL,
	@ContactNumberInHindi  [nvarchar](250) NULL,
	 @IsHelpline  bit,
	@HelplineDescription  [nvarchar](max)  ,
	@HelplineDescriptionInHindi  [nvarchar](max)  ,
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
 INSERT INTO [RuleActHelpline]      
           (ID,
	TitleInEnglish,
	TitleInHindi,
	 
	 
ContactNumber,
	ContactNumberInHindi  ,
	 IsHelpline ,
	HelplineDescription    ,
	HelplineDescriptionInHindi  ,
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
	 
	@ContactNumber ,
	@ContactNumberInHindi ,
	 @IsHelpline  ,
	@HelplineDescription    ,
	@HelplineDescriptionInHindi   ,

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
      
  Update  [RuleActHelpline]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi,
	ContactNumber= @ContactNumber ,
	ContactNumberInHindi =@ContactNumberInHindi  ,
	IsHelpline = @IsHelpline  ,
	HelplineDescription =@HelplineDescription   ,
	HelplineDescriptionInHindi =@HelplineDescriptionInHindi  ,
 
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



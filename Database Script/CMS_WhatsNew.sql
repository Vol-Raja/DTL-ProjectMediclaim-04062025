
SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[WhatsNew](
	ID [uniqueidentifier] NOT NULL,
	TitleInEnglish  [nvarchar](250) NOT NULL,
	TitleInHindi  [nvarchar](250) NULL,
	
	WhatsNewDate  [datetime] NULL,
	WhatsNewDateHindi  [datetime] NULL,

	AttachmentTitleInEnglish  [nvarchar](max) NULL,
	AttachmentTitleInHindi  [nvarchar](max) NULL,
	AttachmentFileInEnglish [varbinary](max) NULL,
	EnglishFileName [nvarchar](250)  NULL,
	EnglishContentType  [nvarchar](250) NULL,
	AttachmentFileInHindi [varbinary](max) NULL,
	HindiFileName [nvarchar](250)  NULL,
	HindiContentType  [nvarchar](250) NULL,
	IsDeleted  bit,
	IsArchieved  bit,

	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,


 CONSTRAINT [PK_WhatsNew] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[WhatsNew] ADD  CONSTRAINT [DF_WhatsNew_ID]  DEFAULT (newid()) FOR [ID]
GO


ALTER TABLE [dbo].[WhatsNew] ADD  CONSTRAINT [DF_WhatsNew_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE   PROCEDURE [dbo].[AddEditWhatsNew]      
    
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
  
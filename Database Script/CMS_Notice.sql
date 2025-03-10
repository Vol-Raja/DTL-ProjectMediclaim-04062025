
SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[Notice](
	ID [uniqueidentifier] NOT NULL,
	TitleInEnglish  [nvarchar](250) NOT NULL,
	TitleInHindi  [nvarchar](250) NULL,
	
	NoticeDate  [datetime] NULL,
	NoticeDateHindi  [datetime] NULL,

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


 CONSTRAINT [PK_Notice] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Notice] ADD  CONSTRAINT [DF_Notice_ID]  DEFAULT (newid()) FOR [ID]
GO


ALTER TABLE [dbo].[Notice] ADD  CONSTRAINT [DF_Notice_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE   PROCEDURE [dbo].[AddEditNotice]      
    
  @Id uniqueIdentifier = null,
  @TitleInEnglish  [nvarchar](250),
   @TitleInHindi  [nvarchar](250),
	@NoticeDate  [datetime],
	@NoticeDateHindi [datetime],
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
 INSERT INTO [Notice]      
           (ID,
	TitleInEnglish,
	TitleInHindi,
	NoticeDate,
	NoticeDateHindi,
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
           (@ID,
	@TitleInEnglish,
	@TitleInHindi,
	@NoticeDate,
	@NoticeDateHindi,
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
      
  Update  [Notice]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi,
	NoticeDate = @NoticeDate,
	NoticeDateHindi =@NoticeDateHindi,
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
  
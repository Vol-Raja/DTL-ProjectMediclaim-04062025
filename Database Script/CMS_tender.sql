

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tender](
	ID [uniqueidentifier] NOT NULL,
	TitleInEnglish  [nvarchar](250) NOT NULL,
	TitleInHindi  [nvarchar](250) NOT NULL,
	TenderIdInHindi  [nvarchar](250) NOT NULL,
	TenderIdEnglish  [nvarchar](250) NOT NULL,
	OpeningDate  [datetime] NULL,
	OpeningTime  [datetime] NULL,
	PublishDate  [datetime] NULL,
	PublishTime  [datetime] NULL,
	ClosingDate  [datetime] NULL,
	ClosingTime  [datetime] NULL,

	DiscriptionInEnglish  [nvarchar](max) NULL,
	DiscriptionInHindi  [nvarchar](max) NULL,
	AttachmentTitleInEnglish  [nvarchar](max) NULL,
	AttachmentTitleInHindi  [nvarchar](max) NULL,
	AttachmentFileInEnglish [varbinary](max) NULL,
	AttachmentFileInHindi [varbinary](max) NULL,

	IsDeleted  bit,
	IsArchieved  bit,

	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,


 CONSTRAINT [PK_Tender] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Tender] ADD  CONSTRAINT [DF_Tender_ID]  DEFAULT (newid()) FOR [ID]
GO


ALTER TABLE [dbo].[Tender] ADD  CONSTRAINT [DF_Tender_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


CREATE   PROCEDURE [dbo].[AddEditTender]      
    
  @Id uniqueIdentifier = null,   
  @TitleInEnglish  [nvarchar](250) ,  
   @TitleInHindi  [nvarchar](250),      
   @TenderIdInHindi  [nvarchar](250),       
	@TenderIdEnglish  [nvarchar](250),
	@OpeningDate  [datetime]  ,
	@OpeningTime  [datetime]  ,
	@PublishDate  [datetime]  ,
	@PublishTime  [datetime]  ,
	@ClosingDate  [datetime]  ,
	@ClosingTime  [datetime]  ,

	@DiscriptionInEnglish  [nvarchar](max)  ,
	@DiscriptionInHindi  [nvarchar](max)  ,
	@AttachmentTitleInEnglish  [nvarchar](max)  ,
	@AttachmentTitleInHindi  [nvarchar](max)  ,
	@AttachmentFileInEnglish [varbinary](max)  ,
	@AttachmentFileInHindi [varbinary](max)  ,
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
 INSERT INTO [Tender]      
           (	ID ,
	TitleInEnglish ,
	TitleInHindi  ,
	TenderIdInHindi  ,
	TenderIdEnglish  ,
	OpeningDate ,
	OpeningTime ,
	PublishDate ,
	PublishTime ,
	ClosingDate ,
	ClosingTime ,

	DiscriptionInEnglish ,
	DiscriptionInHindi ,
	AttachmentTitleInEnglish  ,
	AttachmentTitleInHindi ,
	AttachmentFileInEnglish ,
	AttachmentFileInHindi,
	IsDeleted  ,
	IsArchieved  ,
	CreatedBy  ,
	ModifiedBy)      
     VALUES      
           (@ID ,
	@TitleInEnglish ,
	@TitleInHindi  ,
	@TenderIdInHindi  ,
	@TenderIdEnglish  ,
	@OpeningDate ,
	@OpeningTime ,
	@PublishDate ,
	@PublishTime ,
	@ClosingDate ,
	@ClosingTime ,

	@DiscriptionInEnglish ,
	@DiscriptionInHindi ,
	@AttachmentTitleInEnglish  ,
	@AttachmentTitleInHindi ,
	@AttachmentFileInEnglish ,
	@AttachmentFileInHindi,
	@IsDeleted  ,
	@IsArchieved  ,
	@CreatedBy,
	@ModifiedBy)     
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [Tender]      
  set     
    TitleInEnglish = @TitleInEnglish,
	TitleInHindi = @TitleInHindi  ,
	TenderIdInHindi=TenderIdInHindi  ,
	TenderIdEnglish  = @TenderIdEnglish,
	OpeningDate = @OpeningDate,
	OpeningTime = @OpeningTime,
	PublishDate = @PublishDate,
	PublishTime = @PublishTime,
	ClosingDate = @ClosingDate,
	ClosingTime = @ClosingTime,

	DiscriptionInEnglish = @DiscriptionInEnglish,
	DiscriptionInHindi = @DiscriptionInHindi,
	AttachmentTitleInEnglish  = @AttachmentTitleInEnglish,
	AttachmentTitleInHindi = @AttachmentTitleInHindi,
	AttachmentFileInEnglish = @AttachmentFileInEnglish,
	AttachmentFileInHindi = @AttachmentFileInHindi,
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
  
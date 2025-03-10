

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Link](
	ID [uniqueidentifier] NOT NULL,
	Title  [nvarchar](500) NOT NULL,
	DocumentFileName  [nvarchar](500) NOT NULL,
	ContentType  [nvarchar](250)  NULL,
	FileSize  int  NULL,
	FileContent [varbinary](max) NULL,
	IsDeleted  bit,
	IsArchieved  bit,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,


 CONSTRAINT [PK_Link] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Link] ADD  CONSTRAINT [DF_Link_ID]  DEFAULT (newid()) FOR [ID]
GO


ALTER TABLE [dbo].[Link] ADD  CONSTRAINT [DF_Link_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


CREATE   PROCEDURE [dbo].[AddEditLink]      
    
  @Id uniqueIdentifier = null,   
  @Title  [nvarchar](500) ,  
   @DocumentFileName  [nvarchar](500),      
   @ContentType  [nvarchar](250),       
	@FileSize  int,
	@FileContent [varbinary](max) ,
	
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
 INSERT INTO [Link]      
           (	ID ,
	Title ,
	DocumentFileName  ,
	ContentType  ,
	FileSize  ,
	FileContent ,
	
	IsDeleted  ,
	IsArchieved  ,
	CreatedBy  ,
	ModifiedBy)      
     VALUES      
           (@ID ,
	@Title ,
	@DocumentFileName  ,
	@ContentType  ,
	@FileSize  ,
	@FileContent ,

	@IsDeleted  ,
	@IsArchieved  ,
	@CreatedBy,
	@ModifiedBy)     
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [Link]      
  set     
    Title = @Title,
	DocumentFileName = @DocumentFileName  ,
	ContentType=@ContentType  ,
	FileSize  = @FileSize,
	FileContent = @FileContent,
	
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
  

SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[BannerImage](
	Id [uniqueidentifier] NOT NULL,
	IsPublished  bit,
	Description  [nvarchar](250) NULL,
	FileName  [nvarchar](250) NULL,
	ContentType  [nvarchar](250) NULL,
	IsDeleted  bit,
	Image  [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,


 CONSTRAINT [PK_BannerImage] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[BannerImage] ADD  CONSTRAINT [DF_BannerImage_Id]  DEFAULT (newid()) FOR [Id]
GO


ALTER TABLE [dbo].[BannerImage] ADD  CONSTRAINT [DF_BannerImage_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE   PROCEDURE [dbo].[AddEditBannerImage]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@Description  [nvarchar](250) NULL,
	@FileName  [nvarchar](250) NULL,
	@ContentType  [nvarchar](250) NULL,
	
	@Image  [varbinary](max) NULL,
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
		   FileName,
		   ContentType,
		   Image,
	IsDeleted,
	IsPublished,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	 @Description,
		   @FileName,
		   @ContentType,
		   @Image,
	@IsDeleted,
	@IsPublished,
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
	FileName = @FileName,
	ContentType = @ContentType,
	Image = @Image,
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

go
 
CREATE  PROCEDURE [dbo].[GetBannerImageByParam]          
(          
 @Id  uniqueidentifier = NULL,          
 @IsPublished bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
     
      ,[IsDeleted]
      ,[IsPublished]
	  ,Description,
		FileName,
		ContentType,
		Image,
      [CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[BannerImage]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsPublished = @IsPublished) OR @IsPublished IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
  
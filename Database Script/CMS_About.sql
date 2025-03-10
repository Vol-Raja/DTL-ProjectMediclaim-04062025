
SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[About](
	Id [uniqueidentifier] NOT NULL,
	IsPublished  bit,
	TextContent  [nvarchar](max) NULL,
	
	IsDeleted  bit,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,


 CONSTRAINT [PK_About] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[About] ADD  CONSTRAINT [DF_About_Id]  DEFAULT (newid()) FOR [Id]
GO


ALTER TABLE [dbo].[About] ADD  CONSTRAINT [DF_About_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE   PROCEDURE [dbo].[AddEditAbout]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@TextContent  [nvarchar](max) = NULL,
	@CreatedBy [nvarchar] (250) = NULL  ,
	@ModifiedBy [nvarchar](250)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [About]      
           (Id,
		   TextContent,
	IsDeleted,
	IsPublished,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@TextContent,
	@IsDeleted,
	@IsPublished,
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [About]      
  set     
   
	IsDeleted   = @IsDeleted,
	IsPublished = @IsPublished,
	TextContent =@TextContent
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


 
CREATE  PROCEDURE [dbo].[GetAboutByParam]          
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
	  ,TextContent
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[About]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsPublished = @IsPublished) OR @IsPublished IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
  
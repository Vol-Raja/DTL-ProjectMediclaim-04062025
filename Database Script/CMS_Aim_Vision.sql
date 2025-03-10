
SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[AimVision](
	Id [uniqueidentifier] NOT NULL,
	IsPublished  bit,
	AimContent  [nvarchar](max) NULL,
	VisionContent  [nvarchar](max) NULL,
	IsDeleted  bit,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,


 CONSTRAINT [PK_AimVision] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[AimVision] ADD  CONSTRAINT [DF_AimVision_Id]  DEFAULT (newid()) FOR [Id]
GO


ALTER TABLE [dbo].[AimVision] ADD  CONSTRAINT [DF_AimVision_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE   PROCEDURE [dbo].[AddEditAimVision]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@AimContent  [nvarchar](max) = NULL,
	@VisionContent  [nvarchar](max) = NULL,
	@CreatedBy [nvarchar] (250) = NULL  ,
	@ModifiedBy [nvarchar](250)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [AimVision]      
           (Id,
		   AimContent,
		    VisionContent,
	IsDeleted,
	IsPublished,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@AimContent,
	@VisionContent,
	@IsDeleted,
	@IsPublished,
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [AimVision]      
  set     
   
	IsDeleted   = @IsDeleted,
	IsPublished = @IsPublished,
	AimContent =@AimContent,
	VisionContent =@VisionContent,
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
 
CREATE  PROCEDURE [dbo].[GetAimVisionByParam]          
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
	  ,AimContent
	    ,VisionContent
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[AimVision]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
   AND ((IsPublished = @IsPublished) OR @IsPublished IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   
  
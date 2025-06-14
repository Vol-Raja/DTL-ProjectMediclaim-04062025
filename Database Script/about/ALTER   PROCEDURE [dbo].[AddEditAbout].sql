USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditAbout]    Script Date: 22-09-2022 8.14.00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER   PROCEDURE [dbo].[AddEditAbout]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@TextContent  [nvarchar](max) = NULL,
	@CreatedBy [nvarchar] (250) = NULL  ,
	@ModifiedBy [nvarchar](250)  = NULL,
	@TextContentHindi nvarchar(max) = NULL,
  @Image varbinary(max) = NULL,
   @ImageName nvarchar (250) = NULL,
	@ImageContentType nvarchar(250)  = NULL,
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
	TextContentHindi,
	Image,
	ImageName,
	ImageContentType,

	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@TextContent,
	@IsDeleted,
	@IsPublished,
	@TextContentHindi,
	@Image,
	@ImageName,
	@ImageContentType,
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
	TextContent =@TextContent,
	TextContentHindi =@TextContentHindi,
	Image =@Image,
	ImageContentType =@ImageContentType,
	ImageName =@ImageName,
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


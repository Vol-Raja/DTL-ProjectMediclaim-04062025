USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditSocialMediaAccount]    Script Date: 17-10-2022 9.19.23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create   PROCEDURE [dbo].[AddEditSocialMediaAccount]      
    
  @Id uniqueIdentifier = null,
	@Facebook nvarchar(500)  NULL,
	@Instagram  nvarchar(500) NULL,
	@Youtube nvarchar(500) NULL,
	@Twitter nvarchar(500) NULL,

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
 INSERT INTO [SocialMediaAccount]      
           (ID,
	[Facebook]  ,
	[Instagram]  ,
	[Youtube]  ,
	[Twitter] ,
	IsDeleted,
	 
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@Facebook ,
	@Instagram  ,
	@Youtube  ,
	@Twitter ,
	@IsDeleted,
	
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [SocialMediaAccount]      
  set     
   
	IsDeleted   = @IsDeleted,
	Facebook	=@Facebook ,
	Instagram=@Instagram  ,
	Youtube =@Youtube  ,
	Twitter=@Twitter ,
	
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



USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditAimVision]    Script Date: 13-10-2022 9.21.42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER   PROCEDURE [dbo].[AddEditAimVision]      
    
  @Id uniqueIdentifier = null,
  
	@IsDeleted  bit,
	@IsPublished  bit,
	@AimContent  [nvarchar](max) = NULL,
	@VisionContent  [nvarchar](max) = NULL,
	@AimContentHindi  [nvarchar](max) = NULL,
	@VisionContentHindi  [nvarchar](max) = NULL,
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
			  AimContentHindi,
		    VisionContentHindi,
	IsDeleted,
	IsPublished,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@AimContent,
	@VisionContent,
	@AimContentHindi,
	@VisionContentHindi,
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
	AimContentHindi = @AimContentHindi,
	VisionContentHindi = @VisionContentHindi,
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


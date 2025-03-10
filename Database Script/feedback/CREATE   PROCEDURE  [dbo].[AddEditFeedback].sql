USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditFeedback]    Script Date: 25-02-2023 7.02.11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE   PROCEDURE  [dbo].[AddEditFeedback]       
 @ID uniqueidentifier  = NULL,
	 @Name nvarchar(250),
	 @EmailAddress   nvarchar (250) ,
	 @PhoneNumber   nvarchar (250) ,
	  @Details   nvarchar (max) ,
	@IsDeleted  bit,
	@CreatedBy  nvarchar  (50) =null  ,
	@ModifiedBy  nvarchar (50) =null  ,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO  Feedback       
           (ID,
		   [Name],
		   EmailAddress ,
		  
		   PhoneNumber,
		   Details ,
	IsDeleted,
	 
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@Name,
	 
	@EmailAddress,
 
	@PhoneNumber,
	@Details,
	@IsDeleted,
	
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update   Feedback       
  set     
    Name = @Name,
	 
	EmailAddress = @EmailAddress,
	PhoneNumber =@PhoneNumber,
	Details =@Details,
	IsDeleted   = @IsDeleted,

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



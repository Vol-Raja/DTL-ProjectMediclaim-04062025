USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditLink]    Script Date: 15-10-2022 3.28.18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   PROCEDURE [dbo].[AddEditLink]      
    
  @Id uniqueIdentifier = null,   
  @Title  [nvarchar](500) ,  
  @TitleHindi  [nvarchar](500) , 
   @Link  [nvarchar](500) ,
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
	TitleHindi,
	Link,
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
	@TitleHindi,
	@Link,
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
	TitleHindi = @TitleHindi,
	Link = @Link,
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
  
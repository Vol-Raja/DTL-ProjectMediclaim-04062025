USE  DTLPension 
GO
/****** Object:  StoredProcedure  dbo . AddEditContactUs     Script Date: 17-10-2022 5.32.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter   PROCEDURE  dbo.AddEditContactUs       
 @ID uniqueidentifier  = NULL,
	 @Name nvarchar(250),
	 @NameHindi   nvarchar (250),
	 @Designation   nvarchar (250) ,
	 @DesignationHindi   nvarchar (250) ,
	 @EmailAddress   nvarchar (250) ,
	 @Telephone   nvarchar (250) ,
	 @PhoneNumber   nvarchar (250) ,
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
 INSERT INTO  ContactUs       
           (ID,
		   [Name],
		   NameHindi,
		   Designation,
		   DesignationHindi,
		   EmailAddress ,
		   Telephone,
		   PhoneNumber,

	IsDeleted,
	 
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@Name,
	@NameHindi,
	@Designation,
	@DesignationHindi,
	@EmailAddress,
	@Telephone,
	@PhoneNumber,

	@IsDeleted,
	
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update   ContactUs       
  set     
    Name = @Name,
	NameHindi = @NameHindi,
	Designation = @Designation,
	DesignationHindi=@DesignationHindi,
	EmailAddress = @EmailAddress,
	PhoneNumber =@PhoneNumber,
	Telephone =@Telephone,
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
  
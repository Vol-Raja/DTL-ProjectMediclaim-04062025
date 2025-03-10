CREATE TABLE DVBUser
(
	Id UNIQUEIDENTIFIER NOT NULL,
	DVBUserId INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(60) NOT NULL,
	EmailAddress NVARCHAR(120) NOT NULL,
	PhoneNumber NVARCHAR(20) NOT NULL,
	Department NVARCHAR(100) NOT NULL,
	Designation NVARCHAR(50) NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL
	
	CONSTRAINT PK_DVBUSer_ID PRIMARY KEY (DVBUserId),
	CONSTRAINT UQ_DVBUSer_EmailAddress UNIQUE (EmailAddress),
);

ALTER TABLE DVBUSER 
ADD CONSTRAINT DF_DVBUSer_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE DVBUSER 
ADD CONSTRAINT DF_DVBUSer_IsDeleted DEFAULT 0 FOR IsDeleted



CREATE PROCEDURE [dbo].[GetDVBUserByParam]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@DVBUserId INT
)
As
BEGIN
	SELECT
		DVBUserId
		,[Name]
		,EmailAddress
		,PhoneNumber
		,Department
		,Designation
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.DVBUser
	WHERE 
		((DVBUserId = @DVBUserId) OR @DVBUserId IS NULL)
	AND (([Name] = @Name) OR @Name IS NULL)
	AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)
	AND ((Department = @Department) OR @Department IS NULL)
	AND ((Designation =@Designation ) OR @Designation  IS NULL)
	AND IsDeleted = 0
END
GO

CREATE PROCEDURE [dbo].[SaveDVBUser]
(
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@CreatedBy NVARCHAR(60),
	@DVBUserId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[DVBUser]
			([ID]
			,[Name]
			,[EmailAddress]	
			,[PhoneNumber]
			,[Department]
			,[Designation]
			,[CreatedBy])
     VALUES
           (@ID
		    ,@Name
			,@EmailAddress
			,@PhoneNumber
			,@Department
			,@Designation
			,@CreatedBy)
	COMMIT TRAN  

	SET @DVBUserId = SCOPE_IDENTITY();
	
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
  
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();  
  
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH
GO


CREATE PROCEDURE [dbo].[DeleteDVBUser]
(	
	@DVBUserId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.DVBUser SET IsDeleted = 1
	WHERE DVBUserId = @DVBUserId;
	COMMIT TRAN
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH
GO

CREATE PROCEDURE [dbo].[UpdateDVBUser]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@ModifiedBy NVARCHAR(60),
	@DVBUserId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.DVBUser 
		SET [Name] = @Name,
		EmailAddress = @EmailAddress,
		PhoneNumber = @PhoneNumber,
		Department = @Department,
		Designation = @Designation,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE DVBUserId = @DVBUserId;
	COMMIT TRAN
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH
GO



GO
EXEC sp_rename 'dbo.DVBUser.Id', 'ID', 'COLUMN';
GO

CREATE TABLE AdminUser
(
	Id UNIQUEIDENTIFIER NOT NULL,
	AdminUserId INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(60) NOT NULL,
	EmailAddress NVARCHAR(120) NOT NULL,
	PhoneNumber NVARCHAR(20) NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL
	
	CONSTRAINT PK_AdminUser_ID PRIMARY KEY (AdminUserId),
	CONSTRAINT UQ_AdminUser_EmailAddress UNIQUE (EmailAddress),
);

ALTER TABLE AdminUser 
ADD CONSTRAINT DF_AdminUser_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE AdminUser 
ADD CONSTRAINT DF_AdminUser_IsDeleted DEFAULT 0 FOR IsDeleted

CREATE PROCEDURE [dbo].[GetAdminUserByParam]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@AdminUserId INT
)
As
BEGIN
	SELECT
		AdminUserId
		,[Name]
		,EmailAddress
		,PhoneNumber
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.AdminUser
	WHERE 
		((AdminUserId = @AdminUserId) OR @AdminUserId IS NULL)
	AND (([Name] = @Name) OR @Name IS NULL)
	AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)
	AND ((PhoneNumber = @PhoneNumber) OR @PhoneNumber IS NULL)
	AND IsDeleted = 0
END
GO

CREATE PROCEDURE [dbo].[GetArchiveAdminUser]
As
BEGIN
	SELECT
		AdminUserId
		,[Name]
		,EmailAddress
		,PhoneNumber
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.AdminUser
	WHERE  
		IsDeleted = 1
END
GO

CREATE PROCEDURE [dbo].[SaveAdminUser]
(
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@CreatedBy NVARCHAR(60),
	@AdminUserId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[AdminUser]
			([ID]
			,[Name]
			,[EmailAddress]	
			,[PhoneNumber]
			,[CreatedBy])
     VALUES
           (@ID
		    ,@Name
			,@EmailAddress
			,@PhoneNumber
			,@CreatedBy)
	COMMIT TRAN  

	SET @AdminUserId = SCOPE_IDENTITY();
	
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


CREATE PROCEDURE [dbo].[DeleteAdminUser]
(	
	@AdminUserId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.AdminUser SET IsDeleted = 1
	WHERE AdminUserId = @AdminUserId;
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

CREATE PROCEDURE [dbo].[UpdateAdminUser]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@ModifiedBy NVARCHAR(60),
	@AdminUserId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.AdminUser 
		SET [Name] = @Name,
		EmailAddress = @EmailAddress,
		PhoneNumber = @PhoneNumber,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE AdminUserId = @AdminUserId;
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



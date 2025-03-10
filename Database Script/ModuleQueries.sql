CREATE TABLE Module
(
	ModuleId INT IDENTITY(1,1) NOT NULL,
	ModuleName NVARCHAR(60) NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL
	
	CONSTRAINT PK_Module_ModuleId PRIMARY KEY (ModuleId)
);

ALTER TABLE Module 
ADD CONSTRAINT DF_Module_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE Module 
ADD CONSTRAINT DF_Module_IsDeleted DEFAULT 0 FOR IsDeleted


CREATE PROCEDURE [dbo].[GetModuleByParam]
(	
	@ModuleId INT,
	@ModuleName NVARCHAR(60)
)
As
BEGIN
	SELECT
		ModuleId
		,ModuleName
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.Module
	WHERE 
		((ModuleId = @ModuleId) OR @ModuleId IS NULL)
	AND ((ModuleName = @ModuleName) OR @ModuleName IS NULL)
	AND IsDeleted = 0
END
GO


CREATE PROCEDURE [dbo].[SaveModule]
(
	@ModuleName NVARCHAR(60),
	@CreatedBy NVARCHAR(60),
	@ModuleId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[Module]
			([ModuleName]
			,[CreatedBy])
     VALUES
           (@ModuleName
			,@CreatedBy)
	COMMIT TRAN  

	SET @ModuleId = SCOPE_IDENTITY();
	
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


CREATE PROCEDURE [dbo].[DeleteModule]
(	
	@ModuleId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.Module SET IsDeleted = 1
	WHERE ModuleId = @ModuleId;
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

CREATE PROCEDURE [dbo].[UpdateModule]
(	
	@ModuleName NVARCHAR(60),
	@ModifiedBy NVARCHAR(60),
	@ModuleId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.Module 
		SET ModuleName = @ModuleName,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE ModuleId = @ModuleId;
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

CREATE PROCEDURE [dbo].[GetArchivedModule]
As
BEGIN
	SELECT
		ModuleId
		,ModuleName
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.Module
	WHERE 
		IsDeleted = 1
END
GO


CREATE TABLE SubModule
(
	SubModuleId INT IDENTITY(1,1) NOT NULL,
	SubModuleName NVARCHAR(60) NOT NULL,
	ModuleName NVARCHAR(60) NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL,
	
	CONSTRAINT PK_SubModule_SubModuleId PRIMARY KEY (SubModuleId),
	CONSTRAINT UQ_SubModule_Module UNIQUE (SubModuleName,ModuleName)
);

ALTER TABLE SubModule 
ADD CONSTRAINT DF_SubModule_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE SubModule 
ADD CONSTRAINT DF_SubModule_IsDeleted DEFAULT 0 FOR IsDeleted


CREATE PROCEDURE [dbo].[GetSubModuleByParam]
(	
	@SubModuleId INT,
	@SubModuleName NVARCHAR(60),	
	@ModuleName NVARCHAR(60)
)
As
BEGIN
	SELECT
		SubModuleId
		,SubModuleName
		,ModuleName
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.SubModule
	WHERE 
		((SubModuleId = @SubModuleId) OR @SubModuleId IS NULL)
	AND ((SubModuleName = @SubModuleName) OR @SubModuleName IS NULL)
	AND ((ModuleName = @ModuleName) OR @ModuleName IS NULL)
	AND IsDeleted = 0
END
GO


CREATE PROCEDURE [dbo].[SaveSubModule]
(
	@SubModuleName NVARCHAR(60),
	@ModuleName NVARCHAR(60),
	@CreatedBy NVARCHAR(60),
	@SubModuleId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[SubModule]
			([SubModuleName]
			,[ModuleName]
			,[CreatedBy])
     VALUES
           (@SubModuleName
		    ,@ModuleName
			,@CreatedBy)
	COMMIT TRAN  

	SET @SubModuleId = SCOPE_IDENTITY();
	
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


CREATE PROCEDURE [dbo].[DeleteSubModule]
(	
	@SubModuleId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.SubModule SET IsDeleted = 1
	WHERE SubModuleId = @SubModuleId;
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

CREATE PROCEDURE [dbo].[UpdateSubModule]
(	
	@ModuleName NVARCHAR(60),
	@SubModuleName NVARCHAR(60),
	@ModifiedBy NVARCHAR(60),
	@SubModuleId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.SubModule 
		SET SubModuleName = @SubModuleName, 
		ModuleName = @ModuleName,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE SubModuleId = @SubModuleId;
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

CREATE PROCEDURE [dbo].[GetArchivedSubModule]
As
BEGIN
	SELECT
		SubModuleId
		,SubModuleName
		,ModuleName
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
	FROM dbo.SubModule
	WHERE 
		IsDeleted = 1
END
GO


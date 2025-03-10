DROP TABLE AssignPermission
CREATE TABLE AssignPermission
(
	AssignPermissionId INT IDENTITY(1,1) NOT NULL,
	UserId NVARCHAR(60) NOT NULL,
	ModuleName NVARCHAR(60) NOT NULL,
	SubModuleName NVARCHAR(60) NOT NULL,
	[Create] BIT NOT NULL,
	[View] BIT NOT NULL,
	[Edit] BIT NOT NULL,
	[Delete] BIT NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL
	
	CONSTRAINT PK_AssignPermission_AssignPermissionId PRIMARY KEY (AssignPermissionId),
	CONSTRAINT UQ_AssignPermission UNIQUE (ModuleName,SubModuleName,UserId)
);

ALTER TABLE AssignPermission 
ADD CONSTRAINT DF_AssignPermission_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE AssignPermission 
ADD CONSTRAINT DF_AssignPermission_IsDeleted DEFAULT 0 FOR IsDeleted

DROP PROCEDURE [dbo].[GetAssignPermissionByParam]
CREATE PROCEDURE [dbo].[GetAssignPermissionByParam]
(	
	@UserId NVARCHAR(60),
	@ModuleName NVARCHAR(60),
	@SubModuleName NVARCHAR(60)
)
As
BEGIN
	SELECT
		AssignPermissionId,
		UserId,
		ModuleName,
		SubModuleName,
		[Create],
		[View],
		[Edit],
		[Delete],
		CreatedBy,
		CreatedDate,
		ModifiedBy,
		ModifiedDate,
		IsDeleted
	FROM dbo.AssignPermission
	WHERE 
		((UserId = @UserId) OR @UserId IS NULL)
		AND (([ModuleName] = @ModuleName) OR @ModuleName IS NULL)
		AND (([SubModuleName] = @SubModuleName) OR @SubModuleName IS NULL)
	AND IsDeleted = 0
END
GO

DROP PROCEDURE [dbo].[SaveAssignPermission]
CREATE PROCEDURE [dbo].[SaveAssignPermission]
(
	@UserId NVARCHAR(60),
	@ModuleName NVARCHAR(60),
	@SubModuleName NVARCHAR(60),
	@Create BIT,
	@View BIT,
	@Edit BIT,
	@Delete BIT,
	@CreatedBy NVARCHAR(60),
	@AssignPermissionId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[AssignPermission]
			(UserId
			,ModuleName
			,SubModuleName
			,[Create]
			,[View]
			,[Edit]
			,[Delete]
			,[CreatedBy])
     VALUES
           (@UserId
		   ,@ModuleName
		   ,@SubModuleName
		   ,@Create
		   ,@View
		   ,@Edit
		   ,@Delete
		   ,@CreatedBy)
	COMMIT TRAN  

	SET @AssignPermissionId = SCOPE_IDENTITY();
	
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

DROP PROCEDURE [dbo].[DeleteAssignPermission]
CREATE PROCEDURE [dbo].[DeleteAssignPermission]
(	
	@AssignPermissionId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.AssignPermission SET IsDeleted = 1
	WHERE AssignPermissionId = @AssignPermissionId;
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

DROP PROCEDURE [dbo].[UpdateAssignPermission]
CREATE PROCEDURE [dbo].[UpdateAssignPermission]
(	
	@ModuleName NVARCHAR(60),
	@SubModuleName NVARCHAR(60),
	@Create BIT,
	@View BIT,
	@Edit BIT,
	@Delete BIT,
	@ModifiedBy NVARCHAR(60),
	@AssignPermissionId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.AssignPermission 
		SET ModuleName = @ModuleName,
		SubModuleName = @SubModuleName,
		[Create] = @Create,
		[View] = @View,
		[Edit] = @Edit,
		[Delete] = @Delete,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE AssignPermissionId = @AssignPermissionId
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


DROP PROCEDURE [dbo].[GetArchivedAssignPermission]
CREATE PROCEDURE [dbo].[GetArchivedAssignPermission]
As
BEGIN
	SELECT
		AssignPermissionId,
		UserId,
		ModuleName,
		SubModuleName,
		[Create],
		[View],
		[Edit],
		[Delete],
		CreatedBy,
		CreatedDate,
		ModifiedBy,
		ModifiedDate,
		IsDeleted
	FROM dbo.AssignPermission
	WHERE IsDeleted = 0
END
GO


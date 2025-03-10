CREATE TABLE HospitalUser
(
	ID UNIQUEIDENTIFIER NOT NULL,
	HospitalUserId INT IDENTITY(1,1) NOT NULL,
	NameOfHospital NVARCHAR(60) NOT NULL,
	EmailAddress NVARCHAR(120) NOT NULL,
	PhoneNumber NVARCHAR(20) NOT NULL,
	TypeOfHospital NVARCHAR(150) NOT NULL,
	AuthorizedPerson NVARCHAR(60) NOT NULL,
	Address NVARCHAR(50) NOT NULL,
	CreatedBy NVARCHAR(60) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy NVARCHAR(60) NULL,
	ModifiedDate DATETIME NULL,
	IsDeleted BIT NOT NULL
	
	CONSTRAINT PK_HospitalUser_ID PRIMARY KEY (HospitalUserId),
	CONSTRAINT UQ_HospitalUser_EmailAddress UNIQUE (EmailAddress),
);

ALTER TABLE HospitalUser 
ADD CONSTRAINT DF_HospitalUser_CreatedDate DEFAULT GETDATE() FOR CreatedDate

ALTER TABLE HospitalUser 
ADD CONSTRAINT DF_HospitalUser_IsDeleted DEFAULT 0 FOR IsDeleted



CREATE PROCEDURE [dbo].[GetHospitalUserByParam]
(	
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@TypeOfHospital NVARCHAR(150),
	@PhoneNumber NVARCHAR(50),
	@HospitalUserId INT
)
As
BEGIN
	SELECT
		ID
		,HospitalUserId
		,NameOfHospital
		,EmailAddress
		,PhoneNumber
		,TypeOfHospital
		,AuthorizedPerson
		,Address
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
		,IsDeleted
	FROM dbo.HospitalUser
	WHERE 
		((HospitalUserId = @HospitalUserId) OR @HospitalUserId IS NULL)
	AND ((NameOfHospital LIKE '%'+@NameOfHospital+'%') OR @NameOfHospital IS NULL)
	AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)
	AND ((TypeOfHospital LIKE '%'+@TypeOfHospital+'%') OR @TypeOfHospital IS NULL)
	AND ((PhoneNumber =@PhoneNumber ) OR @PhoneNumber  IS NULL)
	AND IsDeleted = 0
END
GO

CREATE PROCEDURE [dbo].[SaveHospitalUser]
(
	@ID UNIQUEIDENTIFIER,
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@TypeOfHospital NVARCHAR(150),
	@AuthorizedPerson NVARCHAR(60),
	@Address NVARCHAR(50),
	@CreatedBy NVARCHAR(60),
	@HospitalUserId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[HospitalUser]
			([ID]
			,[NameofHospital]
			,[EmailAddress]	
			,[PhoneNumber]
			,[TypeOfHospital]
			,[AuthorizedPerson]
			,[Address]
			,[CreatedBy])
     VALUES
           (@ID
		    ,@NameOfHospital
			,@EmailAddress
			,@PhoneNumber
			,@TypeOfHospital
			,@AuthorizedPerson
			,@Address
			,@CreatedBy)
	COMMIT TRAN  

	SET @HospitalUserId = SCOPE_IDENTITY();
	
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


CREATE PROCEDURE [dbo].[DeleteHospitalUser]
(	
	@HospitalUserId INT 
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.HospitalUser SET IsDeleted = 1
	WHERE HospitalUserId = @HospitalUserId;
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

CREATE PROCEDURE [dbo].[UpdateHospitalUser]
(	
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@TypeOfHospital NVARCHAR(150),
	@AuthorizedPerson NVARCHAR(60),
	@Address NVARCHAR(50),
	@ModifiedBy NVARCHAR(60),
	@HospitalUserId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.HospitalUser 
		SET NameOfHospital = @NameOfHospital,
		EmailAddress = @EmailAddress,
		PhoneNumber = @PhoneNumber,
		TypeOfHospital = @TypeOfHospital,
		AuthorizedPerson = @AuthorizedPerson,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE()
	WHERE HospitalUserId = @HospitalUserId;
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


CREATE PROCEDURE [dbo].[GetArchiveHospitalUser]

As
BEGIN
	SELECT
		ID
		,HospitalUserId
		,NameOfHospital
		,EmailAddress
		,PhoneNumber
		,TypeOfHospital
		,AuthorizedPerson
		,Address
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
		,IsDeleted
	FROM dbo.HospitalUser
	WHERE 
		IsDeleted = 1
END
GO
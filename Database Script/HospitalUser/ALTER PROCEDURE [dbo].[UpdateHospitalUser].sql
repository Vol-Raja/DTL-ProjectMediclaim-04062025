USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[UpdateHospitalUser]    Script Date: 12-02-2023 9.19.59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateHospitalUser]
(	
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@TypeOfHospital NVARCHAR(150),
	@AuthorizedPerson NVARCHAR(60),
	@Address NVARCHAR(50),
	@ModifiedBy NVARCHAR(60),
	@HospitalUserId INT,
	@Username NVARCHAR(255)
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
		Username = @Username,
		Address = @Address,
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

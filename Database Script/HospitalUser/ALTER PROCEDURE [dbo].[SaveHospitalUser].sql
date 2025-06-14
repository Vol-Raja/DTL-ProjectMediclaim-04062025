USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveHospitalUser]    Script Date: 25-09-2022 9.37.48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveHospitalUser]
(
	@ID UNIQUEIDENTIFIER,
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@TypeOfHospital NVARCHAR(150),
	@AuthorizedPerson NVARCHAR(60),
	@Address NVARCHAR(50),
	@CreatedBy NVARCHAR(60),
	@HospitalUserId INT OUTPUT,
	@Username NVARCHAR(255)
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
			,Username
			,[CreatedBy])
     VALUES
           (@ID
		    ,@NameOfHospital
			,@EmailAddress
			,@PhoneNumber
			,@TypeOfHospital
			,@AuthorizedPerson
			,@Address
			,@Username
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

USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveAdminUser]    Script Date: 06-12-2022 7.01.31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveAdminUser]
(
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@CreatedBy NVARCHAR(60),
	@Username NVARCHAR(120),
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
			,[Username]
			,[CreatedBy])
     VALUES
           (@ID
		    ,@Name
			,@EmailAddress
			,@PhoneNumber
			,@Username
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
